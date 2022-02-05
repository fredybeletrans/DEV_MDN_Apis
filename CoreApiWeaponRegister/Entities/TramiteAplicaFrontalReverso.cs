using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities
{

    [Table("TRAMITEAPLICAFRONTALREVERSO")]
    public partial class TramiteAplicaFrontalReverso
    {
        [Key]
        [Required(ErrorMessage = "Id registro es requerido")]
        public int IDREGISTROTRAMITEDOCUMENTO { get; set; }
        public int IDDOCUMENTO { get; set; }
        public int APLICAFRONTALREVERSO { get; set; }

    }
}
