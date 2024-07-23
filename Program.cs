using System.Text;
using e_commerce.Configurations;
using e_commerce.Domain.Repositories;
using e_commerce.Middlewares;
using e_commerce.utils;
using e_commerce.Services;
using Microsoft.IdentityModel.Tokens;
using e_commerce.DepandancyInjection;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddRepositories();
builder.Services.AddServices();
builder.Services.AddHttpContextAccessor();
builder.Services.AddAuthorization(Options => Options.AddPolicy("signed_in", policy => policy.RequireClaim("signed_in","true","True")));


// set Http request Body Limit to ~ 30MB
builder.WebHost.ConfigureKestrel(Options => Options.Limits.MaxRequestBodySize = 30_000_000);

var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("PrivateKey"));

// set JWT configuraton Options with a exntestion funtion
builder.Services.AddJwt(key);


builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("Database"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();

}
else
{   
    app.UseDeveloperExceptionPage(); 
}

// this suppose to be hidden in the production. i just added it to make you able to view the endpoints
app.UseSwagger();
app.UseSwaggerUI();


app.UseStaticFiles();

app.UseRouting();

app.UseMiddleware<CookieMiddleware>();

// middleware to handle the session
app.UseMiddleware<SessionManagementMiddleware>();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}/{id?}");

// factory that insert dummy data into a database
Seeder.InsertDummyData(app);

app.Run();
