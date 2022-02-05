using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;
using CoreApiWeaponRegister.Data;
using Microsoft.EntityFrameworkCore;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using CoreApiWeaponRegister.Repository;
using CoreApiWeaponRegister.Repository.Interfaces;

namespace CoreApiWeaponRegister.Extensions
{
    public static class ServiceExtensions
    {   
       
        public static void ConfigureCors(this IServiceCollection services)
        {
            services.AddCors(options =>
            {                
                options.AddPolicy("CorsPolicy",
                    buiilder => buiilder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        }

        public static void JsonCamelCToPascalC(this IServiceCollection services)
        {
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            });
        }

        public static void ApiVersion(this IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.AssumeDefaultVersionWhenUnspecified = true;
                options.DefaultApiVersion = new ApiVersion(1, 0);
                options.ReportApiVersions = true;
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new HeaderApiVersionReader("X-Api-Version"),
                    new QueryStringApiVersionReader("version"));
            });
        }

        public static void ConnectionDB(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config["ConnectionStrings:OracleDBConnectionDEV"];
            services.AddDbContext<DataContext>(options => options.UseOracle(connectionString));
            
        }

        public static void SwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            { 
                options.SwaggerDoc("v1", new OpenApiInfo { Title = "Apis Ministerio Defensa - Portal Registro Armas", Version = "v1" });
            });
        }

        public static void DapperConfig(this IServiceCollection services)   
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEstadoCivilRepository, EstadoCivilRepository>();
            services.AddScoped<IGeneroRepository, GenerosRepository>();
            services.AddScoped<IEstadosTablaRepository, EstadosTablaRepository>();
            services.AddScoped<ITiposDocumentosRepository, TiposDocumentosRepository>();
            services.AddScoped<ITiposPersonasRepository, TiposPersonasRepository>();
            services.AddScoped<ITiposIdentificacionesPersonalesRepository, TiposIdentificacionesPersonalesRepository>();
            services.AddScoped<IRelacionTiposIdentificacionesPersonasRepository, RelacionTiposIdentificacionesPersonasRepository>();
            services.AddScoped<IPreguntasRepository, PreguntasRepository>();
            services.AddScoped<IPersonasRepository, PersonasRepository>();
            services.AddScoped<ITramitesRepository, TramitesRepository>();
            services.AddScoped<IRelacionTramiteOpcionesRepository, RelacionTramiteOpcionesRepository>();
            services.AddScoped<IRelacionTramitesTiposRepository, RelacionTramitesTiposRepository>();
            services.AddScoped<IRegistrosTramitesRepository, RegistrosTramitesRepository>();
            services.AddScoped<ITramitesDocumentosRepository, TramitesDocumentosRepository>();
            services.AddScoped<IRegistrosTramitesDocumentosRepository, RegistrosTramitesDocumentosRepository>();
            services.AddScoped<IRegistrosTramitesArchivosRepository, RegistrosTramitesArchivosRepository>();
            services.AddScoped<IEstadosSiguentesRepository, EstadosSiguentesRepository>();
        }

        public static void AutoMapper(this IServiceCollection services)
        {
            //services.AddAutoMapper(typeof(Startup));
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        }
        //public static void ConfigureApacheIntegration(this IServiceCollection services)
        //{
        //    services.Configure<ApacheOptions>(options =>
        //    {
        //        options.
        //    });

        //}
    }
}
