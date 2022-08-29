using System.Reflection;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.IOC;

namespace OnboardingSIGDB1.API;

public class Startup
{
    public IConfiguration _configuration { get; }
    
    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection services)
    {
        StartupIoc.ConfigureServices(services, _configuration);
        
        services.AddControllers()
            .ConfigureApiBehaviorOptions(options =>
            {
                options.SuppressModelStateInvalidFilter = true;
            });
        
        services.AddSwaggerGen(c =>
        {

            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            c.IncludeXmlComments(xmlPath);
        });
    }
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="app"></param>
    /// <param name="env"></param>
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        app.Use(async (context, next) =>
        {
            await next.Invoke();
            var unitOfWork = (IUnitOfWork)context.RequestServices.GetService(typeof(IUnitOfWork))!;
            await unitOfWork.Commit();
        });
        
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseHttpsRedirection();

        app.UseRouting();
        
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        app.UseSwagger();

        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "OnboardingSIGDB1");
            c.RoutePrefix = string.Empty;
        });
    }
}