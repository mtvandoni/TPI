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
    public class EntregaController : ControllerBase
    {
        private readonly TPI_DBContext context;

        public EntregaController(TPI_DBContext context)
        {
            this.context = context;

        }

        [AllowAnonymous]
        // GET: api/<EntregaController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            try {
                return Ok(context.Entregas.ToList());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<EntregaController>/5
        [HttpGet("{id}", Name = "getentrega")]
        public ActionResult Get(int id)
        {
            try {
                var entrega = context.Entregas.FirstOrDefault(p => p.IdEntrega == id);
                return Ok(entrega);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Entrega entrega)
        {
            try {
                var entregaExist = context.Entregas.FirstOrDefault(p => p.IdEntrega == entrega.IdEntrega);
                if (entregaExist != null) {
                    return BadRequest("Equipo existente");
                }
                else {
                    context.Entregas.Add(entrega);
                    context.SaveChanges();
                    return CreatedAtRoute("Getentrega", new { id = entrega.IdEntrega }, entrega);
                }
            }
            catch (Exception ex) {
                return BadRequest("Error al insertar el registro, validar los datos requeridos");
            }
        }

        // PUT api/<EntregaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Entrega entrega)
        {
            try {
                if (entrega.IdEntrega == id) {
                    context.Entry(entrega).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("Getentrega", new { id = entrega.IdEntrega }, entrega);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<EntregaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try {
                var entrega = context.Entregas.FirstOrDefault(p => p.IdEntrega == id);
                if (entrega != null) {
                    context.Entregas.Remove(entrega);
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
