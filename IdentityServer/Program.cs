using IdentityServer.Context;
using IdentityServer.Model;
using IdentityServer.Model.Model;
using IdentityServer.Pages.TwoFactorAuthentication;
using IdentityServer.Servises;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddDbContext<ApplicationDB>
    (opt => opt.UseSqlServer( builder.Configuration.GetConnectionString("DefaoultConnection"))) ;
builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDB>().AddDefaultTokenProviders();

builder.Services.AddSingleton<ResetPassword>();
builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.Configure<IdentityOptions>(opt =>
{
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequireLowercase = false;  
    opt.Password.RequireUppercase = false;  
    opt.Password.RequireDigit = false;  
    opt.Lockout.MaxFailedAccessAttempts = 5;    
    opt.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(30);     
    opt.SignIn.RequireConfirmedEmail = false;      
    
});
builder.Services.ConfigureApplicationCookie(options => 
{
    options.AccessDeniedPath = new PathString("/Shared/AccessDeniedPage");
});
builder.Services.Configure<Smptsetting>(builder.Configuration.GetSection("SMTP"));
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
