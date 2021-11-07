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
    public class CursadaController : ControllerBase
    {
        private readonly TPI_DBContext context;

        public CursadaController(TPI_DBContext context)
        {
            this.context = context;

        }
        [AllowAnonymous]
        // GET: api/<CursadaController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            try {
                return Ok(context.Cursada.ToList());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<CursadaController>/5
        [HttpGet("{id}", Name = "getCursada")]
        public ActionResult Get(int id)
        {
            try {
                var cursada = context.Cursada.FirstOrDefault(p => p.IdCursada == id);
                return Ok(cursada);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Cursadum cursada)
        {
            try {
                var curadaNueva = context.Cursada.FirstOrDefault(p => p.CodCursada == cursada.CodCursada);
                if (curadaNueva != null) {
                    return BadRequest("Cursada existente");
                }
                else {
                    context.Cursada.Add(cursada);
                    context.SaveChanges();
                    return CreatedAtRoute("Getpersona", new { id = cursada.IdCursada }, cursada);
                }
            }
            catch (Exception ex) {
                return BadRequest("Error al insertar el registro, validar los datos requeridos");
            }
        }

        // PUT api/<CursadaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Cursadum cursada)
        {
            try {
                if (cursada.IdCursada == id) {
                    context.Entry(cursada).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("Getpersona", new { id = cursada.IdCursada }, cursada);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CursadaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try {
                var cursada = context.Cursada.FirstOrDefault(p => p.IdCursada == id);
                if (cursada != null) {
                    context.Cursada.Remove(cursada);
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

