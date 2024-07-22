using System.Text;
using e_commerce.Configurations;
using e_commerce.Domain.Repositories;
using e_commerce.Middlewares;
using e_commerce.utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using Microsoft.Extensions.Options;
using e_commerce.Services;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories();
builder.Services.AddServices();

var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("PrivateKey"));

builder.Services.AddAuthentication(Options => {
    Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(
    Options =>{
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
            
          
                
            if (context.Request.Headers.ContainsKey("Authorization")){
                //context.Token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c";
                context.Token = context.Request.Headers["Authorization"];
            }
                
            Debugging.print(context.Token);

            return Task.CompletedTask;
        },

        OnAuthenticationFailed = context =>
            {
                // Log the error
                
                Debugging.print(context.Exception.ToString());
                return context.Response.WriteAsync(context.Exception.ToString());
            },
            OnTokenValidated = context =>
            {
                // Log the validated token
                var validatedToken = context.SecurityToken as JwtSecurityToken;
                if (validatedToken != null)
                {
                    Debugging.print($"Validated Token: {validatedToken.RawData}");
                }
                return Task.CompletedTask;
            },
    };
});

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("Database"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

}else
{   
    app.UseDeveloperExceptionPage(); 
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<SessionManagement>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

Seeder.InsertDummyData(app);

app.Run();
