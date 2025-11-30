using BaseCleanArchTemplete.Configuration;
using Domain.DTOs.User;
using Infrastructure.Configuration;
using Microsoft.Extensions.FileProviders;
using Service.Configuration;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssemblies(
        Assembly.GetExecutingAssembly(),
        Assembly.Load("Service"),
        Assembly.Load("Domain")
    );
});
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddServiceServices(builder.Configuration);
builder.Services.AddSwaggerConfiguration();
builder.Services.AddAuthenticationConfiguration(builder.Configuration);
builder.Services.Configure<JWT>(builder.Configuration.GetSection("JWT"));

var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DisplayRequestDuration();
    });
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "uploads")),
    RequestPath = "/uploads"
});
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
