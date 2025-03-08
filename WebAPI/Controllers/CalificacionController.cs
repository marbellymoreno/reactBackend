using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reactBackend.Models;
using reactBackend.Repository;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class CalificacionController : ControllerBase
    {
        private CalificacionDAO _cali = new CalificacionDAO();

        #region GetCalificaciones
        [HttpGet("calificaciones")]
        public List<Calificacion> GetCalificaciones(int idMatricula)
        {
            return _cali.Seleccion(idMatricula);
        }
        #endregion

        #region PostCalificaciones
        [HttpPost("calificaciones")]
        public bool Insertar([FromBody] Calificacion calificacion)
        {
            return _cali.InsertCalificacion(calificacion);
        }
        #endregion

        #region DeleteCalificaciones
        [HttpDelete("calificaciones")]
        public bool DeleteCalificacion(int idCalificacion)
        {
            return _cali.EliminarCalificacion(idCalificacion);
        }
        #endregion
    }
}