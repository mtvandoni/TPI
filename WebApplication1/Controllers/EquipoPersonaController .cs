using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class EquipoPersonaController : ControllerBase
    {
        private readonly TPI_DBContext context;

        public EquipoPersonaController(TPI_DBContext context)
        {
            this.context = context;

        }

        [AllowAnonymous]
        // GET: api/<EquipoPersonaController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            try {
                return Ok(context.EquipoPersonas.ToList());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        // GET api/<EquipoPersonaController>/5
        [HttpGet("{id}", Name = "getequipoPersonas")]
        public ActionResult Get(int id)
        {
            try {
                var cursada = context.EquipoPersonas.FirstOrDefault(p => p.IdEquipo == id);
                return Ok(cursada);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] EquipoPersona equipoPersona)
        {
            try {
                var equipoPersonaExist = context.EquipoPersonas.FirstOrDefault(p => p.IdEquipo == equipoPersona.IdEquipo && p.IdPersona == equipoPersona.IdPersona);
                if (equipoPersonaExist != null) {
                    return BadRequest("Relacion Equipo- personas existente");
                }
                else {
                    context.EquipoPersonas.Add(equipoPersona);
                    context.SaveChanges();
                    return CreatedAtRoute("GetequipoPersonas", new { id = equipoPersona.IdEquipo }, equipoPersona);
                }
            }
            catch (Exception ex) {
                return BadRequest("Error al insertar el registro, validar los datos requeridos");
            }
        }

        [HttpPost("AsiganrIntegrantes")]
        public ActionResult Post([FromBody] List<EquipoPersona> EquipoPersona)
        {
            try {
                foreach (EquipoPersona integrante in EquipoPersona) {
                    var equipoExist = context.EquipoPersonas.FirstOrDefault(p => p.IdEquipo == integrante.IdEquipo && p.IdPersona == integrante.IdPersona);
                    if (equipoExist == null) {
                        context.EquipoPersonas.Add(integrante);
                        context.SaveChanges();
                    }
                }
                return Ok("integrantes Asignados correctamente");
            }
            catch (Exception ex) {
                return BadRequest("Error al insertar el registro, validar los datos requeridos");
            }
        }

        // PUT api/<EquipoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] EquipoPersona EquipoPersona)
        {
            try {
                if (EquipoPersona.IdEquipo == id) {
                    context.Entry(EquipoPersona).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetequipoPersona", new { id = EquipoPersona.IdEquipo }, EquipoPersona);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<EquipoPersonaController>/5
        [HttpDelete]
        public ActionResult Delete([FromBody] EquipoPersona equipoPersona)
        {
            try {
                var equipoPersonaExist = context.EquipoPersonas.FirstOrDefault(p => p.IdEquipo == equipoPersona.IdEquipo && p.IdPersona == equipoPersona.IdPersona);
                if (equipoPersonaExist != null) {
                    context.EquipoPersonas.Remove(equipoPersonaExist);
                    context.SaveChanges();
                    return Ok("Se elimino la relacion");
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
