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
        services.AddEndpointsApiExplorer();
       // services.AddSwaggerGen();
    }

    public static void ConfigureApp(WebApplication app)
    {
        //app.UseSwagger();
       // app.UseSwaggerUI();
        app.UseRouting();
        app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
    }
}
