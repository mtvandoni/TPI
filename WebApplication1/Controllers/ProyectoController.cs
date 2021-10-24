using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
                return Ok(context.Proyectos.ToList());
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
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ProyectoController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ProyectoController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
