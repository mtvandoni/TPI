﻿using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
                var usuario = context.Personas.FirstOrDefault(p => p.Nombre == persona.Nombre && p.Password == persona.Password);
                if (usuario != null) {
                    var token = _authService.Authenticate(usuario.Nombre, usuario.Password);

                    if (token == null) {
                        return Unauthorized();
                    }
                    return Ok(token);
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
        [HttpDelete("{id}")]
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
        }
    }
}
