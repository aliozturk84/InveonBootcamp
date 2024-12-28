using InveonBootcamp.DataAccess.Abstract;
using InveonBootcamp.DataAccess.Repositories.EntityFramework;
using InveonBootcamp.Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using InveonBootcamp.DataAccess.Concrete;
using InveonBootcamp.Business.Abstract;
using InveonBootcamp.Business.Concrete;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using InveonBootcamp.DataAccess;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

var tokenOptions = builder.Configuration.GetSection("Token");

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = tokenOptions["Issuer"],
            ValidAudience = tokenOptions["Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOptions["SecurityKey"])),
            ClockSkew = TimeSpan.Zero // Token süre dolumu gecikmesini sýfýrla
        };
    });

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"));
});

builder.Services.AddIdentity<User, Role>().AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddScoped<ICourseService, CourseManager>();
builder.Services.AddScoped<IOrderService, OrderManager>();
builder.Services.AddScoped<IPaymentService, PaymentManager>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICourseDal, EfCoreCourseDal>();
builder.Services.AddScoped<IPaymentDal, EfCorePaymentDal>();
builder.Services.AddScoped<IOrderDal, EfCoreOrderDal>();

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "InveonBootcamp API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\""
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<Role>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<User>>();

    // Kullanýcý ve rol seed iþlemleri
    await DataInitializer.SeedRolesAndUsersAsync(roleManager, userManager);
}

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
