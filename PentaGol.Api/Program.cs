using Microsoft.EntityFrameworkCore;
using PentaGol.Api.Extensions;
using PentaGol.Api.MiddleWares;
using PentaGol.Data.Contexts;
using PentaGol.Service.Helpers;
using PentaGol.Service.Mappers;

//var policyName = "_myAllowSpecificOrigins";
var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(name: policyName,
//                      builder =>
//                      {
//                          builder
//                            .WithOrigins("http://localhost:3000")
//                            //.AllowAnyOrigin()
//                            .WithMethods("GET")
//                            .AllowAnyHeader();
//                      });
//});
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(MappingProfile));
builder.Services.AddCustomServices();
//Add database to the container.
builder.Services.AddDbContext<AppDbContext>(options =>
       options.UseNpgsql(builder.Configuration.GetConnectionString("MyDatabase")));

var app = builder.Build();

//Configure ImageUploading
EnvironmentHelper.WebHostPath =
    app.Services.GetRequiredService<IWebHostEnvironment>().WebRootPath;

app.UseMiddleware<ExceptionHandlerMiddleWare>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
//app.UseCors(policyName);
app.UseAuthorization();

app.MapControllers();

app.Run();
