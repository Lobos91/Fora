
global using Fora.UI.Models;
using Fora.UI.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
builder.Services.AddRazorPages();



var connectionString3 = builder.Configuration.GetConnectionString("AuthConnection");
builder.Services.AddDbContext<AuthDbContextUI>(options => options.UseSqlServer(connectionString3));
builder.Services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<AuthDbContextUI>();


var connectionString4 = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AppDbContextUI>(options => options.UseSqlServer(connectionString4));

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
