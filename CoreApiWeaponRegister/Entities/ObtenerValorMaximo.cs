using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CoreApiWeaponRegister.Entities
{
    [Table("OBTNERVALORMAXIMO")]
    public class ObtenerValorMaximo
    {
        [Key]
        public int MaximoValor { get; set; }
    }
}
