using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;

namespace CoreApiWeaponRegister
{
    public static class Utils
    {
        public static DateTime ObtenerFechaHoraActual()
        {
            return DateTime.Parse(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss.fff", CultureInfo.CreateSpecificCulture("es-SV")), CultureInfo.CreateSpecificCulture("es-SV"));
        }
    }
}
