using Customers.MainApp.Services.Implementations;
using Customers.MainApp.Data;
using Customers.MainApp.Repositories;
using Customers.MainApp.Services;
using Microsoft.AspNetCore.HttpOverrides;
using System.ComponentModel.Design;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<CustomersContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CustomersCS")));
    
builder.Services.AddScoped<ICustomerService, CustomerService>();
builder.Services.AddScoped<ITrackingLogsService, TrackingLogsService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<IHttpService, HttpService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders =
                   ForwardedHeaders.XForwardedFor |
                   ForwardedHeaders.XForwardedProto
});

app.MapRazorPages();

app.Run();
