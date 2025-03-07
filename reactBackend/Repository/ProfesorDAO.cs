using reactBackend.Context;
using reactBackend.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reactBackend.Repository
{
    public class ProfesorDAO
    {
        #region Context
        public RegistroAlumnosContext context = new RegistroAlumnosContext();
        #endregion

        public Profesor login(string usuario, string pass)
        {
            var prof = context.Profesors.Where(p => p.Usuario == usuario && p.Password == pass).FirstOrDefault();

            return prof;
        }
    }
}
