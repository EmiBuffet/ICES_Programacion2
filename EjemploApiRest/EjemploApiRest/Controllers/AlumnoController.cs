﻿using Microsoft.AspNetCore.Mvc;

namespace EjemploApiRest.Controllers
{
    [ApiController]
    [Route("api/alumnos")]
    public class AlumnoController : ControllerBase
    {
        static List<Alumno> alumnoList = new List<Alumno>
        {
            new Alumno(1, "Juan", "Perez"),
            new Alumno(2, "Lionel", "Messi")
        };

        [HttpGet]
        public List<Alumno> ConsultarAlumnos()
        {
            return alumnoList;
        }

        [HttpPost]
        public int CrearAlumno(Alumno alumno)
        {
            alumnoList.Add(alumno);
            return alumno.Id;
        }

        [HttpPut("{id}")]
        public ActionResult ActualizarAlumno(int id, Alumno alumno)
        {
            var alumnoActualizar = alumnoList.Find(x => x.Id == id);

            if (alumnoActualizar == null)
            {
                return NotFound();
            }

            alumnoActualizar.Nombre = alumno.Nombre;
            alumnoActualizar.Apellido = alumno.Apellido;

            return Ok();
        }
    }
}