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
    public class TipoPersonaController : ControllerBase
    {
        private readonly TPI_DBContext context;

        public TipoPersonaController(TPI_DBContext context)
        {
            this.context = context;

        }
        [AllowAnonymous]
        // GET: api/<TipoPersonaController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            try {
                return Ok(context.TipoPersonas.ToList());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<TipoPersonaController>/5
        [HttpGet("{id}", Name = "getTipoPersona")]
        public ActionResult Get(int id)
        {
            try {
                var cursada = context.TipoPersonas.FirstOrDefault(p => p.IdTipo == id);
                return Ok(cursada);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] TipoPersona tipoPersona)
        {
            try {
                var TipoPerNuevo = context.TipoPersonas.FirstOrDefault(p => p.IdTipo == tipoPersona.IdTipo);
                if (TipoPerNuevo != null) {
                    return BadRequest("Tipo existente");
                }
                else {
                    context.TipoPersonas.Add(tipoPersona);
                    context.SaveChanges();
                    return CreatedAtRoute("Getpersona", new { id = tipoPersona.IdTipo }, tipoPersona);
                }
            }
            catch (Exception ex) {
                return BadRequest("Error al insertar el registro, validar los datos requeridos");
            }
        }

        // PUT api/<TipoPersonaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] TipoPersona tipoPersona)
        {
            try {
                if (tipoPersona.IdTipo == id) {
                    context.Entry(tipoPersona).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("Getpersona", new { id = tipoPersona.IdTipo }, tipoPersona);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<TipoPersonaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try {
                var tipoPersona = context.TipoPersonas.FirstOrDefault(p => p.IdTipo == id);
                if (tipoPersona != null) {
                    context.TipoPersonas.Remove(tipoPersona);
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

