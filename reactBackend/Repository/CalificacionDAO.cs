using reactBackend.Context;
using reactBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reactBackend.Repository
{
    public class CalificacionDAO
    {
        private RegistroAlumnosContext _contexto = new RegistroAlumnosContext();

        #region GetCalificaciones
        public List<Calificacion> Seleccion(int matriculaid)
        {

            var matricula = _contexto.Matriculas.Where(x => x.Id == matriculaid);
            Console.WriteLine("matricula encontrada");
            try
            {
                if (matricula != null)
                {

                    var calificacion = _contexto.Calificacions.Where(x => x.Id == matriculaid).ToList();
                    return calificacion;
                }
                else
                {

                    return null;
                }

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                return null;
            }
        }
        #endregion

        #region InsertCalificacion
        public bool InsertCalificacion(Calificacion calificacion)
        {
            try
            {
                if (calificacion == null)
                {
                    return false;
                }
                _contexto.Calificacions.Add(calificacion);
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }
        #endregion

        #region DeleteCalificacion
        public bool EliminarCalificacion(int id)
        {
            try
            {
                var calificacion = _contexto.Calificacions.Where(x => x.Id == id).FirstOrDefault();
                if (calificacion == null)
                {
                    return false;
                }

                _contexto.Calificacions.Remove(calificacion);
                _contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;

            }
        }
        #endregion
    }
}
