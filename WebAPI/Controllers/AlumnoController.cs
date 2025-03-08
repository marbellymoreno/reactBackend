using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reactBackend.Models;
using reactBackend.Repository;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AlumnoController : ControllerBase
    {
        private AlumnoDAO _alDAO = new AlumnoDAO();

        #region AlumnoProfesor
        [HttpGet("alumnoProfesor")]
        public List<AlumnoProfesor> GetAlumnoProfesor(string usuario)
        {
            return _alDAO.AlumnoProfesors(usuario);
        }
        #endregion

        #region GetByID
        [HttpGet("alumno")]
        public Alumno SelectById(int id)
        {
            var alumno = _alDAO.GetByID(id);
            return alumno;
        }
        #endregion

        #region UpdatAlumno
        [HttpPut("alumno")]
        public bool ActualizarAlumno([FromBody] Alumno alum)
        {
            return _alDAO.UpdateAlumno(alum.Id, alum);
        }
        #endregion

        #region AlumnoMAtricula
        [HttpPost("alumno")]
        public bool InsertarMatricula([FromBody] Alumno alumno, int idAsignatura)
        {
            return _alDAO.InsertarMatricula(alumno, idAsignatura);
        }
        #endregion
    }
}