using System.Security.Claims;
using System.Text;
using Ecommerce.Business.src.Abstractions;
using Ecommerce.Business.src.Services;
using Ecommerce.Business.src.Shared;
using Ecommerce.Core.src.Abstractions;
using Ecommerce.WebAPI.src.Database;
using Ecommerce.WebAPI.src.Middleware;
using Ecommerce.WebAPI.src.Repository;
using Ecommerce.WebAPI.src.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<RouteOptions>(options => options.LowercaseUrls = true);
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMvc(options =>
{
    options.SuppressAsyncSuffixInActionNames = false;
});

// Add services to the container.
// declare services

builder.Services.AddScoped<ICategoryService, CategoryService>()
.AddScoped<ICategoryRepo, CategoryRepo>();


builder.Services.AddScoped<IOrderService, OrderService>()
.AddScoped<IOrderRepo, OrderRepo>();

builder.Services.AddScoped<IProductService, ProductService>()
.AddScoped<IProductRepo, ProductRepo>();

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

// Add database contect service
builder.Services.AddDbContext<DatabaseContext>(options => options.UseNpgsql());

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
});

var app = builder.Build();

app.UseMiddleware<ExceptionHandlerMiddleware>();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
