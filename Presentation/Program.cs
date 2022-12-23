using IoC;
using Data;
using Autofac;
using Microsoft.EntityFrameworkCore;
using Autofac.Extensions.DependencyInjection;
using System.Reflection;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Xml.XPath;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(c => 
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Api", Version = "v1" });
    c.ResolveConflictingActions(apiDescriptionExtensions => apiDescriptionExtensions.First());
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

builder.Services.AddDbContext<SqlContext>(s => s.UseSqlServer(builder.Configuration.GetConnectionString("StrConn")));


builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(b => b.RegisterModule(new ModuleIoC()));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseSwagger(c =>
{
    c.RouteTemplate = "api/swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.RoutePrefix = "api";
    c.SwaggerEndpoint("/api/swagger/v1/swagger.json", "ApiSeventh");
    c.DefaultModelsExpandDepth(-1);
});

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
