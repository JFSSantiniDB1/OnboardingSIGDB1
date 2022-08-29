using System;
using System.Linq;
using AutoMapper;
using OnboardingSIGDB1.Domain.Dto.Cargos;
using OnboardingSIGDB1.Domain.Dto.Empresas;
using OnboardingSIGDB1.Domain.Dto.Funcionarios;
using OnboardingSIGDB1.Domain.Dto.FuncionarioXCargos;
using OnboardingSIGDB1.Domain.Entities;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.AutoMapper
{
    public static class BaseMapper
    {
        public static IMapper Mapper;
        
        public static void Init()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<Cargo, CargoDto>();
                cfg.CreateMap<CargoDto, Cargo>();
                cfg.CreateMap<CargoInputDto, CargoDto>();

                cfg.CreateMap<FuncionarioDto, Funcionario>()
                    .ForMember(c => c.IdEmpresa, 
                        act => act.MapFrom(src => src.IdEmpresa == 0 ? null : src.IdEmpresa))
                    .ForMember(c => c.Cpf, 
                        act => act.MapFrom(src => Convertions.GetCpfSemMascara(src.Cpf)));
                
                cfg.CreateMap<FuncionarioInputDto, FuncionarioDto>();

                cfg.CreateMap<Funcionario, FuncionarioListDto>()
                    .ForMember(c => c.DataContratacao,
                        act => act.MapFrom(src => Convertions.GetDateFormatted(src.DataContratacao)))
                    .ForMember(c => c.Cpf,
                        act => act.MapFrom(src => Convertions.GetCpfComMascara(src.Cpf)));
                
                cfg.CreateMap<Funcionario, FuncionarioDto>()
                    .ForMember(c => c.DataContratacao, 
                        act => act.MapFrom(src => Convertions.GetDateFormatted(src.DataContratacao)))
                    .ForMember(c => c.Cpf, 
                        act => act.MapFrom(src => Convertions.GetCpfComMascara(src.Cpf)))
                    .ForMember(c => c.Empresa, 
                        act => act.MapFrom(src => src.Empresa.Nome))
                    .ForMember(c => c.DataVinculoCargo, act => 
                        act.MapFrom(src =>  Convertions.GetDateFormatted(src.Cargos.OrderByDescending(x => x.DataVinculo)
                            .FirstOrDefault(x => x.DataVinculo <= DateTime.Now).DataVinculo)))
                    .ForMember(c => c.IdCargoAtual, act => 
                        act.MapFrom(src => ((int?) src.Cargos.OrderByDescending(x => x.DataVinculo)
                            .FirstOrDefault(x => x.DataVinculo <= DateTime.Now).IdCargo) ?? 0))
                    .ForMember(c => c.CargoAtual, act => 
                        act.MapFrom(src => src.Cargos.OrderByDescending(x => x.DataVinculo)
                            .FirstOrDefault(x => x.DataVinculo <= DateTime.Now).Cargo.Descricao ?? ""));
                
                
                cfg.CreateMap<FuncionarioXCargo, FuncionarioXCargoDto>()
                    .ForMember(c => c.DataVinculo, 
                        act => act.MapFrom(src => Convertions.GetDateFormatted(src.DataVinculo)))
                    .ForMember(c => c.DescricaoCargo, 
                        act => act.MapFrom(src => src.Cargo.Descricao))
                    .ForMember(c => c.NomeFuncionario, 
                        act => act.MapFrom(src => src.Funcionario.Nome));
                
                cfg.CreateMap<FuncionarioXCargoDto, FuncionarioXCargo>();
                cfg.CreateMap<FuncionarioXCargoInputDto, FuncionarioXCargoDto>();

                cfg.CreateMap<Empresa, EmpresaListDto>()
                    .ForMember(c => c.Cnpj,
                        act => act.MapFrom(src => Convertions.GetCnpjComMascara(src.Cnpj)))
                    .ForMember(c => c.DataFundacao,
                        act => act.MapFrom(src => Convertions.GetDateFormatted(src.DataFundacao)));            
                
                cfg.CreateMap<Empresa, EmpresaDto>()
                    .ForMember(c => c.Cnpj, 
                        act => act.MapFrom(src => Convertions.GetCnpjComMascara(src.Cnpj)))
                    .ForMember(c => c.DataFundacao, 
                        act => act.MapFrom(src => Convertions.GetDateFormatted(src.DataFundacao)))
                    .ForMember(c => c.QuantidadeFuncionarios, 
                        act => act.MapFrom(src => src.Funcionarios.Count()))
                    .ForMember(c => c.QuantidadeFuncionariosComCargo, 
                        act => act.MapFrom(src => src.Funcionarios.Count(x => x.Cargos.Any())));
                
                cfg.CreateMap<EmpresaDto, Empresa>()
                    .ForMember(c => c.Cnpj, 
                        act => act.MapFrom(src => Convertions.GetCnpjSemMascara(src.Cnpj)));
                
                cfg.CreateMap<EmpresaInputDto, EmpresaDto>();
            });

            Mapper = configuration.CreateMapper();
        }
    }
}