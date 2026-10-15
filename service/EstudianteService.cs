using GestionEstudiante.model;
using GestionEstudiante.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEstudiante.service
{
    public class EstudianteService
    {
        private EstudianteRepository repository = new EstudianteRepository();


        public EstudianteService() { }


        public void Agregar(Estudiante estudiante)
        {
            repository.Agregar(estudiante);
        }

        public void Modificar(Estudiante estudiante)
        {
            repository.Modificar(estudiante);
        }
        public void Eliminar(int id)
        {
            repository.Eliminar(id);
        }

        public Estudiante[] Listar()
        {
            return repository.Listar();
        }

        public Estudiante ObtenerPorId(int id)
        {
            return repository.ObtenerPorId(id);
        }

        public int Conteo()
        {
            return repository.Conteo();
        }

    }
}
