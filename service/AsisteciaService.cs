using GestionEstudiante.model;
using GestionEstudiante.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static GestionEstudiante.model.Asistencia;

namespace GestionEstudiante.service
{
    namespace GestionEstudiante.service
    {
        public class AsistenciaService
        {
            private readonly AsistenciaRepository repository;

            public AsistenciaService(AsistenciaRepository repository)
            {
                this.repository = repository;
            }

            public List<Asistencia> Listar()
            {
                return repository.Listar();
            }

            public void Guardar(Asistencia item)
            {
                if (item == null)
                {
                    throw new ArgumentNullException("item", "No hay datos de asistencia para guardar.");
                }

                Asistencia copia = new Asistencia();
                copia.id = item.id;
                copia.codigoEstudiante = (item.codigoEstudiante ?? "").Trim();
                copia.fecha = item.fecha.Date;
                copia.estado = (item.estado ?? "").Trim();

                if (string.IsNullOrWhiteSpace(copia.codigoEstudiante))
                {
                    throw new ArgumentException("El código del estudiante es obligatorio.");
                }

                if (copia.estado != "Presente" && copia.estado != "Falta" && copia.estado != "Tardanza")
                {
                    throw new ArgumentException("Seleccione un estado válido: Presente, Falta o Tardanza.");
                }

                repository.Guardar(copia);
            }
        }
    }
}
