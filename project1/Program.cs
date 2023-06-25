using Microsoft.EntityFrameworkCore;
using project1.Data;
using project1.Repositories;

internal class Program
{
    private static void Main(string[] args)
    {
        var con = new AnimalDbContext();
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllersWithViews();

        builder.Services.AddDbContext<AnimalDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("Default")));

        builder.Services.AddScoped<IRepository, PetShopRepository>();

        var app = builder.Build();

        if (app.Environment.IsStaging() || app.Environment.IsProduction())
        {
            app.UseExceptionHandler("/Error/Index");
        }
        app.UseStaticFiles();
       
        app.MapControllerRoute("Default", "{controller=Home}/{action=Index}/{id?}");

        using (var scope = app.Services.CreateScope())
        {
            var ctx = scope.ServiceProvider.GetRequiredService<AnimalDbContext>();
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
        }

        app.Run();

    }
}