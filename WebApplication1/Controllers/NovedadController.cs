using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NovedadController : ControllerBase
    {

        private readonly TPI_DBContext context;

        public NovedadController(TPI_DBContext context)
        {
            this.context = context;

        }

        [AllowAnonymous]
        // GET: api/<NovedadController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            try {
                return Ok(context.Novedads.ToList());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<NovedadController>/5
        [HttpGet("{id}", Name = "getnovedad")]
        public ActionResult Get(int id)
        {
            try {
                var entrega = context.Novedads.FirstOrDefault(p => p.IdNovedad == id);
                return Ok(entrega);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Novedad novedad)
        {
            try {
                var novedadExist = context.Novedads.FirstOrDefault(p => p.IdNovedad == novedad.IdNovedad);
                if (novedadExist != null) {
                    return BadRequest("Novedad existente");
                }
                else {
                    context.Novedads.Add(novedad);
                    context.SaveChanges();
                    return CreatedAtRoute("Getnovedad", new { id = novedad.IdNovedad}, novedad);
                }
            }
            catch (Exception ex) {
                return BadRequest("Error al insertar el registro, validar los datos requeridos");
            }
        }

        // PUT api/<NovedadController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Novedad novedad)
        {
            try {
                if (novedad.IdNovedad == id) {
                    context.Entry(novedad).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("Getentrega", new { id = novedad.IdNovedad }, novedad);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<NovedadController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try {
                var novedad = context.Novedads.FirstOrDefault(p => p.IdNovedad == id);
                if (novedad != null) {
                    context.Novedads.Remove(novedad);
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
