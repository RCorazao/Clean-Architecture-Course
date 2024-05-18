using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tarker.Booking.Application.External.GetTokenJwt;
using Tarker.Booking.External.GetTokenJwt;


namespace Tarker.Booking.External
{
    public static class DependencyInjectionService
    {
        public static IServiceCollection AddExternal(this IServiceCollection services, 
            IConfiguration configuration)
        {
            services.AddSingleton<IGetTokenJwtService, GetTokenJwtService>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
            {
                option.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecretKeyJwt"] ?? string.Empty)),
                    ValidIssuer = configuration["IssuerJwt"],
                    ValidAudience = configuration["AudienceJwt"]
                };
            });
            return services;
        }
    }
}
