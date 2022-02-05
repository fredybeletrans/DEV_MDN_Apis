using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CoreApiWeaponRegister.Entities
{
	[Table("PERSONAS_PW")]
	public partial class Personas_pw
	{
		[Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
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

		[DataType(DataType.DateTime)]
		public DateTime FECHANACIMIENTO { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? FECHACREACION { get; set; }
		public string USUARIO { get; set; }

		[DataType(DataType.DateTime)]
		public DateTime? FECHAMODIFICACION { get; set; }

		[JsonIgnore]
		public string USUARIOMOD { get; set; }
		public string PARTICULA { get; set; }
		public int? IDESTADOCIVIL { get; set; }

	}
}
