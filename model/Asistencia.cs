using GestionEstudiante.util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEstudiante.model
{
    public class Asistencia: ModeloBase
    {
        public int id { get; set; }
        public string codigoEstudiante { get; set; } = "";
        public DateTime fecha { get; set; }
        public string estado { get; set; } = "Sin registro";

        public override string[] ObtenerCampos()
        {
            return new string[]
            {
                id.ToString(),
                codigoEstudiante,
                fecha.ToString("yyyy-MM-dd"),
                estado
            };
        }
    }
    
}
