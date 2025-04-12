using CourseManagement.Data;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews()
    .AddRazorRuntimeCompilation()
    .AddViewOptions(options =>
    {
        options.HtmlHelperOptions.ClientValidationEnabled = true;
    });

builder.Services.AddDbContext<CourseManagementDbContext>(options =>
   options.UseSqlServer(builder.Configuration.GetConnectionString("CourseManagementConnection")));


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Users/Login/Login"; // Đường dẫn đến trang đăng nhập
        options.LogoutPath = "/Users/Login/Logout"; // Đường dẫn đến trang đăng xuất
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // Thời gian hết hạn cookies
    });

builder.Services.AddRazorPages();
builder.Services.AddAuthorization();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapRazorPages();

app.UseAuthentication();
app.UseAuthorization();

// Map static assets
app.MapStaticAssets();

// Map area routes
app.MapControllerRoute(
   name: "areas",
   pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// Map default route
app.MapControllerRoute(
   name: "default",
   pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
