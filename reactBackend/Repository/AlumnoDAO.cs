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

        #region SelectByID
        public Alumno? GetByID(int id)
        {
            var alumno = contexto.Alumnos.Where(x => x.Id == id).FirstOrDefault();
            return alumno == null ? null : alumno;
        }
        #endregion

    }
}
