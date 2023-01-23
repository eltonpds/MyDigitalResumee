using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

using MyDigitalResumee.Api.Model;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Title",
            Description= "Description",
            TermsOfService= new Uri("https://example.com/terms"),
            Contact = new OpenApiContact
            {
                Name = "",
                Url = new Uri("https://example.com/contact")
            },
            License = new OpenApiLicense
            {
                Name = "Example license",
                Url = new Uri("https://example.com/license")
            }
        });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });
builder.Services.AddDbContext<MyDigitalResumeeContext>(opt =>
    opt.UseInMemoryDatabase("MyDigitalResumeeDB"));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
