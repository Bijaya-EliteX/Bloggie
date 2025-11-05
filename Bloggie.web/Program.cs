using Bloggie.web.Data;
using Bloggie.web.Repositories;
using Bloggie.Web.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BloggiedbContext>(options=>
options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieDbConnectionString")));

builder.Services.AddDbContext<AuthDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("BloggieAuthDbConnectionString")));

//crucial bridge between Identity system and our Database
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<AuthDbContext>();



// Configure Identity Password Options (19:50)
builder.Services.Configure<IdentityOptions>(options =>
{
    
    options.Password.RequireDigit = true; 
    options.Password.RequireLowercase = true; 
    options.Password.RequireNonAlphanumeric = true; 
    options.Password.RequireUppercase = true; 
    options.Password.RequiredLength = 6; 
    options.Password.RequiredUniqueChars = 1; 
});

//We Register the ITagTepository here
builder.Services.AddScoped<ITagRepository, TagRepository>();
// NEW: Registering the IBlogPostRepository
builder.Services.AddScoped<IBlogPostRepository, BlogPostRepository>();
// Registering the IImageRepository with CloudinaryImageRepository implementation
builder.Services.AddScoped<IImageRepository, CloudinaryImageRepository>();


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
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
