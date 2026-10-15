using GestionEstudiante.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionEstudiante.repository
{
    public class EstudianteRepository
    {

        private const int MAX_ESTUDIANTES = 10;
        private Estudiante[] estudiantes = new Estudiante[MAX_ESTUDIANTES];//BASE DE DATOS


        private int ObtenerPosicion()
        {
            int conteo = 0;
            for (int i = 0; i < estudiantes.Length; i++)
            {
                if (estudiantes[i] != null)
                {
                    conteo++;
                }
            }

            return conteo +1;
        }

        private string GenerarCodigo()
        {
            int posicion = ObtenerPosicion(); // 1
            return "E"+ posicion.ToString("000000"); // "E" + "000010"
        }

        private void ReOrdenar() //ordenamiento burbuja
        {
            for (int i = 0; i < estudiantes.Length - 1; i++)
            {
                for (int j = 0; j < estudiantes.Length - 1 - i; j++)
                {
                    if (estudiantes[j] == null && estudiantes[j + 1] != null)
                    {
                        Estudiante aux = estudiantes[j];
                        estudiantes[j] = estudiantes[j+1];
                        estudiantes[j + 1] = aux;
                    }
                }
            }
        }

        public void Agregar(Estudiante estudiante)
        {
            int posicion = ObtenerPosicion(); // 1
            estudiante.codigo = GenerarCodigo(); // E000001
            estudiante.id = posicion; //1
            //lo guardamos en la posicion 0 = posicion(1) - 1
            this.estudiantes[posicion - 1] = estudiante;
        }

        public void Modificar(Estudiante estudiante) 
        {
            this.estudiantes[estudiante.id-1] = estudiante;
        }


        public void Eliminar(int id)
        {
            this.estudiantes[id-1] = null;
            ReOrdenar();
        }

        public Estudiante ObtenerPorId(int id)
        {
            Estudiante estudiante = null;
            for (int i = 0; i < this.estudiantes.Length; i++)
            {
                if (estudiantes[i] != null && estudiantes[i].id == id)
                {
                    estudiante = this.estudiantes[i];
                }
            }

            return estudiante;
        }

        public Estudiante[] Listar() // te devuelve todo el arreglo
        {
            return this.estudiantes;
        }

        public int Conteo() // obtiene la cantida de estudiantes del arreglo
        {
            return ObtenerPosicion() - 1;
        }

    }
}
