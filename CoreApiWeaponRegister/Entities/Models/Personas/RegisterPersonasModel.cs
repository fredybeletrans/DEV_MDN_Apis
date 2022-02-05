using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
namespace CoreApiWeaponRegister.Entities.Models.Personas
{
    public class RegisterPersonasModel
    {
		public int IDPERSONA { get; set; }
		public int IDTIPOPERSONA { get; set; }
		public int IDTIPOIDENTIFICACIONPERSONAL { get; set; }
		public string NUMDOCUMENTO { get; set; }
		public string NIT { get; set; }
		public string PRIMERNOMBRE { get; set; }
		public string SEGUNDONOMBRE { get; set; }
		public string TERCERNOMBRE { get; set; }
		public string PRIMERAPELLIDO { get; set; }
		public string SEGUNDOAPELLIDO { get; set; }
		public int? IDGENERO { get; set; }
		public string RAZONSOCIAL { get; set; }
		public string NOMBRE { get; set; }
		public string NODOCUMENTORESIDENTE { get; set; }
		public int ORGESTATALMINISTERIO { get; set; }
		public DateTime FECHANACIMIENTO { get; set; }
		public DateTime? FECHACREACION { get; set; }
		public string USUARIO { get; set; }
		public DateTime? FECHAMODIFICACION { get; set; }
		public string USUARIOMOD { get; set; }
		public string PARTICULA { get; set; }
		public int? IDESTADOCIVIL { get; set; }

	}
}
