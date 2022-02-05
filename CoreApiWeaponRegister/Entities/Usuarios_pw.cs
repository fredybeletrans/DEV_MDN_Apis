using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json.Serialization;


namespace CoreApiWeaponRegister.Entities
{
    [Table("USUARIOS_PW")]
    public partial class Usuarios_pw
    {
        //private Roles_pw Roles;
        //public Usuarios_pw()
        //{
        //    Roles_pw = new HashSet<Roles_pw>();
        //}

        [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]        
        //[JsonIgnore]
        [Required(ErrorMessage = "Id de Usuario autogenerado")]
        public int IDUSUARIO { get; set; }
                
        //[StringLength(12)]
        [Required(ErrorMessage = "Usuario es requerido")]
        //[MinLength(12, ErrorMessage = "El Usuario debe de ser minimo de 12 caracteres.")]
        //[MaxLength(12, ErrorMessage = "El Usuario debe de ser maximo de 12 caracteres.")]
        public string USUARIO { get; set; }

        //[StringLength(50)]
        [Required(ErrorMessage = "Nombres de usuario son requeridos")]
        //[MinLength(10, ErrorMessage = "Nombre de Usuario debe de ser minimo de 10 caracteres.")]
        [MaxLength(50, ErrorMessage = "Nombre de Usuario debe de ser maximo de 50 caracteres.")]
        public string NOMBRES { get; set; }

        //[StringLength(50)]
        [Required(ErrorMessage = "Apellidos de usuario son requeridos")]
        //[MinLength(10, ErrorMessage = "Nombre de Usuario debe de ser minimo de 10 caracteres.")]
        //[MaxLength(50, ErrorMessage = "Nombre de Usuario debe de ser maximo de 50 caracteres.")]
        public string APELLIDOS { get; set; }

        //[StringLength(14)]
        //[Required(ErrorMessage = "Contrasena es requerida")]
        //[MinLength(8, ErrorMessage = "Contrasena debe de ser minimo de 8 caracteres.")]
        //[MaxLength(14, ErrorMessage = "Contrasena debe de ser maximo de 10 caracteres.")]
        
        [Required]
        public String PASSWORDHASH { get; set; }
        //public byte[] PASSWORDHASH { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime? FECHAMODIFICACION { get; set; }
        
        [JsonIgnore]
        public string USUARIOMOD { get; set; }

        [JsonIgnore]
        public String PASSWORDSALT { get; set; }
        //public byte[] PASSWORDSALT { get; set; }

        //[Column(TypeName = "date")]
        [DataType(DataType.DateTime)]
        public DateTime? FECHAVERIFICACIONCUENTA { get; set; }

        //[Required]
        public string TOKENVERIFICACION { get; set; }

        [Required(ErrorMessage = "El Correo es requerido")]
        public string CORREO { get; set; }
        public string ESTADO { get; set; }

        //[ForeignKey(nameof(idrol))]


        //[InverseProperty("idusuariorol")]
        //public virtual ICollection<Roles_pw> Roles_pw { get; set; }
    }
}
