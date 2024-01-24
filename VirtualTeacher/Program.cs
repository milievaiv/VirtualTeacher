using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.Extensions.Configuration;

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