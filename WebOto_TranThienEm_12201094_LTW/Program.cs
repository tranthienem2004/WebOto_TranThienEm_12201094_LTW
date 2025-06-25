using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using WebOto_TranThienEm_12201094_LTW.Models;
using WebOto_TranThienEm_12201094_LTW.Models.Data;
using WebOto_TranThienEm_12201094_LTW.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<CarDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<User>(options => {
    options.SignIn.RequireConfirmedAccount = false;
    options.Password.RequiredLength = 6;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
})
.AddEntityFrameworkStores<CarDbContext>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddScoped<ICarService, CarService>();

builder.Services.AddLogging();

builder.Services.AddRazorPages();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();

app.UseStaticFiles(new StaticFileOptions
{
    OnPrepareResponse = ctx =>
    {
        if (ctx.Context.Request.Path.StartsWithSegments("/images"))
        {
            ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=86400");
        }
        else
        {
            ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=3600");
        }
    },
    ServeUnknownFileTypes = false,
    DefaultContentType = "application/octet-stream"
});

if (app.Environment.IsDevelopment())
{
    app.UseDirectoryBrowser(new DirectoryBrowserOptions
    {
        FileProvider = new PhysicalFileProvider(
            Path.Combine(builder.Environment.WebRootPath, "images")),
        RequestPath = "/images"
    });
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();