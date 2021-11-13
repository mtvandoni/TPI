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

namespace WebApplication1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AltaMasivaUsuarios : ControllerBase
    {
        private readonly TPI_DBContext context;

        public AltaMasivaUsuarios(TPI_DBContext context)
        {
            this.context = context;
        }

        // POST api/<AltaMasivaUsuariosController>
        [HttpPost("PostAltaMasiva")]
        public ActionResult PostAltaMasiva([FromBody] List<Persona> personas)
        {
            List<Persona> altaUsuariosFalla = new List<Persona>();
            var cursada = context.Cursada.OrderByDescending(c => c.IdCursada).FirstOrDefault();
            foreach (Persona UsuarioNuevo in personas) {
                var usuario = context.Personas.FirstOrDefault(p => p.Dni == UsuarioNuevo.Dni);
                try {
                    if (usuario != null) {
                        usuario.IdCursada = cursada.IdCursada;
                        context.SaveChanges();

                        //Envio de mail
                        EnviarMail enviar = new EnviarMail();
                        string mensaje = enviar.envio(UsuarioNuevo.EmailUnlam, UsuarioNuevo.Password);
                    }
                    else {
                        UsuarioNuevo.IdCursada = cursada.IdCursada;
                        context.Personas.Add(UsuarioNuevo);
                        context.SaveChanges();
                    }
                }catch (Exception ex) {
                    altaUsuariosFalla.Add(UsuarioNuevo);
                }
            }

            //List<Persona> mailAlumnos = new List<Persona>();
            //var mailAlumnos = context.Personas.Where(p => p.IdCursada == cursada.IdCursada);

            //var mailAlumnos = context.Personas.FirstOrDefault(p => p.IdCursada == cursada.IdCursada);
            //EnviarMail enviar = new EnviarMail();

            //string mensaje = enviar.envio(mailAlumnos.EmailUnlam, mailAlumnos.Password);

            return Ok(altaUsuariosFalla);

        }
    }
}
