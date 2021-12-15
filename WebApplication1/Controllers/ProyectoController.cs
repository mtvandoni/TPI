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
                var proyectos = from pro in context.Proyectos
                              join eq in context.Equipos on pro.IdProyecto equals eq.IdProyecto
                              // join equiper in context.EquipoPersonas on eq.IdEquipo equals equiper.IdEquipo
                              // join per in context.Personas on equiper.IdPersona equals per.Id
                              join cat in context.Categoria on pro.IdCategoria equals cat.IdCategoria
                              join tp in context.TipoProyects on pro.IdTipoProyecto equals tp.IdTipoProyecto
                              select new {
                                  idProyecto = pro.IdProyecto,
                                  nombreEquipo = eq.Nombre,
                                  nombreProyecto = pro.Nombre,
                                  pro.Descripcion, pro.PropuestaValor, pro.Repositorio, pro.CantMeGusta, pro.RutaFoto, pro.RutaVideo,
                                  pro.IdRed,
                                  idTipoProyecto = pro.IdTipoProyecto,
                                  categoria = cat.Descripcion,
                                  tipoProyecto = tp.Descripcion,
                                  estado = eq.Estado,
                                  idEquipo = eq.IdEquipo,
                              };
                return Ok(proyectos);
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

                    proyecto.RutaFoto = proyecto.RutaFoto;
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

        [HttpGet("MasVotados")]
        public ActionResult GetMasVotados()
        {

            var proyectos = (from pro in context.Proyectos
                            orderby pro.CantMeGusta descending
                            select pro).Take(3);

            return Ok(proyectos);
        }
    }
}