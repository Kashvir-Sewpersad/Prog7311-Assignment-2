
//----------------------------------------- Start of file ---------------------------------------------------//


/*
 
 Program.cs is the entry to the program and is responsible for start up operations 
 
 
 */
using Microsoft.EntityFrameworkCore;
using Prog7311_Assignment_2.Data;
using Prog7311_Assignment_2.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Add session support
builder.Services.AddDistributedMemoryCache();
    builder.Services.AddSession(options =>
    {
        options.IdleTimeout = TimeSpan.FromMinutes(30);
        options.Cookie.HttpOnly = true;
        options.Cookie.IsEssential = true;
    });

/*
 Add scope is used to create a single instance of each aspect 
 
 */
builder.Services.AddScoped<AuthService>(); // registering the authentication service (AUTH)

builder.Services.AddScoped<FarmerService>(); // Registering the farmer service 

builder.Services.AddScoped<ProductService>(); // Registering the Product service 

var app = builder.Build();

// Configure the HTTP request
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

//----------------------------------------------------  End of file ----------------------------------------//