using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities.Models.RegistrosTramitesDocumentos
{
   
    public partial class RegisterTramitesDocumentosModel
    {
        public int IDREGISTROTRAMITEDOCUMENTO { get; set; }
        public int IDDOCUMENTO { get; set; }
        public int IDREGISTROTRAMITE { get; set; }
        public DateTime? FECHAMODIFICACION { get; set; }
        public string USUARIOMOD { get; set; }
        public DateTime? FECHAVIGENCIA { get; set; }
        public string ESTADO { get; set; }
        public int? CADUCADO { get; set; }
        public DateTime? FECHAEXPIRACION { get; set; }
        public DateTime? FECHAEMISION { get; set; }

    }
}
