using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace ProyectoEscuela.Models
{
    public class EscuelaContext: DbContext
    {
        public DbSet<Escuela> Escuelas { get; set; }
        public DbSet<Asignatura> Asignaturas { get; set; }
        public DbSet<Alumno> Alumnos { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<Evaluación> Evaluaciones { get; set; }

        public EscuelaContext ( DbContextOptions<EscuelaContext> options):base (options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var escuela = new Escuela();

            escuela.AñoDeCreación = 2020;
            escuela.Id = Guid.NewGuid().ToString();
            escuela.Nombre = "Escuela de caballeros Jedi Paniagua";
            escuela.Dirección = "En la cima del cerro tecana";
            escuela.Ciudad = "Santa Ana";
            escuela.Pais = "El Salvador";
            escuela.TipoEscuela = TiposEscuela.Secundaria;

            //Cargar cursos por escuela
            var cursosescuela = CargarCursos(escuela);

            //Cargar asignaturas por curso
            var asignaturas = CargarAsignaturas(cursosescuela);

            //Cargar alumnos por curso
            var alumnoscursos = CargarAlumno(cursosescuela);

            //Cargar evaluaciones por alumno

            //Sembrar datos
            modelBuilder.Entity<Escuela>().HasData(escuela);
            modelBuilder.Entity<Curso>().HasData(cursosescuela.ToArray());
            modelBuilder.Entity<Asignatura>().HasData(asignaturas.ToArray());
            modelBuilder.Entity<Alumno>().HasData(alumnoscursos.ToArray());

        }

        private List<Alumno> CargarAlumno(List<Curso> cursos)
        {
            var listaAlumnos = new List<Alumno>();

            Random random = new Random();
            foreach (var curso in cursos)
            {
                int cantRandom = random.Next(5,20);
                var tempList = GenerarAlumnosAlAzar(curso, cantRandom);
                listaAlumnos.AddRange(tempList);
            }

            return listaAlumnos;
        }

        private static List<Asignatura> CargarAsignaturas(List<Curso> cursosescuela)
        {
            List<Asignatura> listAsignaturas = new List<Asignatura>();
            foreach (var curso in cursosescuela)
            {
                var tempList = new List<Asignatura> {
                    new Asignatura
                    {
                        Nombre = "Matemáticas",
                        CursoId = curso.Id,
                        Id = Guid.NewGuid().ToString()
                    },
                    new Asignatura
                    {
                        Nombre = "Educación Física",
                        CursoId = curso.Id,
                        Id = Guid.NewGuid().ToString()
                    },
                    new Asignatura
                    {
                        Nombre = "Castellano",
                        CursoId = curso.Id,
                        Id = Guid.NewGuid().ToString()
                    },
                    new Asignatura
                    {
                        Nombre = "Ciencias Naturales",
                        CursoId = curso.Id,
                        Id = Guid.NewGuid().ToString()
                    },
                    new Asignatura
                    {
                        Nombre = "Programacion",
                        CursoId = curso.Id,
                        Id = Guid.NewGuid().ToString()
                    }
                };
                listAsignaturas.AddRange(tempList);
            }
            return listAsignaturas;
        }

        private static List<Curso> CargarCursos(Escuela escuela)
        {
            return new List<Curso>(){
                new Curso() {
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela.Id,
                    Nombre = "101",
                    Jornada = TiposJornada.Tarde,
                    Dirección = "la escuelita de la esquina"
                },
                new Curso() {
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela.Id,
                    Nombre = "201",
                    Jornada = TiposJornada.Mañana,
                    Dirección = "la escuelita de la esquina"
                },
                new Curso() {
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela.Id,
                    Nombre = "301",
                    Jornada = TiposJornada.Mañana,
                    Dirección = "la escuelita de la esquina"
                },
                new Curso() {
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela.Id,
                    Nombre = "401",
                    Jornada = TiposJornada.Tarde,
                    Dirección = "la escuelita de la esquina"
                },
                new Curso() {
                    Id = Guid.NewGuid().ToString(),
                    EscuelaId = escuela.Id,
                    Nombre = "501",
                    Jornada = TiposJornada.Noche,
                    Dirección = "la escuelita de la esquina"
                }
            };
        }

        private List<Alumno> GenerarAlumnosAlAzar(Curso curso, int carntidadAlumnos)
        {
            string[] nombre1 = { "Alba", "Felipa", "Eusebio", "Farid", "Donald", "Alvaro", "Nicolás" };
            string[] apellido1 = { "Ruiz", "Sarmiento", "Uribe", "Maduro", "Trump", "Toledo", "Herrera" };
            string[] nombre2 = { "Freddy", "Anabel", "Rick", "Murty", "Silvana", "Diomedes", "Nicomedes", "Teodoro" };

            var listaAlumnos = from n1 in nombre1
                               from n2 in nombre2
                               from a1 in apellido1
                               select new Alumno {
                                   CursoId = curso.Id,
                                   Nombre = $"{n1} {n2} {a1}",
                                   Id = Guid.NewGuid().ToString()
                                };

            return listaAlumnos.OrderBy((al) => al.Id).Take(carntidadAlumnos).ToList();
        }
        
    }
}