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
    public class EquipoController : ControllerBase
    {
        private readonly TPI_DBContext context;

        public EquipoController(TPI_DBContext context)
        {
            this.context = context;

        }

        [AllowAnonymous]
        // GET: api/<EquipoController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            try {
                return Ok(context.Equipos.ToList());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<EquipoController>/5
        [HttpGet("{id}", Name = "getequipo")]
        public ActionResult Get(int id)
        {
            try {
                var cursada = context.Equipos.FirstOrDefault(p => p.IdEquipo == id);
                return Ok(cursada);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Equipo equipo)
        {
            try {
                var equipoExist = context.Equipos.FirstOrDefault(p => p.IdEquipo == equipo.IdEquipo);
                if (equipoExist != null) {
                    return BadRequest("Equipo existente");
                }
                else {
                    equipo.Estado = "S";
                    context.Equipos.Add(equipo);
                    context.SaveChanges();
                    return CreatedAtRoute("Getequipo", new { id = equipo.IdEquipo }, equipo);
                }
            }
            catch (Exception ex) {
                return BadRequest("Error al insertar el registro, validar los datos requeridos");
            }
        }

        // PUT api/<EquipoController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Equipo equipo)
        {
            try {
                if (equipo.IdEquipo == id) {
                    context.Entry(equipo).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("Getequipo", new { id = equipo.IdEquipo }, equipo);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<EquipoController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try {
                var equipopersona = context.EquipoPersonas.Where(p => p.IdEquipo == id);

                if (equipopersona != null) {
                    foreach (EquipoPersona eq in equipopersona.ToList()) {
                        context.EquipoPersonas.Remove(eq);
                        context.SaveChanges();
                    }

                    var equipo = context.Equipos.FirstOrDefault(p => p.IdEquipo == id);
                    if (equipo != null) {

                        context.Equipos.Remove(equipo);
                        context.SaveChanges();
                    }
                    return Ok("Equipo eliminado correctamente");
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
