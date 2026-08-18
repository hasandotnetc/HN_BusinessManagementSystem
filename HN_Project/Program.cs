using HN_Backend.Data;
using HN_Backend.DTOs;
using HN_Backend.Interface; 
using HN_Backend.Repository;
using HN_Backend.Service; 
using HN_Project.Interface;
using HN_Project.Repository;
using HN_Project.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"));
});


//-------------------- DI For Controller ----------------//
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<ISearchingByDapperRepository, SearchingByDapperRepository>();
builder.Services.AddScoped<SearchingByDapperService>();
builder.Services.AddScoped<IProductRepositroy, ProductRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<ICurrentStock, CurrentStockRepository>();
builder.Services.AddScoped<ICurrentStockDetail, CurrentStockDetailRepository>();
builder.Services.AddScoped<ISalesOrder, SalesOrderRepository>();
builder.Services.AddScoped<ISalesOrderDetail, SalesOrderDetailRepository>();
builder.Services.AddScoped<ICollectionIn, CollectionRepository>();
builder.Services.AddScoped<ICollectionDetail, CollectionDetailRepository>();
builder.Services.AddScoped<ISalesOrderSerial, SalesOrderDetailSerialRepository>();
builder.Services.AddScoped<ISupplier, SupplierRepository>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<IUserLoginAndAuthentication, UserAuthenticationAndLoginRepository>();
builder.Services.AddScoped<UserAuthenticationAndLoginService>();
builder.Services.AddScoped<PointOfSalesService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<JWTTokenService>();
builder.Services.Configure<EmailSettingsDto>(builder.Configuration.GetSection("EmailSettings"));   //Email Sender Credential Setting from appsettings.json file.
builder.Services.AddScoped<ISMSorEmailServices, SMSorEmailServices>();
builder.Services.Configure<JwtSettingsDto>(builder.Configuration.GetSection("JwtSettings"));

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
   {
       var jwtSettings =builder.Configuration.GetSection("JwtSettings").Get<JwtSettingsDto>();
       options.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuer = true,
           ValidateAudience = true,
           ValidateLifetime = true,
           ValidateIssuerSigningKey = true,
           ValidIssuer = jwtSettings!.Issuer,
           ValidAudience = jwtSettings.Audience,
           IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.SecretKey))
       };
   });



//builder.Services.AddScoped<IUserAuthenticationAndLoginService,          UserAuthenticationAndLoginService>();
//-------------------- DI For Controller END ----------------//

var app = builder.Build();

//Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    //app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();  //for loading imageUrl show Image.
app.UseRouting();
app.UseCors();


app.UseAuthentication();
app.UseAuthorization(); 
app.MapControllers();

app.Run();



//# Overwrites ApplicationDbContext and Models in-place safely
//Scaffold - DbContext "Name=DefaultConnection" Microsoft.EntityFrameworkCore.SqlServer - OutputDir Data - Context ApplicationDbContext - Force

//Scaffold-DbContext "Name=DefaultConnection" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data -Context ApplicationDbContext -Force

//Scaffold-DbContext "Name=DefaultConnection" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data -Context ApplicationDbContext -Tables LoginUser -Force