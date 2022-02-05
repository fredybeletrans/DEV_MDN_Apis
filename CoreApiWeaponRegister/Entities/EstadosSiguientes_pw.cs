using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CoreApiWeaponRegister.Entities
{
    [Table("ESTADOSSIGUIENTES_PW")]
    public class EstadosSiguientes_pw
    {
        [Key]
        [Column(Order = 0)]
        //[JsonIgnore]
        public string ESTADO { get; set; }

        [Key]
        [Column(Order = 1)]
        public string TABLA { get; set; }

        [Key]
        [Column(Order = 2)]
        public string ESTADOSIGUIENTE { get; set; }

        public int CAMBIOMANUAL { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }

        public string USUARIOMOD { get; set; }
    }
}
