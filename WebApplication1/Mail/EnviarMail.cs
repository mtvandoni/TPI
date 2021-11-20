using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EASendMail;

namespace WebApplication1.Mail
{
    public class EnviarMail
    {
        public async Task envio(string correoDestino, string clave) 
        {
            string correoOrigen = "webtpi.tecnicaturas@gmail.com";
            //string mensaje = "";
            try{
                SmtpMail obCorreo = new SmtpMail("TryIt");
                obCorreo.From = correoOrigen;
                obCorreo.To = correoDestino;
                obCorreo.Subject = "Alta WEB TPI";
                obCorreo.TextBody = "Hola, bienvenido a WEB TPI, tu clave es " + clave;

                SmtpServer obServer = new SmtpServer("smtp.gmail.com");
                obServer.User = correoOrigen;
                obServer.Password = "web2021tpi";
                obServer.Port = 587;
                obServer.ConnectType = SmtpConnectType.ConnectSSLAuto;

                SmtpClient obClient = new SmtpClient();
                await obClient.SendMailAsync(obServer, obCorreo);

                //mensaje = "ok";
                //return mensaje;

            }catch (Exception ex) {
               // mensaje = "error";
                //return mensaje;
            }
        }
    }
}
