using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Api.Middleware;

public static class Auth
{
    public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, string key)
    {
        services.AddAuthentication(options => {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };

                // Intercepta a req para pegar o jwt enviado na url 
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // Acessar o header Authorization
                        var authorizationHeader = context.Request.Headers["Authorization"].ToString();
                        var accessToken = context.Request.Query["access_token"].ToString();

                        var path = context.HttpContext.Request.Path;
                        if (path.StartsWithSegments("/chathub") && !string.IsNullOrEmpty(accessToken))
                        {
                            // Remover o prefixo "Bearer " e obter apenas o token
                            context.Token = accessToken;
                        }
                        else
                        {
                            // Verificar se o header contém o prefixo "Bearer " e extrair o token
                            if (!string.IsNullOrEmpty(authorizationHeader) && authorizationHeader.StartsWith("Bearer "))
                            {
                                // Remover o prefixo "Bearer " e obter apenas o token
                                var token = authorizationHeader.Substring("Bearer ".Length).Trim();
                            
                                // Atribuir o token ao context.Token para que ele seja validado
                                context.Token = token;
                            }
                        }
                        
                        
                        return Task.CompletedTask;
                    }
                };
            });

        services.AddAuthorization();
        return services;
    }
}