using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using System.Threading.Tasks;
using CoreApiWeaponRegister.Entities;
using CoreApiWeaponRegister.Entities.Models;
using CoreApiWeaponRegister.Entities.Models.Usuarios;
using CoreApiWeaponRegister.Repository;
using CoreApiWeaponRegister.Repository.Interfaces;

namespace CoreApiWeaponRegister.Repository.Interfaces
{
    public interface IUsuarioRepository : IGenericRepository<Usuarios_pw>
    {
        Usuarios_pw Authenticate(string username, string password);
        //AuthenticateResponse Authenticate(AuthenticateRequest model);
        Usuarios_pw Create(Usuarios_pw user, string password, string tokenverificacion);

       bool FindEmailRegister(string email);
       bool VerifEmailUsuario(string tokenverif);

    }
}
