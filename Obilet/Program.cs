using Business;
using Infrastructure.Concretes;
using Infrastructure.Interfaces;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        var Configuration = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .AddEnvironmentVariables()
            .Build();

        builder.Services.AddScoped<IApiCaller, ApiCaller>();
        builder.Services.AddScoped<IBusJourneyService, BusJourneyService>();
        builder.Services.AddScoped<IBusinessHelper, BusinessHelper>();
        builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
        builder.Services.AddAutoMapper(typeof(MapperProfile));
        builder.Services.AddSession();
        builder.Services.AddHttpContextAccessor();
        var app = builder.Build();


        // Configure the HTTP request pipeline.
        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
        }
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthorization();

        app.UseSession();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.Run();
    }
}