using Microsoft.EntityFrameworkCore;
using TestTask.Common;
using TestTask.Data;
using TestTask.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(1440);
});

builder.Services.AddDbContext<TestTaskContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestTaskContext") ?? throw new InvalidOperationException("Connection string 'TestTaskContext' not found."))
    , ServiceLifetime.Singleton);

builder.Services.AddSingleton<ITestTaskService, TestTaskService>();

builder.Services.AddScoped<IPageHelper, PageHelper>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    var context = services.GetRequiredService<TestTaskContext>();
    context.Database.Migrate();

    var workerService = services.GetRequiredService<ITestTaskService>();
    workerService.InitializeCache();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseSession();

app.MapRazorPages();

app.Run();
