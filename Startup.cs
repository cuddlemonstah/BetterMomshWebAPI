using BetterMomshWebAPI.EFCore;
using BetterMomshWebAPI.Models;
using BetterMomshWebAPI.Models.Configuration;
using BetterMomshWebAPI.Utils.Services;
using BetterMomshWebAPI.Utils.TokenValidator;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Authentication:Issuer"],
                        ValidAudience = Configuration["Authentication:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Authentication:Key"]))
                    };
                });
        services.AddAuthorization();
        services.AddControllers();

        AuthenticationConfiguration authenticationConfiguration = new AuthenticationConfiguration();
        Configuration.Bind("Authentication", authenticationConfiguration);
        services.AddSingleton(authenticationConfiguration);

        services.AddSingleton<AccessTokenGenerator>();
        services.AddSingleton<TokenGenerator>();
        services.AddSingleton<RefreshTokenGenerator>();
        services.AddSingleton<RefreshTokenValidator>();

        services.AddScoped<DbHelper>();
        services.AddLogging(configure =>
        {
            configure.AddConsole(); // Add console logging
                                    // Add other logging providers if needed
        });

        services.AddDbContext<API_DataContext>(options =>
        {
            options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
        });

        services.AddCors(options =>
        {
            options.AddPolicy("AllowOrigin", builder =>
            {
                builder
                    .AllowAnyOrigin() // This allows requests from any origin
                    .AllowAnyHeader()
                    .AllowAnyMethod();
            });
        });
    }

    public void Configure(IApplicationBuilder app)
    {
        app.UseAuthentication();
        app.UseCors("AllowOrigin");
        app.UseRouting();
        app.UseAuthorization();
        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllerRoute(
                name: "default",
                pattern: "{controller=Auth}/{action=UserLogin}"
            );
        });
    }
}