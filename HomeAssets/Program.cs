using HomeAssets.Domain.Interfaces;
using HomeAssets.Domain.Models;
using HomeAssets.Domain.Services;
using HomeAssets.Infraestructure.DataBaseContext;
using HomeAssets.Infraestructure.ExtendedIdentity;
using HomeAssets.Infraestructure.Extensions;
using HomeAssets.Infraestructure.Repositories;
using HomeAssets.Security;
using Microsoft.AspNetCore.Identity;

/* Create Builder */
var builder = WebApplication.CreateBuilder(args);

/* Register Services */
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(builder =>
    {
        builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

builder.Services.Configure<SmtpSettings>(builder.Configuration.GetSection("SmtpSettings"));

builder.Services.Configure<DataProtectionTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromMinutes(10);
});

builder.Services.Configure<CustomEmailConfirmationTokenProviderOptions>(options =>
{
    options.TokenLifespan = TimeSpan.FromDays(2);
});

builder.Services.RegisterSqlServerDatabaseContext(builder.Configuration.GetConnectionString("HomeAssetsDB"));

builder.Services.AddIdentity<App_IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = true;

    options.SignIn.RequireConfirmedEmail = true;

    options.Tokens.EmailConfirmationTokenProvider = "CustomEmailConfirmationTokenProvider";

    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
}).AddEntityFrameworkStores<AppDbContext>()
  .AddDefaultTokenProviders()
  .AddTokenProvider<CustomEmailConfirmationTokenProvider<App_IdentityUser>>("CustomEmailConfirmationTokenProvider");

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication()
    .AddGoogle(options =>
    {
        options.ClientId = "{Google-API-Client-Id}";
        options.ClientSecret = "{Google-API-Client-Secret}";
    });

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AccountManagers", policy => policy.RequireClaim("Role", new string[] { "admin1" }));
    options.AddPolicy("AccountViewers", policy => policy.RequireClaim("Role", new string[] { "admin2", "admin1" }));
    options.AddPolicy("ServiceManagers", policy => policy.RequireClaim("Role", new string[] { "user1", "admin2", "admin1" }));
    options.AddPolicy("ServiceViewers", policy => policy.RequireClaim("Role", new string[] { "user2", "user1", "admin2", "admin1" }));
});

builder.Services.AddScoped<IHomeServiceRepository, HomeServiceRepository>();
builder.Services.AddScoped<IAuthorizedEmailRepository, AuthorizedEmailRepository>();
builder.Services.AddTransient<IMailService, EmailService>();
builder.Services.AddSingleton<DataProtectionPurposeStrings>();

/* Build Application */
WebApplication app = builder.Build();

/* Define Pipeline */
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseStatusCodePagesWithReExecute("/Error/{0}");
}

app.UseCors(builder =>
{
    builder.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod();
});

app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

/* Let's go! */
app.Run();