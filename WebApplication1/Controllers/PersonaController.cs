using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApplication1.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;



// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase

    {
        private readonly IJwtAuthenticationService _authService;
        private readonly TPI_DBContext context;

        public PersonaController(TPI_DBContext context, IJwtAuthenticationService authService)
        {
            this.context = context;
            _authService = authService;


        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Persona persona)
        {
            var token = _authService.Authenticate(persona.NombreUsuario, persona.Password);

            if (token == null)
            {
                return Unauthorized();
            }

            return Ok(token);
        }

        [AllowAnonymous]
        // GET: api/<PersonaController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            try
            {
                return Ok(context.Personas.ToList());
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<PersonaController>/5
        [HttpGet("{id}", Name = "getPersona")]
        public ActionResult Get(int id)
        {
            try
            {
                var persona = context.Personas.FirstOrDefault(p => p.Id == id);
                return Ok(persona);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // POST api/<PersonaController>
        [HttpPost]
        public ActionResult Post([FromBody] Persona persona)
        {
            context.Personas.Add(persona);
            context.SaveChanges();
            return CreatedAtRoute("Getpersona", new { id = persona.Id }, persona);
        }

        // PUT api/<PersonaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Persona persona)
        {
            try
            {
                if (persona.Id == id)
                {
                    context.Entry(persona).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("Getpersona", new { id = persona.Id }, persona);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<PersonaController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var persona = context.Personas.FirstOrDefault(p => p.Id == id);
                if (persona != null)
                {
                    context.Personas.Remove(persona);
                    context.SaveChanges();
                    return Ok(id);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
