using Azure.Data.Tables;
using Azure.Data.Tables.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RecipeSharingProject.API;
using RecipeSharingProject.Business;
using RecipeSharingProject.Common.Interfaces;
using RecipeSharingProject.Common.Model;
using RecipeSharingProject.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
DIConfiguration.RegisterService(builder.Services);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IGenericRepository<Recipe>, GenericRepository<Recipe>>();
builder.Services.AddScoped<IUploadService, AzureBlobUploadService>();
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
    dbcontext.Database.EnsureCreated();
}

app.UseMiddleware<ExceptionMiddleware>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
