using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using e_commerce.utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace e_commerce.DepandancyInjection
{
    public static class Security
    {
        public static void AddJwt(this IServiceCollection collection,byte[] key){
            collection.AddAuthentication(Options => {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                
                }).AddJwtBearer(
                    Options => {
                        Options.UseSecurityTokenValidators=true;
                        Options.TokenValidationParameters = new TokenValidationParameters{
                        ValidateIssuer = false, 
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                
                Options.Events = new JwtBearerEvents{
                    OnMessageReceived = context => {
                            context.Token = context.Request.Headers["Authorization"];
                        return Task.CompletedTask;
                    },

                    OnAuthenticationFailed = context =>
                        {
                            // Log the error
                            Debugging.print(context.Exception.ToString());
                            return context.Response.WriteAsync(context.Exception.ToString());
                        },
                        
                };
            });

        }
    }
}