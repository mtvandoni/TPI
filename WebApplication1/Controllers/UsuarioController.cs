using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApplication1.DTO;
using WebApplication1.Mail;
using WebApplication1.Models;
using WebApplication1.Utils;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase

    {
        private readonly IJwtAuthenticationService _authService;
        private readonly TPI_DBContext context;
        private readonly IConfiguration Configuration;

        public UsuarioController(TPI_DBContext context, IJwtAuthenticationService authService, IConfiguration configuration)
        {
             this.context = context;
            _authService = authService;
            Configuration = configuration;

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
                            usuarioDTO.Estado = usuario.Estado;
                            usuarioDTO.Descripcion  = usuario.Descripcion;

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
                    var cursada = context.Cursada.OrderByDescending(c => c.IdCursada).FirstOrDefault();
                    persona.IdCursada = cursada.IdCursada;

                    PassWordRandom pass = new PassWordRandom();
                    persona.Password = pass.RandomPassword();

                    context.Personas.Add(persona);
                    context.SaveChanges();

                    //Envio de mail
                    EnviarMail enviar = new EnviarMail(Configuration);
                    Task<string> myTask = enviar.envio(persona.EmailUnlam, persona.Password, persona.Nombre, cursada.CodCursada);
                    string mensaje = myTask.Result;

                    if (mensaje.Equals("OK")) {
                        return Ok("integrantes Asignados correctamente");
                    }
                    else {
                        return BadRequest("Error al enviar Mail : " + mensaje);
                    }
                }
            }
            catch (Exception ex) {
                return BadRequest("Error al insertar el registro, validar los datos requeridos : "+ ex);
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

        // PUT api/<PersonaController>/5
        [HttpPut("RecuperarPass")]
        public ActionResult RecuperarPass( [FromBody] Persona persona)
        {
            try {
                var usuario = context.Personas.FirstOrDefault(p => p.EmailUnlam == persona.EmailUnlam);
                if (usuario !=null) {
                    PassWordRandom pass = new PassWordRandom();
                    usuario.Password = pass.RandomPassword();

                    context.Entry(usuario).State = EntityState.Modified;
                    context.SaveChanges();

                    //Envio de mail
                    EnviarMail enviar = new EnviarMail(Configuration);
                    Task<string> myTask = enviar.envio(usuario.EmailUnlam, usuario.Password, usuario.Nombre, null);

                    string mensaje = myTask.Result;

                    if (mensaje.Equals("OK")) {
                        return Ok("Password renovada exitosamente");
                    }
                    else {
                        return BadRequest("Error al enviar Mail : " + mensaje);
                    }
                }
                else {
                    return BadRequest("Error el mail no se encuentra registrado en la base");
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PersonaController>/5
        [HttpPut("ReenvioMail")]
        public ActionResult ReenvioMail([FromBody] Persona persona)
        {
            try {
                var usuario = context.Personas.FirstOrDefault(p => p.Dni == persona.Dni);
                if (usuario != null) {

                    //Envio de mail
                    EnviarMail enviar = new EnviarMail(Configuration);
                    Task<string> myTask = enviar.envio(usuario.EmailUnlam, usuario.Password, usuario.Nombre, null);

                    string mensaje = myTask.Result;

                    if (mensaje.Equals("OK")) {
                        return Ok("Mail Reenviado al correo: " + usuario.EmailUnlam + " usuario: " + usuario.Nombre);
                    }
                    else {
                        return BadRequest("Error al enviar Mail : " + mensaje);
                    }
                }
                else {
                    return BadRequest("Error el mail no se encuentra registrado en la base");
                }
            }
            catch (Exception ex) {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("DeshabilitarUsuario")]
        public ActionResult DeshabilitarUsuario([FromBody] Persona persona)
        {
            try {
                var usuario = context.Personas.FirstOrDefault(p => p.Id == persona.Id);
                if (usuario == null) {
                    return BadRequest("Usuario existente");
                }
                else {
                    usuario.Estado = "N";
                    context.Entry(usuario).State = EntityState.Modified;
                    context.SaveChanges();
                    return Ok("Usuario deshabilitado correctamente");
                }
            }
            catch (Exception ex) {
                return BadRequest("Error al insertar el registro, validar los datos requeridos: " + ex.Message);
            }
        }

        [HttpPut("HabilitarUsuario")]
        public ActionResult HabilitarUsuario([FromBody] Persona persona)
        {
            try {
                var usuario = context.Personas.FirstOrDefault(p => p.Id == persona.Id);
                if (usuario == null) {
                    return BadRequest("Usuario existente");
                }
                else {
                    usuario.Estado = "S";
                    context.Entry(usuario).State = EntityState.Modified;
                    context.SaveChanges();
                    return Ok("Usuario habilitado correctamente");
                }
            }
            catch (Exception ex) {
                return BadRequest("Error al insertar el registro, validar los datos requeridos: " + ex.Message);
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
