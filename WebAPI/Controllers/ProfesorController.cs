using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using reactBackend.Models;
using reactBackend.Repository;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ProfesorController : ControllerBase
    {
        private ProfesorDAO _proDAO = new ProfesorDAO();
        [HttpGet("autentificacion")]

        public string loginProfesor([FromBody] Profesor prof)
        {
            var prof1 = _proDAO.login(prof.Usuario, prof.Password);
            if (prof1 != null)
            {
                return prof1.Usuario;
            }
            else
            {
                return "Usuario o contraseña incorrectos";
            }
        }
    }
}