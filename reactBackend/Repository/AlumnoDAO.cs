using reactBackend.Context;
using reactBackend.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public Alumno? GetByID(int id)
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
                // Debemos verificar el id del alumno
                var alumno = contexto.Alumnos.Where(x => x.Id == id).FirstOrDefault();
                if (alumno != null)
                {
                    // matriculaAlumno == idALumno
                    var matriculaA = contexto.Matriculas.Where(x => x.AlumnoId == alumno.Id).ToList();
                    Console.WriteLine("Alumno  encontrado");
                    //Traemos la calificaciones asociadas a esa matricula 
                    foreach (Matricula m in matriculaA)
                    {
                        var calificacion = contexto.Calificacions.Where(x => x.MatriculaId == m.Id).ToList();
                        Console.WriteLine("Matricula encontrada");
                        contexto.Calificacions.RemoveRange(calificacion);

                    }
                    contexto.Matriculas.RemoveRange(matriculaA);
                    contexto.Alumnos.Remove(alumno);
                    contexto.SaveChanges();
                    return true;
                }
                else
                {
                    Console.WriteLine("Alumno no encontrado");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;

            }
        }
        #endregion

        #region LeftJoin
        public List<AlumnoAsignatura> GetAlumnoAsignatura()
        {
            var query = from a in contexto.Alumnos
                        join m in contexto.Matriculas on a.Id equals m.AlumnoId
                        join asig in contexto.Asignaturas on m.Id equals asig.Id
                        select new AlumnoAsignatura
                        {
                            nombreAlumno = a.Nombre,
                            nombreAsignatura = asig.Nombre
                        };

            return query.ToList();
        }
        #endregion

        #region AlumnoProfesor
        public List<AlumnoProfesor> AlumnoProfesors(string nombreProfesor)
        {
            var listadoALumno = from a in contexto.Alumnos
                                join m in contexto.Matriculas on a.Id equals m.AlumnoId
                                join asig in contexto.Asignaturas on m.Id equals asig.Id
                                where asig.Profesor == nombreProfesor
                                select new AlumnoProfesor
                                {
                                    Id = a.Id,
                                    Dni = a.Dni,
                                    Nombre = a.Nombre,
                                    Direccion = a.Direccion,
                                    Edad = a.Edad,
                                    Correo = a.Email,
                                    Asignatura = asig.Nombre
                                };

            return listadoALumno.ToList();
        }
        #endregion

        #region AlumnoDNI
        public Alumno DNIAlumno(Alumno alumno)
        {
            var alumnos = contexto.Alumnos.Where(x => x.Dni == alumno.Dni).FirstOrDefault();
            return alumnos == null ? null : alumnos;
        }
        #endregion

        #region AlumnoMatricula
        public bool InsertarMatricula(Alumno alumno, int idAsing)
        {
            try
            {
                var alumnoDNI = DNIAlumno(alumno);
                if (alumnoDNI == null)
                {
                    InsertAlumno(alumno);
                    var alumnoInsertado = DNIAlumno(alumno); // Recuperamos el alumno con el ID generado

                    if (alumnoInsertado == null)
                        return false; // Evitamos errores si el alumno no se insertó correctamente

                    var unirAlumnoMatricula = MatriculaAsignaturaALumno(alumnoInsertado, idAsing);
                    return unirAlumnoMatricula; // Devolvemos el resultado de la matrícula
                }
                else
                {
                    return MatriculaAsignaturaALumno(alumnoDNI, idAsing);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error en InsertarMatricula: {ex.Message}");
                return false;
            }
        }
        #endregion

        #region Matriucla
        public bool MatriculaAsignaturaALumno(Alumno alumno, int idAsignatura)
        {
            try
            {
                Matricula matricula = new Matricula();
                matricula.AlumnoId = alumno.Id;
                matricula.Id= idAsignatura;
                contexto.Matriculas.Add(matricula);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }
        #endregion

    }
}