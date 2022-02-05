using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister.Entities.Models.RegistrosTramitesArchivos
{
    public class ModifiPreCargaModel
    {
        public int IDREGISTROTRAMITEDOCUMENTO { get; set; }
        public int ITEM { get; set; }
        public string NOMBREARCHIVO { get; set; }
        public string RUTA { get; set; }
        public DateTime? FECHACARGA { get; set; }
    }
}
