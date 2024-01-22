using ChatNet.SignalR;
using Data;
using Data.Mapper;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Services.Repositories;
using Services.Services;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        builder.Services.AddControllersWithViews();
        builder.Services.AddDbContext<ChatContext>(
            o => o.UseSqlServer(builder.Configuration.GetConnectionString("conectionString"),
            options => options.MigrationsAssembly("ChatNet"))
            );

        builder.Services.AddScoped<UsuarioRepository<Usuario>, UsuarioService>();
        builder.Services.AddAutoMapper(typeof(ProfileMapper));
        builder.Services.AddScoped<SalaRepository<Sala>, SalaService>();
        builder.Services.AddScoped<mensajeRepository<Mensaje>, MensajeService>();

        builder.Services.AddSignalR(o => {
            o.EnableDetailedErrors = true;
        });

        builder.Services.AddDistributedMemoryCache();
        // Esto faltaba:  
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromSeconds(10);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

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

        app.UseSession();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapHub<ChatHub>("/Chat");

        app.Run();
    }
}