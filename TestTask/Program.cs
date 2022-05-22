using Microsoft.EntityFrameworkCore;
using TestTask.Data;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages(); 

builder.Services.AddDbContext<TestTaskContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestTaskContext") ?? throw new InvalidOperationException("Connection string 'TestTaskContext' not found.")));

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<TestTaskContext>();
    context.Database.Migrate();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
