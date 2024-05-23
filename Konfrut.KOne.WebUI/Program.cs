using Konfrut.Business.SiteConfigurations;
using Konfrut.Business.Utilities;
using Konfrut.DataAccess.Konfrut.Abstract;
using Konfrut.DataAccess.Konfrut.Concrete;
using Konfrut.Entity.Concrete;
using Microsoft.AspNetCore.Identity;
using System.Globalization;
using System.Net;
var builder = WebApplication.CreateBuilder(args);
ConfigurationHelper.Initialize(Configuration: builder.Configuration);

#region Culture
var cultureInfo = new CultureInfo("tr-TR");
CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
#endregion

#region Contexts
InitialSystemRequirements.LoadContexts(builder.Services);
#endregion

#region Services
InitialSystemRequirements.LoadServices(builder.Services);
#endregion

#region Identity
builder.Services.AddIdentity<ApplicationUser, ApplicationRole>().AddEntityFrameworkStores<KonfrutContext>().AddDefaultTokenProviders();
#endregion

#region Authorization
builder.Services.AddAuthorization();
#endregion

#region Identity Option
builder.Services.Configure<IdentityOptions>(options =>
{
    //Password
    options.Password.RequireDigit = true;
    options.Password.RequireLowercase = true;
    options.Password.RequireUppercase = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireNonAlphanumeric = false;

    //Lockout
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
    options.Lockout.AllowedForNewUsers = true;

    //EmailConfirmed
    options.User.RequireUniqueEmail = true;
    options.SignIn.RequireConfirmedEmail = true;
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+/";
});
InitialSystemRequirements.IdentityOptions(builder.Services);
#endregion

#region Authentication
InitialSystemRequirements.CookieSettings(builder.Services);
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Identity/Account/Login";
    options.LogoutPath = "/Identity/Account/Logout";
    options.AccessDeniedPath = "/Error/403";
    options.SlidingExpiration = true;
    options.ExpireTimeSpan = TimeSpan.FromDays(3);
    options.Cookie = new CookieBuilder
    {
        HttpOnly = false,
        Name = ".Konfrut.Security.Cookie",
        SameSite = SameSiteMode.Strict
    };
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
        return Task.CompletedTask;
    };
});
#endregion



// Add services to the container.
builder.Services.AddControllersWithViews();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
     name: "areas",
     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
 );
    endpoints.MapDefaultControllerRoute();
});

#region Migrations
app.ApplyMigrations();
#endregion
// Seed Data
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var unitOfWork = services.GetRequiredService<IUnitOfWork>();
    await InitialSystemRequirements.InitializeAsync(unitOfWork);
}

app.Run();
