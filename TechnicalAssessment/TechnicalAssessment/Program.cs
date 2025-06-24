using Microsoft.EntityFrameworkCore;
using TechnicalAssessment.Core.Context;
using TechnicalAssessment.Core.EntityFramework.Interfaces;
using TechnicalAssessment.Core.EntityFramework;
using TechnicalAssessment.Core.Services;
using TechnicalAssessment.Core.Services.Interfaces;
using TechnicalAssessment.Core.DI;
using Microsoft.AspNetCore.Diagnostics;
using TechnicalAssessment.Core.Helper;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSharedInfrastructure<TechAssessmentDbContext>();
builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<TechAssessmentDbContext>(options =>
    options.UseSqlite("Data Source=products.db"));

var app = builder.Build();

app.UseCors(x =>
{
    x.WithOrigins(builder.Configuration["AllowedCorsOrigin"]
      .Split(",", StringSplitOptions.RemoveEmptyEntries)
      .Select(o => o.RemovePostFix("/"))
      .ToArray())
 .AllowAnyMethod()
 .AllowAnyHeader()
 .AllowCredentials();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
