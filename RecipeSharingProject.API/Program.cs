using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using RecipeSharingProject.API;
using RecipeSharingProject.Business;
using RecipeSharingProject.Common.Interfaces;
using Serilog;
using RecipeSharingProject.Common.Model;
using RecipeSharingProject.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, provider, config) => config
//.Enrich.FromLogContext()
.WriteTo.File("log.txt", rollingInterval: RollingInterval.Day,
outputTemplate: "{Timestamp:HH:mm:ss} [{level:u3}] {Message:lj}" + "{Properties:j} {NewLine} {Exception}")
);

DIConfiguration.RegisterService(builder.Services);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IGenericRepository<Recipe>, GenericRepository<Recipe>>();
builder.Services.AddScoped<IUploadService, AzureBlobUploadService>();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(options =>
 {
     options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
     options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;
 }).AddCookie(options =>
 {
     //options.LoginPath = "/Account/Login"; // Specify your login path
 }); ;

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbcontext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbcontext.Database.Migrate();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
