using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using reactBackend.Context;
using reactBackend.Models;

namespace reactBackend.Repository
{
    public class AlumnoDAO
    {
        #region Context
        public RegistroAlumnosContext contexto = new RegistroAlumnosContext();
        #endregion

        #region SelectAll
        public List<Alumno> SelectAll()
        {
            var alumno = contexto.Alumnos.ToList<Alumno>();
            return alumno;
        }
        #endregion

        #region GetByID
        public Alumno GetByID(int id)
        {
            var alumno = contexto.Alumnos.Where(x => x.Id == id).FirstOrDefault();
            return alumno == null ? null : alumno;
        }
        #endregion

        #region InsertAlumno
        public bool InsertAlumno(Alumno alumno)
        {
            try
            {
                var alum = new Alumno()
                {
                    Nombre = alumno.Nombre,
                    Dni = alumno.Dni,
                    Direccion = alumno.Direccion,
                    Email = alumno.Email
                };

                contexto.Alumnos.Add(alum);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region UpdateAlumno
        public bool UpdateAlumno(int id, Alumno alumno)
        {
            try
            {
                var alumnoUpdate = GetByID(id);
                if (alumnoUpdate == null)
                {
                    return false;
                }

                alumnoUpdate.Nombre = alumno.Nombre;
                alumnoUpdate.Dni = alumno.Dni;
                alumnoUpdate.Direccion = alumno.Direccion;
                alumnoUpdate.Email = alumno.Email;
                contexto.SaveChanges();
                return true;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.InnerException);
                return false;
            }
        }
        #endregion

        #region DeleteAlumno
        public bool DeleteAlumno(int id)
        {
            try
            {
                var alumno = GetByID(id);
                if (alumno == null)
                {
                    return false;
                }

                contexto.Alumnos.Remove(alumno);
                contexto.SaveChanges();
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
