using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTO;
using WebApplication1.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProyectoController : ControllerBase
    {
        private readonly TPI_DBContext context;

        public ProyectoController(TPI_DBContext context)
        {
            this.context = context;
        }

        // GET: api/<ProyectoController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            try {
                return Ok(context.Proyectos.ToList());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<ProyectoController>/5
        [HttpGet("{id}", Name = "getProyecto")]
        public ActionResult Get(int id)
        {
            try {
                var persona = context.Proyectos.FirstOrDefault(p => p.IdProyecto == id);
                return Ok(persona);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<ProyectoController>
        [HttpPost]
        public ActionResult Post([FromBody] Proyecto proyecto)
        {

            try {
                var pro = context.Proyectos.FirstOrDefault(p => p.IdProyecto == proyecto.IdProyecto);
                if (pro != null) {
                    return BadRequest("Proyecto existente");
                }
                else {

                    string path = @"D:\Proyectos\" + proyecto.Nombre;
                    try {
                        // Determina si el directorio existe
                        if (Directory.Exists(path)) {
                            Console.WriteLine("Directorio Existente.");
                        }
                        else {
                            // Try to create the directory.
                            DirectoryInfo di = Directory.CreateDirectory(path);
                            Console.WriteLine("El Directorio fue creado correctamente en {0}.", Directory.GetCreationTime(path));
                        }
                    }
                    catch (Exception e) {
                        Console.WriteLine("The process failed: {0}", e.ToString());
                    }

                    proyecto.RutaFoto = path;
                    context.Proyectos.Add(proyecto);
                    context.SaveChanges();
                    return CreatedAtRoute("Getproyecto", new { id = proyecto.IdProyecto }, proyecto);
                }
            }
            catch (Exception ex) {
                return BadRequest("Error al insertar el registro, validar los datos requeridos" + "[" + ex + "]");
            }

        }

        // PUT api/<ProyectoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Proyecto proyecto)
        {
            try {
                if (proyecto.IdProyecto == id) {
                    context.Entry(proyecto).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("Getproyecto", new { id = proyecto.IdProyecto }, proyecto);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("MeGusta")]
        public ActionResult MeGusta([FromBody] ProyectoDTO proyectoDTO)
        {
            try {
                var proyecto = context.Proyectos.FirstOrDefault(p => p.IdProyecto == proyectoDTO.Id);
                if (proyecto != null) {
                    if (proyectoDTO.FlagMeGusta.Equals("SI")) {
                        proyecto.CantMeGusta = proyecto.CantMeGusta + 1;
                    }
                    else {
                        proyecto.CantMeGusta = proyecto.CantMeGusta - 1;
                    }
                    context.Entry(proyecto).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("Getproyecto", new { id = proyecto.IdProyecto }, proyecto);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<ProyectoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try {
                var proyecto = context.Proyectos.FirstOrDefault(p => p.IdProyecto == id);
                if (proyecto != null) {
                    context.Proyectos.Remove(proyecto);
                    context.SaveChanges();
                    return Ok(id);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }
    }
}