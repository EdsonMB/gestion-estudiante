using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEstudiante.model;

public abstract class ModeloBase
{
    public int id { get; set; }
    public abstract string[] ObtenerCampos();

}
