
//using MarkaziaBITStore.ApplicationDBContext;
//using MarkaziaBITStore.Services;
using MarkaziaBITStore.Application.ApplicationDBContext;
using MarkaziaBITStore.Application.ExtentionServices;
using MarkaziaBITStore.Application.Middleware;
using MarkaziaBITStore.Settings;
using MarkaziaWebCommon.Utils.Swagger;
using MarkaziaWebCommon.Utils.VLog;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.Text;

namespace MarkaziaBITStore;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddDbContext<BitStoreDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnectionVPN")));

        builder.AddVLog(new CreateLoggerParam
        {
            LogFilePath = "logs/log-.txt",
            LogEventLevel = LogEventLevel.Debug,
        });


        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //services to the container
        builder.Services.AddScopedServices();

        ////  JWT auth
        //var jwtSection = builder.Configuration.GetSection("Jwt");
        //builder.Services.Configure<JwtSettings>(jwtSection);
        //var jwtSettings = jwtSection.Get<JwtSettings>();
        //if (string.IsNullOrWhiteSpace(jwtSettings?.Key) || Encoding.UTF8.GetByteCount(jwtSettings.Key) < 32)
        //{
        //    throw new InvalidOperationException("JWT key is missing or too short. Provide Jwt:Key with >=32 bytes in appsettings.json.");
        //}

        //// Authentication: JWT Bearer
        //var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key));
        //builder.Services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //})
        //.AddJwtBearer(options =>
        //{
        //    options.RequireHttpsMetadata = false; // set true in prod
        //    options.SaveToken = true;
        //    options.TokenValidationParameters = new TokenValidationParameters
        //    {
        //        ValidateIssuer = true,
        //        ValidIssuer = jwtSettings.Issuer,
        //        ValidateAudience = true,
        //        ValidAudience = jwtSettings.Audience,
        //        ValidateIssuerSigningKey = true,
        //        IssuerSigningKey = signingKey,
        //        ValidateLifetime = true
        //    };
        //});

        //builder.Services.AddAuthorization();

        //// Swagger + JWT configuration
        //builder.Services.AddEndpointsApiExplorer();
        //builder.Services.AddSwaggerGen(c =>
        //{
        //    c.SwaggerDoc("v1", new OpenApiInfo { Title = "MarkaziaBITStore API", Version = "v1" });

        //    // add JWT Authentication to swagger
        //    var securityScheme = new OpenApiSecurityScheme
        //    {
        //        Name = "Authorization",
        //        Description = "Enter 'Bearer {token}' (without quotes). Example: Bearer eyJhbGciOi...",
        //        In = ParameterLocation.Header,
        //        Type = SecuritySchemeType.Http,
        //        Scheme = "bearer",
        //        BearerFormat = "JWT",
        //        Reference = new OpenApiReference
        //        {
        //            Type = ReferenceType.SecurityScheme,
        //            Id = "Bearer"
        //        }
        //    };
        //    c.AddSecurityDefinition("Bearer", securityScheme);
        //    var requirement = new OpenApiSecurityRequirement
        //    {
        //        { securityScheme, Array.Empty<string>() }
        //    };
        //    c.AddSecurityRequirement(requirement);
        //});


        //builder.Host.UseSerilog((ctx, lc) =>
        //lc.ReadFrom.Configuration(ctx.Configuration));

        builder.AddVSwaggerWebBuilder(new VSwaggerParam
        {

            EnableJWT = true,
            EnableApiKey = false,
            ProjectAssembly = typeof(Program).Assembly,
            SwaggerSetupAction = c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MarkaziaBITStore API", Version = "v1" });
            }
        });

        builder.Services.AddMemoryCache();

        var app = builder.Build();

        //app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MarkaziaBITStore API v1"));
            app.UseDeveloperExceptionPage();

        }
        else
        {
            // Use custom error handling for production
            app.UseExceptionHandler("/error");
        }

     

        app.UseMiddleware<ExceptionHandlingMiddleware>();

        app.UseHttpsRedirection();

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
