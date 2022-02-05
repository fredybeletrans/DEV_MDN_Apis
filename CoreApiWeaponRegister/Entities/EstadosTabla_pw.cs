using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CoreApiWeaponRegister.Entities
{
    [Table("ESTADOS_PW")]
    public partial class EstadosTabla_pw
    {
        [Key]
        [Column(Order = 0)]
        public string ESTADO { get; set; }

        //[Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Column(Order = 1)]
        public string TABLA { get; set; }

        [Required]
        public string NOMBRE { get; set; }

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime? FECHAMODIFICACION { get; set; }

        [JsonIgnore]
        public string USUARIOMOD { get; set; }
    }
}
