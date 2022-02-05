using AutoMapper;
using System;
using System.IO;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreApiWeaponRegister.Repository.Interfaces;
using CoreApiWeaponRegister.Entities;
using CoreApiWeaponRegister.Entities.Models;
using CoreApiWeaponRegister.Data;
using CoreApiWeaponRegister.Helpers;
using CoreApiWeaponRegister.Entities.Models.Usuarios;
using System.Data.Common;
using static System.Data.CommandType;
using System.Data.OracleClient;
using Dapper;
using Microsoft.Extensions.Configuration;
using Oracle.ManagedDataAccess.Client;
using System.Data;
using BC = BCrypt.Net.BCrypt;
using System.Data.Entity;
using System.Linq.Expressions;

namespace CoreApiWeaponRegister.Repository
{
    //public abstract class UsuarioRepository<T> : IDaperRespository, IGenericRepository<Usuarios_pw>, IUsuarioRepository where T : class
    
    public class UsuarioRepository : GenericRepository<Usuarios_pw>, IUsuarioRepository
    {
        static private IConfiguration _config;
        private DataContext _context; 
        static string Connectionstring = "OracleDBConnectionDEV";
        private readonly IMapper _mapper;

        //public UsuarioRepository(IConfiguration config, DataContext context) 
        public UsuarioRepository(DataContext context, IConfiguration config, IMapper mapper)
            : base(context)
        {
            _config = config;
            _context = context;
        }

        //public T Adicion<T>(Usuarios_pw entity)
        //T Adicion<T>(Usuarios_pw entity)
        //public async Task<bool> Adicion(Usuarios_pw entity)

        //public Usuarios_pw Authenticate(Usuarios_pw model, string username, string password)
        //public AuthenticateResponse Authenticate(AuthenticateRequest model)
        
        public Usuarios_pw Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _context.USUARIOS_PW.SingleOrDefault(x => x.USUARIO == username);

            // check if username exists
            if (user == null)
                return null;
                        
            // check if password is correct
             if (!VerifyPasswordHash(password, Convert.FromBase64String(user.PASSWORDHASH), Convert.FromBase64String(user.PASSWORDSALT)))
                return null;

            // authentication successful
            return user; //user;
        }

        public Usuarios_pw Create(Usuarios_pw user, string password, string tokenverificacion)
        {
            //int result = 0;
            using IDbConnection db = new OracleConnection(_config.GetConnectionString(Connectionstring));

            var userr = _context.USUARIOS_PW.SingleOrDefault(x => x.USUARIO == user.USUARIO);

            try
            {
                if (db.State == ConnectionState.Closed)
                    db.Open();
                //using var tran = db.BeginTransaction();
                try
                {
                    if (string.IsNullOrWhiteSpace(password))
                        throw new AppException("La contraseña es requerida");

                    if (_context.USUARIOS_PW.Any(x => x.USUARIO == user.USUARIO))
                        throw new AppException("Usuario " + user.USUARIO + " ya esta dado de alta");
                        
                    if (_context.USUARIOS_PW.Any(x => x.CORREO == user.CORREO))
                        throw new AppException("Existe un Usuario registrado con el correo electrónico: " + user.CORREO);

                    //System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

                    byte[] passwordHash, passwordSalt;
                    CreatePasswordHash(password.Trim(), out passwordHash, out passwordSalt);

                    user.PASSWORDHASH = Convert.ToBase64String(passwordHash);
                    user.PASSWORDSALT = Convert.ToBase64String(passwordSalt);
                    user.FECHAMODIFICACION = DateTime.Now.ToLocalTime();
                    user.USUARIOMOD = "USRSYSMDN";
                    user.TOKENVERIFICACION = tokenverificacion;
                    user.FECHAVERIFICACIONCUENTA = DateTime.Now.ToLocalTime();

                    //DynamicParameters parameters = new DynamicParameters(); old

                    //var dp_params = new DynamicParameters();
                    //dp_params.Add("", user.IDUSUARIO);
                    //dp_params.Add("", user.NOMBRES);
                    //dp_params.Add("", user.APELLIDOS);
                    //dp_params.Add("", user.PASSWORDHASH);
                    //dp_params.Add("", user.FECHAMODIFICACION);
                    //dp_params.Add("", user.IDUSUARIOMOD);
                    //dp_params.Add("", user.PASSWORDSALT);
                    //dp_params.Add("", user.FECHAVERIFICACIONCUENTA);
                    //dp_params.Add("", user.TOKENVERIFICACION);
                    //dp_params.Add("", user.CORREO);
                    //dp_params.Add("", user.ESTADO);

                    //result = db.Query<T>("", param: dp_params, commandType: CommandType.StoredProcedure, transaction: tran).FirstOrDefault(); old

                    //result = db.Execute("", param: dp_params, commandType: CommandType.StoredProcedure, transaction: tran);
                    //tran.Commit();

                    //_context.USUARIOS_PW.Add(user);
                    //_context.SaveChanges();
                    Adicionar(user);

                }
                catch (Exception ex)
                {
                    //tran.Rollback();
                    throw ex;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                if (db.State == ConnectionState.Open)
                    db.Close();
            }

            return userr;
            
        }

        static string BytesToString(byte[] bytes)
        {
            using (MemoryStream stream = new MemoryStream(bytes))
            {
                using (StreamReader streamReader = new StreamReader(stream))
                {
                    return streamReader.ReadToEnd();
                }
            }
        }


        public bool FindEmailRegister(string email)
        {
            throw new NotImplementedException();
        }
        public bool VerifEmailUsuario(string tokenverif)
        {
          
            var user = _context.USUARIOS_PW.SingleOrDefault(x => x.TOKENVERIFICACION == tokenverif);

            if (user == null) {
                return false;
            };
            user.TOKENVERIFICACION = null;
            user.ESTADO = "CONFIRMADO";

            Update(user);

            // authentication successful
            return true; //user;
        }


        public static byte[] ToByteArray(string str)
        {
            byte[] bytes = new byte[str.Length * sizeof(char)];

            // Unicode endoding
            Buffer.BlockCopy(str.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }

        //public static int Adicion(RegisterRequest entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public void Add(Usuarios_pw entity)
        //{

        //    try
        //    {
        //        //DynamicParameters parameters = new DynamicParameters();
        //        //parameters.Add("@CustomerName", entity.CustomerName);
        //        //parameters.Add("@CustomerEmail", entity.CustomerEmail);
        //        //parameters.Add("@CustomerMobile", entity.CustomerMobile);
        //        //SqlMapper.Execute(con, "AddCustomer", param: parameters, commandType: StoredProcedure);

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}


        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");


            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(enc.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("El Clave no puede estar vacía o tener espacios en blanco", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                System.Text.ASCIIEncoding enc = new System.Text.ASCIIEncoding();

                byte[] computedHash = hmac.ComputeHash(enc.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }


    }
}
