//using CoreApiMDN.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using EntityFramework.OracleHelpers;
using Microsoft.EntityFrameworkCore.Metadata;
using System.ComponentModel.DataAnnotations;
using Oracle.ManagedDataAccess.Client;
using CoreApiWeaponRegister.Data;
using CoreApiWeaponRegister.Entities;

namespace CoreApiWeaponRegister.Data
{
    //public partial class DataContext : DbContext
    public class DataContext : DbContext
    {

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        public DbSet<Usuarios_pw> USUARIOS_PW { get; set; }
        public DbSet<EstadoCivil_pw> ESTADOCIVIL_PW { get; set; }
        public DbSet<Generos_pw> GENEROS_PW { get; set; }
        public DbSet<EstadosTabla_pw> ESTADOSTABLA_PW { get; set; }
        public DbSet<TiposDocumentos_pw> TIPOSDOCUMENTO_PW { get; set; }
        public DbSet<TiposPersonas_pw> TIPOSPERSONAS_PW { get; set; }
        public DbSet<TiposIdentificacionesPersonales_pw> TIPOSIDENTIFICACIONESPERSONALES_PW { get; set; }
        public DbSet<RelacionTiposIdentificacionesPersonas_pw> RELACIONTIPOSIDENTIFICACIONESPERSONAS_PW { get; set; }
        public DbSet<RelacionTiposIdentificacionesPersonasVista_pw> RELACIONTIPOSIDENTIFICACIONESPERSONASVISTA_PW { get; set; }
        public DbSet<Preguntas_pw> PREGUNTAS_PW { get; set; }
        public DbSet<Personas_pw> PERSONAS_PW { get; set; }
        public DbSet<Tramites_pw> TRAMITES_PW { get; set; }
        public DbSet<RelacionTramiteOpciones_pw> RELACIONTRAMITEOPCIONES_PW { get; set; }
        public DbSet<RelacionTramiteOpcionesVista_pw> RELACIONTRAMITEOPCIONESVISTA_PW { get; set; }
        public DbSet<RelacionTramitesTipos_pw> RELACIONTRAMITESTIPOS_PW { get; set; }
        public DbSet<RelacionTramitesTiposVista_pw> RELACIONTRAMITESTIPOSVISTA_PW { get; set; }
        public DbSet<RegistrosTramites_pw> REGREGISTROSTRAMITES_PW { get; set; }
        public DbSet<TramitesDocumentos_pw> TRAMITESDOCUMENTOS_PW { get; set; }
        public DbSet<TramitesDocumentosVista_pw> TRAMITESDOCUMENTOSVISTA_PW { get; set; }
        public DbSet<RegistrosTramitesDocumentos_pw> REGISTROSTRAMITESDOCUMENTOS_PW { get; set; }
        public DbSet<RegistrosTramitesDocumentosVista_pw> REGISTROSTRAMITESDOCUMENTOSVISTA_PW { get; set; }
        public DbSet<RegistrosTramitesArchivos_pw> REGISTROSTRAMITESARCHIVOS_PW { get; set; }
        public DbSet<ObtenerValorMaximo> OBTNERVALORMAXIMO { get; set; }
        public DbSet<TramiteAplicaFrontalReverso> TRAMITEAPLICAFRONTALREVERSO { get; set; }

        public DbSet<RegistrosTramitesArchivosPrecarga> REGISTROSTRAMITESARCHIVOSPRECARGA { get; set; }

        public DbSet<RegistrosTramitesRuta> REGISTROSTRAMITESRUTA { get; set; }
        public DbSet<RegistrosTramitesAprobacion> REGISTROSTRAMITESAPROBACION { get; set; }
        public DbSet<RegistrosTramitesDocumentosAprobaciones> REGISTROSTRAMITESDOCUMENTOSAPROBACIONES { get; set; }

        public DbSet<EstadosSiguientes_pw> ESTADOSSIGUIENTES_PW { get; set; }
        public DbSet<EstadosSiguientesVista_pw> ESTADOSSIGUIENTESVISTA_PW { get; set; }
        //public virtual DbSet<Usuarios_pw> PERSONAS_PW { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<RelacionTiposIdentificacionesPersonas_pw>().HasKey(p => new { p.IDTIPOIDENTIFICACIONPERSONAL, p.IDTIPOPERSONA });
            modelBuilder.Entity<RelacionTiposIdentificacionesPersonasVista_pw>().HasKey(p => new { p.IDTIPOIDENTIFICACIONPERSONAL, p.IDTIPOPERSONA });

            modelBuilder.Entity<RelacionTramiteOpciones_pw>().HasKey(p => new { p.IDTRAMITE, p.IDTRAMITEOPCION });
            modelBuilder.Entity<RelacionTramiteOpcionesVista_pw>().HasKey(p => new { p.IDTRAMITE, p.IDTRAMITEOPCION });

            modelBuilder.Entity<RelacionTramitesTipos_pw>().HasKey(p => new { p.IDTRAMITE, p.IDTIPOTRAMITE });
            modelBuilder.Entity<RelacionTramitesTiposVista_pw>().HasKey(p => new { p.IDTRAMITE, p.IDTIPOTRAMITE });

            modelBuilder.Entity<TramitesDocumentos_pw>().HasKey(p => new { p.IDTRAMITEOPCION, p.IDTIPOTRAMITE,p.IDDOCUMENTO });
            modelBuilder.Entity<TramitesDocumentosVista_pw>().HasKey(p => new { p.IDTRAMITEOPCION, p.IDTIPOTRAMITE, p.IDDOCUMENTO });

            modelBuilder.Entity<EstadosSiguientes_pw>().HasKey(p => new { p.ESTADO, p.TABLA, p.ESTADOSIGUIENTE });
            modelBuilder.Entity<EstadosSiguientesVista_pw>().HasKey(p => new { p.ESTADO, p.TABLA, p.ESTADOSIGUIENTE });

            modelBuilder.Entity<RegistrosTramitesArchivos_pw>().HasKey(p => new { p.IDREGISTROTRAMITEDOCUMENTO, p.ITEM });
            modelBuilder.Entity<RegistrosTramitesArchivosPrecarga>().HasKey(p => new { p.IDREGISTROTRAMITEDOCUMENTO, p.ITEM });

            modelBuilder.Entity<EstadosTabla_pw>().HasKey(p => new { p.ESTADO, p.TABLA });
        }
    }
}
