using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StayCation.Repository;
using STAYCATION.Data;
//using StayCation.Areas.Identity.Data;
//using StayCation.Data;
var builder = WebApplication.CreateBuilder(args);
//var connectionString = builder.Configuration.GetConnectionString("StayDBContextConnection") ?? throw new InvalidOperationException("Connection string 'StayDBContextConnection' not found.");

//builder.Services.AddDbContext<StayDBContext>(options => options.UseSqlServer(connectionString));

//builder.Services.AddDefaultIdentity<StayCationUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<StayDBContext>();
builder.Services.AddScoped<IRepository, Repository>();
// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<StayDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

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
