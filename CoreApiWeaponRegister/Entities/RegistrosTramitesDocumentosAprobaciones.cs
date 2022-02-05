using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities
{
    [Table("REGISTROSTRAMITESDOCUMENTOSAPROBACIONES")]
    public class RegistrosTramitesDocumentosAprobaciones
    {
        [Key]
        [Required(ErrorMessage = "Id registro es requerido")]
        public int IDREGISTROTRAMITEDOCUMENTO { get; set; }
        public int ITEM { get; set; }
        public int IDDOCUMENTO { get; set; }
        public string NOMBREDOCUMENTO { get; set; }
        public int IDREGISTROTRAMITE { get; set; }
        public string PARTEDOCUMENTO { get; set; }
        public string NOMBREARCHIVO { get; set; }
        public string RUTA { get; set; }
        public int IDPERSONA { get; set; }
        public string NOMBREPERSONA { get; set; }
        public int IDTRAMITE { get; set; }
        public string NOMBRETRAMITE { get; set; }
        public int IDTRAMITEOPCION { get; set; }
        public string NOMBRETRAMITEOPCION { get; set; }
        public int IDTIPOTRAMITE { get; set; }
        public string NOMBRETIPOTRAMITE { get; set; }

    }
}
