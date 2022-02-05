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
    public interface IPreguntasRepository:IGenericRepository<Preguntas_pw>
    {
        IEnumerable<Preguntas_pw> ObtenerPreguntas();
    }
}
