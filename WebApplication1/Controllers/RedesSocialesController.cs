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
    public class RedesSocialesController : ControllerBase
    {
        private readonly TPI_DBContext context;

        public RedesSocialesController(TPI_DBContext context)
        {
            this.context = context;
        }

        [AllowAnonymous]
        // GET: api/<RedesSocialesController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            try {
                return Ok(context.Reds.ToList());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        // GET api/<RedesSocialesController>/5
        [HttpGet("{id}", Name = "getredesSociales")]
        public ActionResult Get(int id)
        {
            try {
                var red = context.Reds.FirstOrDefault(p => p.IdRed == id);
                return Ok(red);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Red red)
        {
            try {
                var redSocialExist = context.Reds.FirstOrDefault(p => p.IdRed == red.IdRed && p.IdProyecto == red.IdProyecto);
                if (redSocialExist != null) {
                    return BadRequest("Relacion Red - Proyecto existente");
                }
                else {
                    context.Reds.Add(red);
                    context.SaveChanges();
                    return CreatedAtRoute("getredesSociales", new { id = red.IdRed }, red);
                }
            }
            catch (Exception ex) {
                return BadRequest("Error al insertar el registro, validar los datos requeridos");
            }
        }

        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Red red)
        {
            try {
                if (red.IdRed == id ) {
                    context.Entry(red).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("GetredesSociales", new { id = red.IdRed }, red);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<RedesSocialesController>/5
        [HttpDelete]
        public ActionResult Delete([FromBody] Red red)
        {
            try {
                var redExist = context.Reds.FirstOrDefault(p => p.IdRed == red.IdRed && p.IdProyecto == red.IdProyecto);
                if (redExist != null) {
                    context.Reds.Remove(redExist);
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
