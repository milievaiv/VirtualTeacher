using Google.Apis.Auth.AspNetCore3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using VirtualTeacher.Data;
using Newtonsoft.Json;
using VirtualTeacher.Services.Contracts;
using VirtualTeacher.Services;
using VirtualTeacher.Repositories.Contracts;
using VirtualTeacher.Repositories;

namespace VirtualTeacher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddControllersWithViews()
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            });

            builder.Services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
           .AddJwtBearer(options =>
           {
               options.SaveToken = true;
               options.TokenValidationParameters = new TokenValidationParameters
               {
                   ValidateIssuer = false,
                   ValidateAudience = false,
                   ValidateLifetime = true,
                   ValidateIssuerSigningKey = true,
                   IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JwtConfig:Secret"]))
               };
           });

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

            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();

            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IAdminService, AdminService>();

            builder.Services.AddScoped<IRegistrationService, RegistrationService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IVerificationService, VerificationService>();

            builder.Services.AddSwaggerGen(c =>
            {
                // Register ConflictingActionsResolver to handle conflicts
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            var app = builder.Build();

            app.UseSwagger();
            //app.UseSession();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "VirtualTeacher API V1");
                options.RoutePrefix = "api/swagger";
            });

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}