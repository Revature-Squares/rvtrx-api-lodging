using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RVTR.Lodging.Context;
using RVTR.Lodging.Context.Repositories;
using RVTR.Lodging.Domain.Interfaces;
using Swashbuckle.AspNetCore.SwaggerGen;
using zipkin4net.Middleware;

namespace RVTR.Lodging.Service
{
  /// <summary>
  ///
  /// </summary>
  public class Startup
  {
    /// <summary>
    ///
    /// </summary>
    private readonly IConfiguration _configuration;

    /// <summary>
    ///
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
      _configuration = configuration;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="services"></param>
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddApiVersioning(options =>
      {
        options.ReportApiVersions = true;
      });

      services.AddControllers().AddNewtonsoftJson(options =>
      {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
      });

      services.AddCors(options =>
      {
        options.AddPolicy("public", policy =>
        {
          policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
        });
      });

      services.AddDbContext<LodgingContext>(options =>
      {
        options.UseNpgsql(_configuration.GetConnectionString("pgsql"), options =>
        {
          options.EnableRetryOnFailure(3);
        });
      }, ServiceLifetime.Transient);

      services.AddScoped<ClientZipkinMiddleware>();
      services.AddScoped<UnitOfWork>();
      services.AddSwaggerGen();
      services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ClientSwaggerOptions>();
      services.AddVersionedApiExplorer(options =>
      {
        options.GroupNameFormat = "VV";
        options.SubstituteApiVersionInUrl = true;
      });

    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="applicationBuilder"></param>
    /// <param name="hostEnvironment"></param>
    /// <param name="descriptionProvider"></param>
    public void Configure(IApiVersionDescriptionProvider descriptionProvider, IApplicationBuilder applicationBuilder, IWebHostEnvironment hostEnvironment)
    {
      if (hostEnvironment.IsDevelopment())
      {
        applicationBuilder.UseDeveloperExceptionPage();
      }

      applicationBuilder.UseZipkin();
      applicationBuilder.UseTracing("lodgingapi.rest");
      applicationBuilder.UseRouting();
      applicationBuilder.UseSwagger(options =>
      {
        options.RouteTemplate = "rest/lodging/{documentName}/swagger.json";
      });
      applicationBuilder.UseSwaggerUI(options =>
      {
        options.RoutePrefix = "rest/lodging";

        foreach (var description in descriptionProvider.ApiVersionDescriptions)
        {
          options.SwaggerEndpoint($"/rest/lodging/{description.GroupName}/swagger.json", description.GroupName);
        }
      });

      applicationBuilder.UseCors();
      applicationBuilder.UseAuthorization();
      applicationBuilder.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
