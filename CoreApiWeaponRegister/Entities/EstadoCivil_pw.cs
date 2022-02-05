using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace CoreApiWeaponRegister.Entities
{
    [Table("ESTADOCIVIL_PW")]
    public partial class EstadoCivil_pw
    {

        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        //[JsonIgnore]
        [Required(ErrorMessage = "Id Estado Civil es requerido")]
        public int IDESTADOCIVIL { get; set; }

        [Required]
        public string NOMBRE { get; set; }

        [JsonIgnore]
        [DataType(DataType.DateTime)]
        public DateTime? FECHAMODIFICACION { get; set; }

        [JsonIgnore]
        public string USUARIOMOD { get; set; }

    }
}
