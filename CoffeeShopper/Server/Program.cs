using IdentityServer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Server.Contexts;

namespace Server
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var seed = args.Contains("/seed");

            if (seed)
            {
                args = args.Except(new[] { "/seed" }).ToArray();
            }

            var builder = WebApplication.CreateBuilder(args);

            var assembly = typeof(Program).Assembly.GetName().Name;
            string defaultConnString = builder.Configuration.GetConnectionString("Default")!;

            if (seed)
            {
                SeedData.EnsureSeedData(defaultConnString);
            }

            builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
                options.UseSqlServer(defaultConnString, x => x.MigrationsAssembly(assembly)));

            //Add Identity
            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AspNetIdentityDbContext>();

            //register IdentityServer services
            builder.Services.AddIdentityServer()
                .AddAspNetIdentity<IdentityUser>()
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = dbOpts => dbOpts.UseSqlServer(defaultConnString,
                        opt => opt.MigrationsAssembly(assembly));
                })
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = dbOpts => dbOpts.UseSqlServer(defaultConnString,
                        opt => opt.MigrationsAssembly(assembly));
                })
                .AddDeveloperSigningCredential();

            builder.Services.AddControllersWithViews();

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.Run();
        }
    }
}
