using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api.Middleware;

public static class Auth
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, string key)
    {
        var keyBytes = Encoding.UTF8.GetBytes(key);
        var symmetricKey = new SymmetricSecurityKey(keyBytes);

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = symmetricKey,

                    ValidateIssuer = false,
                    // ValidIssuer = "https://correct-magpie-48.clerk.accounts.dev",

                    ValidateAudience = false,
                    // ValidAudience = "theflags.app",

                    ValidateLifetime = false,
                    // ClockSkew = TimeSpan.FromMinutes(5)
                };

                
                // ESSENCIAL PRO SIGNALR VIA ?access_token=
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];
                        var path = context.HttpContext.Request.Path;

                        // ATENÇÃO: tem que bater com o MapHub("/chatHub")
                        if (!string.IsNullOrEmpty(accessToken) &&
                            path.StartsWithSegments("/chathub", StringComparison.OrdinalIgnoreCase))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    },
                    
                    OnAuthenticationFailed = context =>
                    {
                        var loggerFactory = context.HttpContext.RequestServices.GetRequiredService<ILoggerFactory>();
                        var logger = loggerFactory.CreateLogger("JwtAuth");

                        logger.LogError(context.Exception, "JWT authentication failed");

                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization();

        return services;
    }
}