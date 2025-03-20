using Microsoft.EntityFrameworkCore;
using static ResumeApp.Model.ResumeModel;

public  class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        ConfigureServices(builder.Services);
        var app = builder.Build();
        ConfigureApp(app);
        app.Run();
    }

    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(opt => opt.UseInMemoryDatabase("ResumeDb"));
        services.AddControllers();
         services.AddControllersWithViews(); // Enable MVC views
        services.AddRazorPages(); // Enable Razor Pages
        services.AddEndpointsApiExplorer();
    }

    public static void ConfigureApp(WebApplication app)
    {
        app.UseRouting();
        
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            endpoints.MapRazorPages();
        });
    }
}
