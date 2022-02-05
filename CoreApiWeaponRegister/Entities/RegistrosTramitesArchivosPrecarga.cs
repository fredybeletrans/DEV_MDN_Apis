using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities
{
    [Table("REGISTROSTRAMITESARCHIVOSPRECARGA")]
    public class RegistrosTramitesArchivosPrecarga
    {
        [Key]
        [Column(Order = 0)]
        [Required(ErrorMessage = "Id registro tramite documento es requerido")]
        public int IDREGISTROTRAMITEDOCUMENTO { get; set; }
        [Key]
        [Column(Order = 1)]
        public int ITEM { get; set; }
        public string NOMBREARCHIVO { get; set; }
        public string RUTA { get; set; }
        public DateTime? FECHACARGA { get; set; }
    }
}
