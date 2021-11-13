using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TipoProyectoController : ControllerBase
    {
        private readonly TPI_DBContext context;

        public TipoProyectoController(TPI_DBContext context)
        {
            this.context = context;

        }
        [AllowAnonymous]
        // GET: api/<TipoProyectoController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            try {
                return Ok(context.TipoProyects.ToList());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<TipoProyectoController>/5
        [HttpGet("{id}", Name = "getTipoProyecto")]
        public ActionResult Get(int id)
        {
            try {
                var cursada = context.TipoProyects.FirstOrDefault(p => p.IdTipoProyecto == id);
                return Ok(cursada);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] TipoProyect tipoProyecto)
        {
            try {
                var tipoProyectoExist = context.TipoProyects.FirstOrDefault(p => p.IdTipoProyecto == tipoProyecto.IdTipoProyecto);
                if (tipoProyectoExist != null) {
                    return BadRequest("Tipo Proyecto existente");
                }
                else {
                    context.TipoProyects.Add(tipoProyecto);
                    context.SaveChanges();
                    return CreatedAtRoute("Getproyecto", new { id = tipoProyecto.IdTipoProyecto }, tipoProyecto);
                }
            }
            catch (Exception ex) {
                return BadRequest("Error al insertar el registro, validar los datos requeridos");
            }
        }

        // PUT api/<TipoProyectoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TipoProyect tipoProyecto)
        {
            try {
                if (tipoProyecto.IdTipoProyecto == id) {
                    context.Entry(tipoProyecto).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("Getproyecto", new { id = tipoProyecto.IdTipoProyecto }, tipoProyecto);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<TipoProyectoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try {
                var tipoProyecto = context.TipoProyects.FirstOrDefault(p => p.IdTipoProyecto == id);
                if (tipoProyecto != null) {
                    context.TipoProyects.Remove(tipoProyecto);
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

