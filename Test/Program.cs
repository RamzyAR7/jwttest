using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using System.Text;
using Test.Contexts;
using Test.Entities;
using Test.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<UserDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IHelperRepository, HelperRepository>();
var jwt = builder.Configuration.GetSection("jwt").Get<Jwt>();
builder.Services.AddSingleton(jwt);
builder.Services.AddAuthentication()
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, Options =>
    {
        Options.SaveToken = true;
        Options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = jwt.Issuer,
            ValidAudience = jwt.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwt.SiningKey))
        };
    });

Action<SqlServerDbContextOptionsBuilder>? UseSqlServer(string v)
{
    throw new NotImplementedException();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
app.MapGet("/", context =>
{
    context.Response.Redirect("/scalar");
    return Task.CompletedTask;
});
app.UseAuthorization();

app.MapControllers();
 
app.Run();
