using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ComentarioController : Controller
    {
        private readonly TPI_DBContext context;
        public ComentarioController(TPI_DBContext context)
        {
            this.context = context;
        }

        [HttpGet("{id}", Name = "getcomentario")]
        public ActionResult Get(int id)
        {
            try {
                //List<Comentario> comentarios = new List<Comentario>();

                var comentarios = context.Comentarios.Where(p => p.IdProyecto == id);

                return Ok(comentarios);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Comentario comentario)
        {
            context.Comentarios.Add(comentario);
            context.SaveChanges();

            return CreatedAtRoute("Getcomentario", new { id = comentario.IdComentario }, comentario);

        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try {
                var comentario = context.Comentarios.FirstOrDefault(p => p.IdComentario == id);
                if (comentario != null) {
                    context.Comentarios.Remove(comentario);
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
