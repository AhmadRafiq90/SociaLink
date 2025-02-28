using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Builder;
using WebApplication1.Models;
using WebApplication1.DBcontext;
using Microsoft.AspNetCore.Authentication.Cookies; // Update this using directive to match your project's namespace
var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddRazorPages().AddRazorPagesOptions(options =>
//{
//	options.RootDirectory = "/Pages"; // Set the root directory of pages
//	options.Conventions.AddPageRoute("/approve_or_reject", ""); // Set your desired page as the default
//});
builder.Services.AddRazorPages();
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.WithOrigins("https://google.com",
                                              "https://drive.google.com")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod()
                                            .AllowAnyOrigin();
                      });

});

// Configure DbContext with SQL Server
builder.Services.AddDbContext<FASTSocietyManagementContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/Login";
		options.LogoutPath = "/Logout";
	});

builder.Services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseCors(MyAllowSpecificOrigins);

app.UseAuthorization();

app.MapRazorPages(); // Or MapControllerRoute(), depending on your project type

app.Run();