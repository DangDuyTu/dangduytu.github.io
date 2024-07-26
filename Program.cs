using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using RoadTrafficManagement.AppModels;
using RoadTrafficManagement.Middlewares;
using RoadTrafficManagement.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddConsole();

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<QuanLyGiaoThongContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var connectionString = builder.Configuration.GetConnectionString("MySqlConnection");
var serverVersion = new MySqlServerVersion(new Version(8, 0, 30));

builder.Services.AddDbContext<QuanLyGiaoThongContext>(options =>
    options.UseMySql(connectionString, serverVersion));

//builder.Services.AddDbContext<QuanLyGiaoThongContext>(options =>
//    options.UseSqlServer(connectionString));

builder.Services.AddScoped<RoadRepository>();
builder.Services.AddScoped<RoadPropertyRepository>();

//.WithOrigins("http://localhost:xxxx", "https://localhost:xxxx", "http://192.168.1.15:xxxx", "https://192.168.1.15:xxxx")

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Cấu hình Kestrel
//builder.WebHost.ConfigureKestrel(options =>
//{
//    options.ListenAnyIP(7157, listenOptions =>
//    {
//        listenOptions.UseHttps("Certificate/mycert.pfx", "mypassword");
//    });
//});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

using (var scope = app.Services.CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    await SeedData(userManager, roleManager);
}

app.UseMiddleware<DbContextMiddleware>();
app.UseMiddleware<CustomLoggingMiddleware>();

app.UseCors("AllowAll");
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    Console.WriteLine($"{context.Request.Method} {context.Request.Path}");
    await next.Invoke();
});

app.Run("http://*:5000");

async Task SeedData(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
{
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    var adminUser = new ApplicationUser
    {
        UserName = "Admin",
        Email = "admin@example.com",
        FullName = "Administrator"
    };

    if (await userManager.FindByNameAsync(adminUser.UserName) == null)
    {
        var result = await userManager.CreateAsync(adminUser, "Admin@123456");
        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(adminUser, "Admin");
        }
    }
}
