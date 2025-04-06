using System.Text;
using System.Text.Json.Serialization;
using imsapi.Data;
using Microsoft.EntityFrameworkCore;
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
builder.Services.AddSwaggerGen(
    c=>c.SwaggerDoc("v1",new OpenApiInfo{Title="Aspnet Core Api", Version="v1"}));
    builder.Services.AddDbContext<AppDbContext>(options => options
        .UseSqlite(builder.Configuration
        .GetConnectionString("DefaultConnection")));
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
// // Add services to the container.
// builder.Services.AddControllers().AddJsonOptions(x =>
//                 x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// builder.Services.AddSwaggerGen(
//     c=>c.SwaggerDoc("v1",new OpenApiInfo{Title="Aspnet Core Api", Version="v1"})
// );
// // Add Auhentication JWT token
// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(option=>{
//     option.TokenValidationParameters=new TokenValidationParameters{
//         ValidateAudience=true,
//         ValidateIssuer=true,
//         ValidateLifetime=true,
//         ValidateIssuerSigningKey=true,
//         ValidIssuer=builder.Configuration["Jwt:Issuer"],
//         ValidAudience=builder.Configuration["Jwt:Audience"],
//         ClockSkew=TimeSpan.Zero,
//         IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!))
//     }; 
// });
// builder.Services.AddAuthorization();


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
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
// Seed.Init(app);
app.UseHttpsRedirection();
// app.UseStaticFiles(
//     new StaticFileOptions
//     {
//         FileProvider= new PhysicalFileProvider(
//             Path.Combine(builder.Environment.ContentRootPath,"StaticFiles")
//         ),
//         RequestPath=" "
//     }
// );
// app.UseAuthentication();
// app.UseAuthorization();

// app.MapControllers();
app.Run();