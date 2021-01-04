using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ProyectoEscuela.Models;

namespace ProyectoEscuela
{
    [Route("[controller]")]
    public class CursoController: Controller
    {
        private EscuelaContext _context;

        public CursoController(EscuelaContext context){
            _context = context;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Fecha = DateTime.UtcNow.ToString();

            return View("MultiCurso",_context.Cursos);
        }
        
        [HttpGet("{cursoId}")]
        public IActionResult Index(string cursoId)
        {
            var curso = from cur in _context.Cursos
                                where cur.Id == cursoId
                                select cur;

            return View(curso.SingleOrDefault());
        }

        [Route("/[controller]/Create")]
        public IActionResult Create()
        {
            ViewBag.Fecha = DateTime.UtcNow.ToString();

            return View();
        }

        [Route("/[controller]/Create")]
        [HttpPost]
        public IActionResult Create(Curso curso)
        {
            ViewBag.Fecha = DateTime.UtcNow.ToString();

            if(ModelState.IsValid)
            {
                var escuela = _context.Escuelas.FirstOrDefault();

                curso.EscuelaId = escuela.Id;
                _context.Cursos.Add(curso);
                _context.SaveChanges();
                ViewBag.mensaje = "Curso creado";
                return View("Index",curso);
            }
            else
            {
                return View(curso);
            }

        }

        [Route("/[controller]/Edit/{Id}")]
        [HttpGet]
        public IActionResult Edit(string Id)
        {
            ViewBag.Fecha = DateTime.UtcNow.ToString();

            var curso = from cur in _context.Cursos
                            where cur.Id == Id
                            select cur;

            if(curso.FirstOrDefault() != null)
            {
                return View(curso.FirstOrDefault());
            }
            else
            {
                return View("MultiCurso",_context.Cursos);
            }

        }

        [Route("/[controller]/Edit/{Id}")]
        [HttpPost]
        public IActionResult Edit(Curso curso)
        {
            ViewBag.Fecha = DateTime.UtcNow.ToString();

            if(ModelState.IsValid)
            {
                var editCurso = (from cur in _context.Cursos
                            where cur.Id == curso.Id
                            select cur).FirstOrDefault();

                if(editCurso==null){
                    return View("MultiCurso",_context.Cursos);
                }

                editCurso.Nombre = curso.Nombre;
                editCurso.Dirección = curso.Dirección;
                editCurso.Jornada = curso.Jornada;

                _context.Update(editCurso);
                _context.SaveChanges();
                ViewBag.mensaje = "Curso Editado";
                return View("Index",curso);
            }
            else
            {
                return View(curso);
            }

        }

        [Route("/[controller]/Delete/{Id}")]
        public IActionResult Delete(string Id, string Nombre)
        {
            ViewBag.Fecha = DateTime.UtcNow.ToString();
            ViewBag.Nombre = Nombre;
            ViewBag.Id = Id;
            
            return View();
        }

        [Route("/[controller]/Delete/{Id}")]
        [HttpPost]
        public IActionResult Delete(string Id)
        {

            var curso = (from cur in _context.Cursos
                            where cur.Id == Id
                            select cur).FirstOrDefault();

            if(curso != null)
            {
                _context.Remove(curso);
                _context.SaveChanges();

            }

            return View("MultiCurso",_context.Cursos);
        }
    }
}