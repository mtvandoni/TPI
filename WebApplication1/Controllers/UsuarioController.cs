using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.DTO;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase

    {
        private readonly IJwtAuthenticationService _authService;
        private readonly TPI_DBContext context;

        public UsuarioController(TPI_DBContext context, IJwtAuthenticationService authService)
        {
             this.context = context;
            _authService = authService;

        }

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] Persona persona)
        {
            try {
                var usuario = context.Personas.FirstOrDefault(p => p.EmailUnlam == persona.EmailUnlam && p.Password == persona.Password);

                if (usuario != null) {
                    var token = _authService.Authenticate(usuario.EmailUnlam, usuario.Password);

                    if (token == null) {
                        return Unauthorized();
                    }
                    else {
                        PersonaDTO usuarioDTO = new PersonaDTO();
                            usuarioDTO.Id = usuario.Id;
                            usuarioDTO.token = "Bearer "+ token;
                            usuarioDTO.Nombre = usuario.Nombre;
                            usuarioDTO.Dni = usuario.Dni;
                            usuarioDTO.Email = usuario.Email;
                            usuarioDTO.EmailUnlam = usuario.EmailUnlam;
                            usuarioDTO.Carrera = usuario.Carrera;
                            usuarioDTO.Avatar = usuario.Avatar;
                            usuarioDTO.IdTipo = (int)usuario.IdTipo;
                        return Ok(usuarioDTO);
                    }
                }
                else {
                    return BadRequest("Usuario no encontrado");
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [AllowAnonymous]
        // GET: api/<PersonaController>
        [HttpGet]
        public ActionResult<string> Get()
        {
            try {
                return Ok(context.Personas.ToList());
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<PersonaController>/5
        [HttpGet("{id}", Name = "getPersona")]
        public ActionResult Get(int id)
        {
            try {
                var persona = context.Personas.FirstOrDefault(p => p.Id == id);
                return Ok(persona);
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult Post([FromBody] Persona  persona)
        {
            try {
                var usuario = context.Personas.FirstOrDefault(p => p.Dni == persona.Dni);
                if (usuario != null) {
                    return BadRequest("Usuario existente");
                }
                else {
                    var cursada = context.Cursada.OrderByDescending(c => c.IdCursada).LastOrDefault();
                    persona.IdCursada = cursada.IdCursada;
                    context.Personas.Add(persona);
                    context.SaveChanges();
                    return CreatedAtRoute("Getpersona", new { id = persona.Id }, persona);
                }
            }
            catch (Exception ex) {
                return BadRequest("Error al insertar el registro, validar los datos requeridos");
            }
        }

        // PUT api/<PersonaController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Persona persona)
        {
            try {
                if (persona.Id == id) {
                    context.Entry(persona).State = EntityState.Modified;
                    context.SaveChanges();
                    return CreatedAtRoute("Getpersona", new { id = persona.Id }, persona);
                }
                else {
                    return BadRequest();
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<PersonaController>/5
      /*  [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try {
                var persona = context.Personas.FirstOrDefault(p => p.Id == id);
                if (persona != null) {
                    context.Personas.Remove(persona);
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
        }*/
    }
}
