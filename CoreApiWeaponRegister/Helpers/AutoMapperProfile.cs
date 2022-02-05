using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiWeaponRegister.Entities;
using CoreApiWeaponRegister.Entities.Models;
using CoreApiWeaponRegister.Entities.Models.Usuarios;
using CoreApiWeaponRegister.Entities.Models.Personas;
using CoreApiWeaponRegister.Entities.Models.RegistrosTramites;
using CoreApiWeaponRegister.Entities.Models.RegistrosTramitesDocumentos;
using CoreApiWeaponRegister.Entities.Models.RegistrosTramitesArchivos;
namespace CoreApiWeaponRegister.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public static IMapper Mapper = new MapperConfiguration(c => c.AddProfile(new AutoMapperProfile())).CreateMapper();

        public AutoMapperProfile()
        {

            //DisableConstructorMapping();

            // User -> AuthenticateResponse
            CreateMap<Usuarios_pw, AuthenticateResponse>();

            // RegisterRequest -> User
            CreateMap<RegisterModel, Usuarios_pw>();
            //CreateMap<Usuarios_pw, RegisterRequest>();

            CreateMap<RegisterPersonasModel, Personas_pw>();

            CreateMap<RegisterTramitesModel, RegistrosTramites_pw>();
            CreateMap<RegisterTramitesDocumentosModel, RegistrosTramitesDocumentos_pw>();
            CreateMap<RegisterTramitesArchModel, RegistrosTramitesArchivos_pw>();
            CreateMap<ModifiPreCargaModel, RegistrosTramitesArchivosPrecarga>();
            // UpdateRequest -> User
            CreateMap<UpdateRequest, Usuarios_pw>()
                .ForAllMembers(x => x.Condition(
                    (src, dest, prop) =>
                    {
                        // ignore null & empty string properties
                        if (prop == null) return false;
                        if (prop.GetType() == typeof(string) && string.IsNullOrEmpty((string)prop)) return false;

                        return true;
                    }
                ));
        }
    }
}
