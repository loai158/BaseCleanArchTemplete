using Domain.Abstracts;
using Domain.Commands.User;
using Domain.DTOs.User;
using Domain.Entities.Business;
using Domain.Entities.Roles;
using Domain.Entities.User;
using Domain.Exceptions;
using Domain.Features.Retailers.Commands;
using Domain.Features.Suppliers.Commands;
using Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Service.Implemetations
{
    public class AuthService : IAuthService
    {
        private readonly IOptions<JWT> _jwt;
        private readonly RoleManager<ApplicationRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUnitOfWork _unitOfWork;

        public AuthService(IOptions<JWT> jwt, RoleManager<ApplicationRole> roleManager,
         UserManager<ApplicationUser> userManager, IUnitOfWork unitOfWork)
        {
            _userManager = userManager;
            _jwt = jwt;
            _roleManager = roleManager;
            _unitOfWork = unitOfWork;
        }
        public async Task<SimpleResult<AuthResponse>> RegisterRetailerAsync(RegisterRetailerCommand request)
        {
            // 1. تأكد إن الـ Email مش موجود
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return SimpleResult<AuthResponse>.Failure(
                    ErrorCode.AlreadyExist, "Email already exists");

            // 2. Create Identity User
            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return SimpleResult<AuthResponse>.Failure(
                    ErrorCode.AccessDenied, result.Errors.First().Description);

            // 3. Create UserDetails
            var userDetails = new UserDetails();
            userDetails.CreateUserDetails(
                firstName: request.FirstName,
                lastName: request.LastName,
                userName: request.Email,
                userId: user.Id
            );
            await _unitOfWork.Repository<UserDetails>().AddAsync(userDetails);

            // 4. Assign Role
            await _userManager.AddToRoleAsync(user, "Retailer");

            // 5. Create Address
            var address = new Address();
            address.Create(request.Street, request.City, request.Area);
            await _unitOfWork.Repository<Address>().AddAsync(address);
            await _unitOfWork.SaveChangesAsync(); // ← Save عشان نحتاج الـ AddressId

            // 6. Create Retailer Profile
            var retailer = new Retailer
            {
                UserId = user.Id,
                ShopName = request.ShopName,
                PhoneNumber = request.PhoneNumber,
                AddressId = address.Id
            };
            await _unitOfWork.Repository<Retailer>().AddAsync(retailer);
            await _unitOfWork.SaveChangesAsync();

            // 7. Generate Token
            var jwtToken = await CreateJwtToken(user);

            return SimpleResult<AuthResponse>.Success(new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                ExpiresOn = jwtToken.ValidTo,
                Email = user.Email!,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Roles = new List<string> { "Retailer" }
            });
        }

        public async Task<SimpleResult<AuthResponse>> RegisterSupplierAsync(RegisterSupplierCommand request)
        {
            // 1. تأكد إن الـ Email مش موجود
            if (await _userManager.FindByEmailAsync(request.Email) != null)
                return SimpleResult<AuthResponse>.Failure(
                    ErrorCode.AlreadyExist, "Email already exists");

            // 2. Create Identity User
            var user = new ApplicationUser
            {
                Email = request.Email,
                UserName = request.Email,
            };

            var result = await _userManager.CreateAsync(user, request.Password);
            if (!result.Succeeded)
                return SimpleResult<AuthResponse>.Failure(
                    ErrorCode.AccessDenied, result.Errors.First().Description);

            // 3. Create UserDetails
            var userDetails = new UserDetails();
            userDetails.CreateUserDetails(
                firstName: request.FirstName,
                lastName: request.LastName,
                userName: request.Email,
                userId: user.Id
            );
            await _unitOfWork.Repository<UserDetails>().AddAsync(userDetails);

            // 4. Assign Role
            await _userManager.AddToRoleAsync(user, "Supplier");

            // 5. Create Address
            var address = new Address();
            address.Create(request.Street, request.City, request.Area);
            await _unitOfWork.Repository<Address>().AddAsync(address);
            await _unitOfWork.SaveChangesAsync(); // ← Save عشان نحتاج الـ AddressId

            // 6. Create Supplier Profile
            var supplier = new Supplier
            {
                UserId = user.Id,
                StoreName = request.StoreName,
                PhoneNumber = request.PhoneNumber,
                AddressId = address.Id
            };
            await _unitOfWork.Repository<Supplier>().AddAsync(supplier);
            await _unitOfWork.SaveChangesAsync();

            // 7. Generate Token
            var jwtToken = await CreateJwtToken(user);

            return SimpleResult<AuthResponse>.Success(new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                ExpiresOn = jwtToken.ValidTo,
                Email = user.Email!,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Roles = new List<string> { "Supplier" }
            });
        }
        public async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
                var identityRole = await _roleManager.FindByNameAsync(role);
                if (identityRole != null)
                {
                    var permissions = await _roleManager.GetClaimsAsync(identityRole);
                    roleClaims.AddRange(permissions);
                }
            }

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email!),
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim("uid", user.Id),
        }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_jwt.Value.Key));

            var signingCredentials = new SigningCredentials(
                symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            return new JwtSecurityToken(
                issuer: _jwt.Value.Issuer,
                audience: _jwt.Value.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwt.Value.DurationInMinutes),
                signingCredentials: signingCredentials);
        }
        public async Task<SimpleResult<AuthResponse>> LoginAsync(LoginCommand request)
        {
            // 1. جيب الـ User
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return SimpleResult<AuthResponse>.Failure(
                    ErrorCode.AccessDenied, "Invalid email or password");

            // 2. تحقق من الـ Password
            if (!await _userManager.CheckPasswordAsync(user, request.Password))
                return SimpleResult<AuthResponse>.Failure(
                    ErrorCode.AccessDenied, "Invalid email or password");

            // 3. جيب الـ UserDetails
            var userDetails = await _unitOfWork.Repository<UserDetails>()
                .GetOneAsync(u => u.UserId == user.Id);

            // 4. جيب الـ Role
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? string.Empty;

            // 5. Generate Token
            var jwtToken = await CreateJwtToken(user);

            return SimpleResult<AuthResponse>.Success(new AuthResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(jwtToken),
                ExpiresOn = jwtToken.ValidTo,
                Email = user.Email!,
                FirstName = userDetails?.FirstName ?? string.Empty,
                LastName = userDetails?.LastName ?? string.Empty,
                Roles = roles.ToList()
            });
        }

    }
}
