using System.Drawing;
using System.Security.Claims;
using System.Text;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.Services;
using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.Core.src.Entities;
using Ecommerce.WebAPI.src.Authorization;
using Ecommerce.WebAPI.src.Database;
using Ecommerce.WebAPI.src.Middleware;
using Ecommerce.WebAPI.src.Repository;
using Ecommerce.WebAPI.src.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;
using Swashbuckle.AspNetCore.Filters;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
        policy =>
        {
            policy.AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(
    options =>
    {
        options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
        {
            Description = "Bearer token authentication",
            Name = "Authorization", // --> this should be "Authorization", because in the request header, "Authentication" is not the right Property name
            In = ParameterLocation.Header,
            Scheme = "Bearer"
        }
        );
        options.OperationFilter<SecurityRequirementsOperationFilter>();
    }
);

//builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

// Add services to the container.
// declare services

builder.Services.AddScoped<ICategoryService, CategoryService>()
.AddScoped<ICategoryRepo, CategoryRepo>();

builder.Services.AddScoped<IProductLineService, ProductLineService>()
.AddScoped<IProductLineRepo, ProductLineRepo>();

builder.Services.AddScoped<IOrderService, OrderService>()
.AddScoped<IOrderRepo, OrderRepo>();

builder.Services.AddScoped<IReviewService, ReviewService>()
.AddScoped<IReviewRepo, ReviewRepo>();

builder.Services.AddScoped<IProductService, ProductService>()
.AddScoped<IProductRepo, ProductRepo>();

builder.Services.AddScoped<IProductSizeService, ProductSizeService>()
.AddScoped<IProductSizeRepo, ProductSizeRepo>();

builder.Services.AddScoped<IAvatarService, AvatarService>()
.AddScoped<IAvatarRepo, AvatarRepo>();

builder.Services.AddScoped<IUserService, UserService>()
.AddScoped<ITokenService, TokenService>()
.AddScoped<IAuthService, AuthService>()
.AddScoped<IUserRepo, UserRepo>();

//add automapper dependency injection
// builder.Services.AddAutoMapper(typeof(Program).Assembly);
builder.Services.AddAutoMapper(typeof(MapperProfile).Assembly);

// Error handler middleware
// builder.Services.AddScoped<ExceptionHandlerMiddleware>();
builder.Services.AddTransient<ExceptionHandlerMiddleware>();


var connectionString = builder.Configuration.GetConnectionString("ElephantDb");
var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionString);
dataSourceBuilder.MapEnum<Role>();
dataSourceBuilder.MapEnum<OrderStatus>();
var dataSource = dataSourceBuilder.Build();


// Add database contect service
builder.Services.AddDbContext<DatabaseContext>(options =>
{
    options.UseNpgsql(dataSource)
         .UseSnakeCaseNamingConvention()
         .AddInterceptors(new TimeStampInterceptor())
                  .EnableSensitiveDataLogging()
                  .EnableDetailedErrors(); ;
});

// Config authentication
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
    {
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true
        };
    });

builder.Services.AddAuthorization(policy =>
{
    //policy.AddPolicy("SuperAdmin", policy => policy.RequireClaim(ClaimTypes.Email, "sunil@mail.com"));
    policy.AddPolicy("Admin", policy => policy.RequireRole(ClaimTypes.Role, "Admin"));
    policy.AddPolicy("Customer", policy => policy.RequireRole(ClaimTypes.Role, "Customer"));
    policy.AddPolicy("AdminOrOwner", policy => policy.Requirements.Add(new AdminOrOwnerRequirement()));
});

//AUthorization handler
builder.Services.AddSingleton<IAuthorizationHandler, AdminOrOwnerHandler>();

var app = builder.Build();

app.UseCors();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
/*
if (app.Environment.IsDevelopment())
{
  
}
*/
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
