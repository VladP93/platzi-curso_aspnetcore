using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProyectoEscuela
{
    public class Curso:ObjetoEscuelaBase
    {
        [Required]
        public override string Nombre { get; set; }
        public TiposJornada Jornada { get; set; }
        public List<Asignatura> Asignaturas{ get; set; }
        public List<Alumno> Alumnos{ get; set; }
        
        [Required]
        public string Dirección { get; set; }

        public string EscuelaId { get; set; }

        public Escuela Escuela { get; set; }
        
    }
}