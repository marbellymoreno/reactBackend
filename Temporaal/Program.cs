using reactBackend.Repository;

namespace Temporal
{
    class Program
    {
        static void Main(string[] args)
        {
            AlumnoDAO alumnoDAO = new AlumnoDAO();
            #region SelectAll
            var alumnos = alumnoDAO.SelectAll();
            foreach (var alumno in alumnos)
            {
                Console.WriteLine(alumno.Nombre);
            }
            #endregion

            #region GetByID
            var selectByID = alumnoDAO.GetByID(1);
            Console.WriteLine(selectByID?.Nombre);
            #endregion
        }
    }
}