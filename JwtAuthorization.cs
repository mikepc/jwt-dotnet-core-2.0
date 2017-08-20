namespace Site.Authorization
{

    using System;
    using System.Collections.Generic;
    using System.IO;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Net.Http.Headers;
    using Newtonsoft.Json.Linq;
    using Microsoft.IdentityModel.Tokens;
    using System.Text;
    using System.Threading.Tasks;

    public static class JwtAuthorizationExtensions
    {
        public static IServiceCollection AddJwtAuthorization(this IServiceCollection services)
        {
                    var signingKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Environment.GetEnvironmentVariable("JWT_SECRET")));
     
                    var tokenValidationParameters = new TokenValidationParameters
                    {
                        // The signing key must match!
                        ValidateIssuerSigningKey = true,
                        ValidateAudience = false,
                        ValidateIssuer = false,
                        IssuerSigningKeys = new List<SecurityKey>{ signingKey },
                        

                        // Validate the token expiry
                        ValidateLifetime = false,
                        ClockSkew = TimeSpan.Zero
                        
                    };
     

                    services.AddAuthentication(options =>
                    {
                        
                        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                        
                        
                    })
                    .AddJwtBearer(o =>
                    {
                        
                        o.IncludeErrorDetails = true;
                   
                        o.TokenValidationParameters  = tokenValidationParameters;
                        o.Events = new JwtBearerEvents()
                        {
                            OnAuthenticationFailed = c =>
                            {
                                c.NoResult();
                                System.Diagnostics.Debug.WriteLine("Failed to authenticate");
                                c.Response.StatusCode = 401;
                                c.Response.ContentType = "text/plain";

                                return c.Response.WriteAsync(c.Exception.ToString());
                            }
                            
                        };
                    });

                    return services;
        }
    }

}