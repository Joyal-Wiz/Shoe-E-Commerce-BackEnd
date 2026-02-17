using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace ECommerce.API.Extensions
{
    public static class JwtAuthenticationExtensions
    {
        public static IServiceCollection AddJwtAuthentication(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            var jwtKey = jwtSettings["SecretKey"];
            var jwtIssuer = jwtSettings["Issuer"];
            var jwtAudience = jwtSettings["Audience"];

            if (string.IsNullOrEmpty(jwtKey))
                throw new Exception("JWT SecretKey is missing in configuration.");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = jwtIssuer,
                        ValidAudience = jwtAudience,

                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes(jwtKey))
                    };

                    options.Events = new JwtBearerEvents
                    {
                        OnChallenge = async context =>
                        {
                            context.HandleResponse();
                            context.Response.StatusCode = 401;
                            context.Response.ContentType = "application/json";

                            await context.Response.WriteAsJsonAsync(new
                            {
                                success = false,
                                message = "Unauthorized. Please provide a valid token."
                            });
                        },

                        OnForbidden = async context =>
                        {
                            context.Response.StatusCode = 403;
                            context.Response.ContentType = "application/json";

                            await context.Response.WriteAsJsonAsync(new
                            {
                                success = false,
                                message = "Forbidden. You do not have permission to access this resource."
                            });
                        }
                    };
                });

            return services;
        }
    }
}
