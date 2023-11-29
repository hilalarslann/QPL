using QPL.BL;
using QPL.DAL;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddQplBusinessLayer();
builder.Services.AddQplDataAccessLayer(builder.Configuration.GetValue<string>("ConnectionStrings:DbConnect") ?? "");

//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = AzureADDefaults.AuthenticationScheme;
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//})
// .AddAzureAD(options => builder.Configuration.Bind("AzureAd", options))
//.AddCookie(CookieAuthenticationDefaults.AuthenticationScheme);

//Kontrol 
//.AddCookie(options =>
// {
//     options.ExpireTimeSpan = TimeSpan.FromMinutes(1); // 30 dakika olarak ayarlanabilir.
//     options.SlidingExpiration = true;
// })


//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
//}).AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
//.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options => builder.Configuration.Bind("AzureAd", options
//));


//builder.Services.ConfigureApplicationCookie(options =>
//    {
//        options.Cookie = new()
//        {
//            Name = "IdentityCookie",
//            HttpOnly = true,
//            SameSite = SameSiteMode.Lax,
//            SecurePolicy = CookieSecurePolicy.Always
//        };
//        options.SlidingExpiration = false;
//        options.ExpireTimeSpan = TimeSpan.FromMinutes(1);
//    });


builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
}).AddCookie(options =>
{
    //options.Cookie.Name = "QPL.Auth";
    options.AccessDeniedPath = "/YetkiHatasi/Index";
    //Çerezlerin ne kadar süre boyunca geçerli olacağını belirtir. 
    //options.ExpireTimeSpan = TimeSpan.FromMinutes(1); // 30 dakika olarak ayarlanabilir.
    //Çerez kullanıcının etkileşimde bulunduğu her seferde süresinin sıfırlanıp sıfırlanmayacağını belirtir.
    //options.SlidingExpiration = false;
})
.AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options => builder.Configuration.Bind("AzureAd", options
));

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromSeconds(20);
});


//builder.Services.Configure<CookiePolicyOptions>(options =>
//{
//    options.MinimumSameSitePolicy = SameSiteMode.None;
//    options.CheckConsentNeeded = context => true;
//    options.MinimumSameSitePolicy = SameSiteMode.None;

//    options.OnAppendCookie = cookieContext =>
//    {
//        cookieContext.CookieOptions.Expires = DateTime.Now.AddMinutes(1);
//    };

//    options.OnDeleteCookie = cookieContext =>
//    {
//        cookieContext.CookieOptions.Expires = DateTime.Now.AddMinutes(1);
//    };
//});


//builder.Services.AddRazorPages(options =>
//{
//    options.Conventions.AuthorizePage("/YetkiHatasi/Index");
//});

//builder.Services.AddRazorPages(options =>
//{
//    options.Conventions.AddPageRoute("/Index", "/YetkiHatasi/Index");
//});

builder.Services.AddControllersWithViews().AddNewtonsoftJson();

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

app.UseStatusCodePagesWithReExecute("/YetkiHatasi/Index");
app.UseExceptionHandler("/YetkiHatasi/Index");


app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.UseSession();

if (app.Environment.IsDevelopment())
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=DetailedSearch}/{action=Index}/{id?}").AllowAnonymous();
else
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=DetailedSearch}/{action=Index}/{id?}");


app.Run();
