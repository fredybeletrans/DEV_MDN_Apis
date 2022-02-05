using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities
{

    [Table("TRAMITES_PW")]
    public partial class Tramites_pw
    {
        [Key]
        [Required(ErrorMessage = "Id tramite es requerido")]
        public int IDTRAMITE { get; set; }
        public string NOMBRE { get; set; }
        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime? FECHAMODIFICACION { get; set; }
        [JsonIgnore]
        public string USUARIOMOD { get; set; }
        public string ICONO { get; set; }
        public string DESCRIPCION { get; set; }


    }
}