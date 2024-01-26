using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using VirtualTeacher.Data;

namespace VirtualTeacher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            var bucketName = configuration["GoogleCloudStorage:BucketName"];

            builder.Services.AddScoped<CloudStorageService>(provider =>
            {
                // Provide the path to your service account key file
                var serviceAccountKeyPath = "SA_key.json";
                return new CloudStorageService(serviceAccountKeyPath, bucketName);
            });

            builder.Services.AddDbContext<VirtualTeacherContext>(options =>
            {
                string connectionString = @"Data Source=127.0.0.1,1435;Initial Catalog=VirtualTeacher;User Id=sqlserver;Password=D?3F&>#(}HAmCOi%;";
                //string connectionString = "Server=localhost;Database=Demo;Trusted_Connection=True;";
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(VirtualTeacher.Data.VirtualTeacherContext).Assembly.FullName));
                options.EnableSensitiveDataLogging();
            });


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}