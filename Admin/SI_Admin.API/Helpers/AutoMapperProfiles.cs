using System;
using AutoMapper;
using System.Collections.Generic;

using SI_Admin.API.DTO;
using Framework.DataTypes.Model.Base;
using Framework.DataTypes.Model.Licenciamiento;
using Framework.DataTypes.Model.Infraestructura;

namespace SI_Admin.API.Helpers
{
    public class AutoMapperProfiles: Profile 
    {
        public AutoMapperProfiles()
        {
            // CreateMap<Origen, Destino>()
            //     .ForMember(dest => dest.PropDest, opt => {
            //         opt.MapFrom(src => src.[PropOrg.FirstOrDefault(p => p.Condicion)].PropOrgFinal);
            //     })
            // .BeforeMap( (s, d) => d.Propiedad = "valor directo"); // Para Inicializar un valor

            //       USO: var myVar = _mapper.Map<destino>(Origen);

            // Ejemplo Listas e Ignore
            // Mapper.CreateMap<Person, PersonView>();
            // Mapper.CreateMap<PersonView, Person>()
            //     .ForMember(person => person.Id, opt => opt.Ignore());
            //      USO:    List<PersonView> personViews =  Mapper.Map<List<Person>, List<PersonView>>(people);

            CreateMap<ClienteParaRegistroDTO, Cliente>();
            CreateMap<Paquete, PaqueteParaLista>(); 
            CreateMap<ClienteActualizacion, ActualizacionClienteParaListaDTO>()
                .ForMember(dest => dest.NombreCliente, opt => {
                     opt.MapFrom(src => src.Cliente.NomEmpresa);
                 })
                 .ForMember(dest => dest.ActualizacionId, opt => {
                     opt.MapFrom(src => src.Id);
                 });
            CreateMap<ClienteActualizacion, ClienteActualizacionDTO>();
            CreateMap<ClienteNegocioParaCreacionDTO, ClienteNegocio>();
            CreateMap<PaqueteApp, LicenciaApp>()
                .ForMember(la => la.Id, opt => opt.Ignore());
            CreateMap<PaqueteApp, ClienteActualizacionApp>()
                .ForMember(ca => ca.Id, opt => opt.Ignore());
            
            CreateMap<Aplicacion, MenuDTO>()
                .ForMember(dn => dn.DisplayName, opt => {
                    opt.MapFrom(src => src.Nombre);                    
                })
                .ForMember(d => d.Description, opt => {
                    opt.MapFrom(src => src.Descripcion);                    
                })
                .BeforeMap( (s, d) => d.DisplayIcon = "")
                .BeforeMap( (s, d) => d.Childs = new List<MenuDTO>());

            CreateMap<AppMenu, MenuDTO>()
                .ForMember(dn => dn.DisplayName, opt => {
                    opt.MapFrom(src => src.Nombre);                    
                })
                .ForMember(d => d.Description, opt => {
                    opt.MapFrom(src => src.Descripcion);                    
                })
                .ForMember(d => d.DisplayIcon, opt => {
                    opt.MapFrom(src => src.ClaseIcono);                    
                });

        }
        
    }
}