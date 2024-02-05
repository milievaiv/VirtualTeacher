using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using VirtualTeacher.Data;
using VirtualTeacher.Helpers;
using VirtualTeacher.Helpers.Contracts;
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

            // Configuration
            string smtpSettingsFilePath = "C:\\Google Keys\\smtpsettings.json";
            var smtpSettingsConfiguration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(smtpSettingsFilePath, optional: false, reloadOnChange: true)
                .Build();
            builder.Services.Configure<SmtpSettings>(smtpSettingsConfiguration);

            // Services
            builder.Services.AddTransient<IEmailService, EmailService>();
            builder.Services.AddTransient<ITeacherCandidateService, TeacherCandidateService>();

            builder.Services.AddControllersWithViews()
                .AddNewtonsoftJson(options =>
                {
                    options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                });

            // Authentication
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

            // Authorization
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));
            });

            // Database
            builder.Services.AddDbContext<VirtualTeacherContext>(options =>
            {
                string connectionString = @"Data Source=127.0.0.1,1435;Initial Catalog=VirtualTeacher;User Id=sqlserver;Password=D?3F&>#(}HAmCOi%;";
                options.UseSqlServer(connectionString, b => b.MigrationsAssembly(typeof(VirtualTeacher.Data.VirtualTeacherContext).Assembly.FullName));
                options.EnableSensitiveDataLogging();
            });

            // Cloud Storage
            var bucketName = configuration["GoogleCloudStorage:BucketName"];
            builder.Services.AddScoped<CloudStorageService>(provider =>
            {
                var serviceAccountKeyPath = "C:\\Google Keys\\SA_key.json";
                return new CloudStorageService(serviceAccountKeyPath, bucketName);
            });

            // Repositories
            builder.Services.AddScoped<IUserRepository, UserRepository>();
            builder.Services.AddScoped<IStudentRepository, StudentRepository>();
            builder.Services.AddScoped<ITeacherRepository, TeacherRepository>();
            builder.Services.AddScoped<IAdminRepository, AdminRepository>();
            builder.Services.AddScoped<ICourseRepository, CourseRepository>();
            builder.Services.AddScoped<ICourseTopicRepository, CourseTopicRepository>();
            builder.Services.AddScoped<ILectureRepository, LectureRepository>();
            builder.Services.AddScoped<IAssignmentRepository, AssignmentRepository>();

            // Services
            builder.Services.AddScoped<IStudentService, StudentService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<ITeacherService, TeacherService>();
            builder.Services.AddScoped<ITeacherCandidateService, TeacherCandidateService>();
            builder.Services.AddScoped<ICourseService, CourseService>();
            builder.Services.AddScoped<ICourseTopicService, CourseTopicService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<ILectureService, LectureService>();
            builder.Services.AddScoped<IAssignmentService, AssignmentService>();


            // Authentication Services
            builder.Services.AddScoped<IRegistrationService, RegistrationService>();
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddScoped<IVerificationService, VerificationService>();

            // Helpers
            builder.Services.AddScoped<IModelMapper, ModelMapper>();

            // Swagger
            builder.Services.AddSwaggerGen(c =>
            {
                // Register ConflictingActionsResolver to handle conflicts
                c.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
            });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "VirtualTeacher API V1");
                options.RoutePrefix = "api/swagger";
            });

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
