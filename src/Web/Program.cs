using Core.Interfaces;
using Core.Services;
using Data.Context;
using Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

#region Context
// Add db context to the container
builder.Services.AddDbContextPool<ILearnDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("SqlServer"))
);

// Add identity context
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ILearnDbContext>()
    .AddDefaultTokenProviders();
#endregion

#region IOC
// Add email sender service to the container
builder.Services.AddScoped<IEmailSender, EmailSender>();


#endregion

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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
