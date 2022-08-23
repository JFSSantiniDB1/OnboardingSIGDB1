using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data;
using OnboardingSIGDB1.Data.Repositories;
using OnboardingSIGDB1.Domain.AutoMapper;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Repositories;
using OnboardingSIGDB1.Domain.Interfaces.Services;
using OnboardingSIGDB1.Domain.Interfaces.Validator;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Services.Cargos;
using OnboardingSIGDB1.Domain.Services.Empresas;
using OnboardingSIGDB1.Domain.Services.Funcionarios;
using OnboardingSIGDB1.Domain.Services.FuncionarioXCargos;

namespace OnboardingSIGDB1.IOC
{
    public static class StartupIoc
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SIGDB1DbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), 
                    b => b.MigrationsAssembly("OnboardingSIGDB1.Data"));
            });
            
            services.AddScoped<ICargoRepository, CargoRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<IFuncionarioXCargoRepository, FuncionarioXCargoRepository>();

            services.AddScoped<ICargoService, CargoService>();
            services.AddScoped<IEmpresaService, EmpresaService>();
            services.AddScoped<IFuncionarioService, FuncionarioService>();
            services.AddScoped<IFuncionarioXCargoService, FuncionarioXCargoService>();
            
            services.AddScoped<INotificationContext, NotificationContext>();

            services.AddScoped<ICargoValidatorService, CargoValidatorService>();
            services.AddScoped<IEmpresaValidatorService, EmpresaValidatorService>();
            services.AddScoped<IFuncionarioValidatorService, FuncionarioValidatorService>();
            services.AddScoped<IFuncionarioXCargoValidatorService, FuncionarioXCargoValidatorService>();

            BaseMapper.Init();
        }
        
    }
}