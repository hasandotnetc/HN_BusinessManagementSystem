using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// MVC
builder.Services.AddControllersWithViews();


// ===============================
// Core API
// ===============================
builder.Services.AddHttpClient("CoreApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7008/");
});


// ===============================
// JWT Authentication
// ===============================
var jwtSecretKey = builder.Configuration["JwtSettings:SecretKey"];
var jwtIssuer = builder.Configuration["JwtSettings:Issuer"];
var jwtAudience = builder.Configuration["JwtSettings:Audience"];

if (string.IsNullOrWhiteSpace(jwtSecretKey))
{
    throw new InvalidOperationException(
        "JWT SecretKey is missing from MVC appsettings.json."
    );
}

if (string.IsNullOrWhiteSpace(jwtIssuer))
{
    throw new InvalidOperationException(
        "JWT Issuer is missing from MVC appsettings.json."
    );
}

if (string.IsNullOrWhiteSpace(jwtAudience))
{
    throw new InvalidOperationException(
        "JWT Audience is missing from MVC appsettings.json."
    );
}


builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
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
                Encoding.UTF8.GetBytes(jwtSecretKey)
            ),

            ClockSkew = TimeSpan.Zero
        };


        // ===============================
        // Read JWT from Cookie
        // ===============================
        options.Events = new JwtBearerEvents
        {
            OnMessageReceived = context =>
            {
                var token = context.Request.Cookies["HN_JWT"];

                if (!string.IsNullOrWhiteSpace(token))
                {
                    context.Token = token;
                }

                return Task.CompletedTask;
            }
        };
    });


// ===============================
// Authorization
// ===============================
builder.Services.AddAuthorization();


var app = builder.Build();


// ===============================
// Middleware
// ===============================
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();


// Authentication MUST come before Authorization
app.UseAuthentication();

app.UseAuthorization();


// ===============================
// MVC Route
// ===============================
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Home}/{action=Index}/{id?}"
//);
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");
app.Run();