using BetsoCare.Core.Entities;
using BetsoCare.Core.Interfaces;
using BetsoCare.Infrastructure.Data;
using BetsoCare.Infrastructure.DataSeed;
using BetsoCare.Infrastructure.Services;
using BetsoCare.Repository;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Text.Json;
using Hangfire;
using Hangfire.SqlServer;

namespace BetsoCareSystem
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // ================= CONTROLLERS =================

            builder.Services.AddControllers();

            // ================= DATABASE =================

            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.GetConnectionString(
                        "DefaultConnection")));

            // ================= REPOSITORIES =================

            builder.Services.AddScoped<IArticleRepository, ArticleRepository>();
            builder.Services.AddScoped<IClinicRepository, ClinicRepository>();
            builder.Services.AddScoped<IShelterRepository, ShelterRepository>();

            builder.Services.AddScoped<IVaccineRepository, VaccineRepository>();
            builder.Services.AddScoped<ILocationRepository, LocationRepository>();

            // ================= SERVICES =================

            builder.Services.AddScoped<IAuthService, AuthService>();

            builder.Services.AddScoped<TokenService>();
            builder.Services.AddScoped<EmailService>();
            builder.Services.AddScoped<ReportService>();
            builder.Services.AddScoped<IVaccineService, VaccineService>();
            builder.Services.AddScoped<ILocationService, LocationService>();
            builder.Services.AddScoped<AppNotificationService>();
            builder.Services.AddScoped<VaccineReminderJob>();
            builder.Services.AddScoped<IVideoService, VideoService>();

            builder.Services.AddHttpClient<IAiService, AiService>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddMemoryCache();
            builder.Services.AddHttpClient();

            // ================= HANGFIRE =================

            builder.Services.AddHangfire(config =>
                config.UseSqlServerStorage(
                    builder.Configuration.GetConnectionString(
                        "DefaultConnection")));

            builder.Services.AddHangfireServer();

            // ================= CORS =================

            var allowedOrigins = new[]
{
    "http://localhost:3000",
    "https://petso-care-ones.vercel.app"
};

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowFrontend", policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyHeader()
                          .AllowAnyMethod()
                          .AllowCredentials();
                });
            });

            // ================= AUTHENTICATION =================

            builder.Services
                .AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme =
                        JwtBearerDefaults.AuthenticationScheme;

                    options.DefaultChallengeScheme =
                        GoogleDefaults.AuthenticationScheme;

                    options.DefaultSignInScheme =
                        CookieAuthenticationDefaults.AuthenticationScheme;
                })

                .AddCookie()

                .AddJwtBearer(options =>
                {
                    var key = Encoding.UTF8.GetBytes(
                        builder.Configuration["JWT:Key"]);

                    options.TokenValidationParameters =
                        new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,

                            ValidIssuer =
                                builder.Configuration["JWT:Issuer"],

                            ValidAudience =
                                builder.Configuration["JWT:Audience"],

                            IssuerSigningKey =
                                new SymmetricSecurityKey(key)
                        };
                })

                .AddGoogle(options =>
                {
                    options.ClientId =
                        builder.Configuration["Google:ClientId"];

                    options.ClientSecret =
                        builder.Configuration["Google:ClientSecret"];

                    // ✅ مهم جدًا لحل redirect_uri_mismatch
                    options.CallbackPath = "/signin-google";
                });

            // ================= AUTHORIZATION =================

            builder.Services.AddAuthorization();

            // ================= SWAGGER =================

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition(
                    "Bearer",
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                    {
                        Name = "Authorization",
                        Type =
                            Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                        Scheme = "bearer",
                        BearerFormat = "JWT",
                        In =
                            Microsoft.OpenApi.Models.ParameterLocation.Header
                    });

                options.AddSecurityRequirement(
    new Microsoft.OpenApi.Models.OpenApiSecurityRequirement { 
                        {
                            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                            {
                                Reference =
                                    new Microsoft.OpenApi.Models.OpenApiReference
                                    {
                                        Type =
                                            Microsoft.OpenApi.Models.ReferenceType
                                                .SecurityScheme,
                                        Id = "Bearer"
                                    }
                            },
                            Array.Empty<string>()
                        }
                    });
            });

            var app = builder.Build();

            // ================= SWAGGER =================

            app.UseSwagger();
            app.UseSwaggerUI();

            // ================= DATABASE SEED =================

            using (var scope = app.Services.CreateScope())
            {
                var context =
                    scope.ServiceProvider
                        .GetRequiredService<ApplicationDbContext>();

                await ArticleSeeder.SeedAsync(context);
                await ClinicSeeder.SeedAsync(context);
                await LocationSeeder.SeedAsync(context);

                if (!context.Shelters.Any())
                {
                    var jsonPath = Path.Combine(
                        Directory.GetCurrentDirectory(),
                        "Infrastructure",
                        "DataSeed",
                        "shelters.json");

                    if (File.Exists(jsonPath))
                    {
                        var json = File.ReadAllText(jsonPath);

                        var shelters =
                            JsonSerializer.Deserialize<List<Shelter>>(json);

                        if (shelters != null)
                        {
                            context.Shelters.AddRange(shelters);

                            context.SaveChanges();
                        }
                    }
                }
            }

            // ================= HANGFIRE DASHBOARD =================

            app.UseHangfireDashboard();

            RecurringJob.AddOrUpdate<VaccineReminderJob>(
                "vaccine-reminder",
                job => job.Execute(),
                Cron.Daily);

            // ================= MIDDLEWARE =================

            app.UseHttpsRedirection();

            app.UseCors("AllowFrontend");

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseStaticFiles();

            app.MapControllers();

            app.Run();
        }
    }
}