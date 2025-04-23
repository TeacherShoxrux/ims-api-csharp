using System.Text;
using System.Text.Json.Serialization;
using imsapi.Data;
using imsapi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});
builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);



    builder.Services.AddDbContext<AppDbContext>(options => options
        .UseSqlite(builder.Configuration
        .GetConnectionString("DefaultConnection")));

        builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));
        builder.Services.AddHttpContextAccessor();
        builder.Services.AddScoped<IJwtService, JwtService>();
        builder.Services.AddScoped<IStoreService, StoreService>();
        builder.Services.AddScoped<ICategoryService, CategoryService>();
        builder.Services.AddScoped<IProductService, ProductService>();
        builder.Services.AddScoped<IUserService, UserService>();
        builder.Services.AddScoped<IImageService, ImageService>();
        builder.Services.AddScoped<IPaymentService, PaymentService>();
        builder.Services.AddScoped<ICustomerService, CustomerService>();
        builder.Services.AddScoped<IStatiscsService, StatisticsService>();


builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("JwtSettings"));


// JWT middleware ni qo'shish
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,

            ValidIssuer = builder.Configuration["JwtSettings:Issuer"],
            ValidAudience = builder.Configuration["JwtSettings:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration["JwtSettings:Key"])
            )
        };
    });

// Authorization middleware'ni qo'shish
builder.Services.AddAuthorization();

// //' Add DbConext
// var connString=builder.Configuration.GetConnectionString("DefaultConnectionString");
// builder.Services.AddDbContext<ApplicationDbContext>(
//     options=>options.UseSqlite(connString)
// );

// // Add UnitOfWork
// builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();

// builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment() )
// {
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
// }
app.UseCors("AllowAll");
// Seed.Init(app);
app.UseHttpsRedirection();
app.UseStaticFiles(); // Bu wwwroot papkasidagi fayllarga kirishga ruxsat beradi: http://localhost:5046/images/

                    // URL orqali fayllarga kirish uchun sozlash (masalan, /images/pic.jpg)
app.UseRouting();
// app.UseStaticFiles(
//     new StaticFileOptions
//     {
//         FileProvider= new PhysicalFileProvider(
//             Path.Combine(builder.Environment.ContentRootPath,"StaticFiles")
//         ),
//         RequestPath=" "
//     }
// );
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();