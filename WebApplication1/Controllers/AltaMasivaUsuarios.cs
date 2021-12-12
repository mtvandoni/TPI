using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication1.Models;
using WebApplication1.DTO;
using WebApplication1.Mail;
using WebApplication1.Utils;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AltaMasivaUsuarios : ControllerBase
    {
        private readonly TPI_DBContext context;
        private readonly IConfiguration Configuration;

        public AltaMasivaUsuarios(TPI_DBContext context, IConfiguration configuration)
        {
            this.context = context;
            Configuration = configuration;
        }

        // POST api/<AltaMasivaUsuariosController>
        [HttpPost("PostAltaMasiva")]
        public ActionResult PostAltaMasiva([FromBody] List<Persona> personas)
        {
            List<Persona> altaUsuariosFalla = new List<Persona>();
            var cursada = context.Cursada.OrderByDescending(c => c.IdCursada).FirstOrDefault();
            foreach (Persona UsuarioNuevo in personas) {
                var usuario = context.Personas.FirstOrDefault(p => p.Dni == UsuarioNuevo.Dni);
                PassWordRandom pass = new PassWordRandom();
                String password = pass.RandomPassword();
                try {
                    if (usuario != null) {
                        usuario.IdCursada = cursada.IdCursada;

                        usuario.Password = password;
                        context.SaveChanges();

                        //Envio de mail
                        EnviarMail enviar = new EnviarMail(Configuration);
                        Task<string> myTask = enviar.envio(UsuarioNuevo.EmailUnlam, UsuarioNuevo.Password, UsuarioNuevo.Nombre, cursada.CodCursada);
                        string mensaje = myTask.Result;

                        if (mensaje.Equals("OK")) {
                            return Ok("integrantes Asignados correctamente");
                        }
                        else {
                            return BadRequest("Error al enviar Mail : " + mensaje);
                        }
                    }
                    else {
                        UsuarioNuevo.IdCursada = cursada.IdCursada;

                        UsuarioNuevo.Password = password;

                        context.Personas.Add(UsuarioNuevo);
                        context.SaveChanges();

                        //Envio de mail
                        EnviarMail enviar = new EnviarMail(Configuration);
                        Task<string> myTask = enviar.envio(UsuarioNuevo.EmailUnlam, UsuarioNuevo.Password, UsuarioNuevo.Nombre, cursada.CodCursada);
                        string mensaje = myTask.Result;

                        if (mensaje.Equals("OK")) {
                            return Ok("integrantes Asignados correctamente");
                        }
                        else {
                            return BadRequest("Error al enviar Mail : " + mensaje);
                        }
                    }
                }catch (Exception ex) {
                    altaUsuariosFalla.Add(UsuarioNuevo);
                }
            }
            return Ok(altaUsuariosFalla);
        }
    }
}
