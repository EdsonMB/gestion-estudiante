using System;
using System.Collections.Generic;
using GestionEstudiante.model;

namespace GestionEstudiante.repository
{
    public class AsistenciaRepository
    {
        private readonly GestorBaseDatos gestor;
        private readonly List<Asistencia> registros;

        public AsistenciaRepository(GestorBaseDatos gestor)
        {
            this.gestor = gestor;
            this.registros = new List<Asistencia>();
        }

        public List<Asistencia> Listar()
        {
            return registros;
        }

        public void Guardar(Asistencia item)
        {
            if (item == null)
            {
                return;
            }

            for (int i = 0; i < registros.Count; i++)
            {
                if (string.Equals(registros[i].codigoEstudiante, item.codigoEstudiante, StringComparison.OrdinalIgnoreCase)
                    && registros[i].fecha.Date == item.fecha.Date)
                {
                    item.id = registros[i].id;
                    item.fecha = item.fecha.Date;
                    registros[i] = item;
                    return;
                }
            }

            item.id = registros.Count + 1;
            item.fecha = item.fecha.Date;
            registros.Add(item);
        }
    }
}