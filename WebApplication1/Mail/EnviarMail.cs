using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EASendMail;

namespace WebApplication1.Mail
{
    public class EnviarMail
    {
        public  async Task<String> envio(string correoDestino, string clave) 
        {
            string correoOrigen = "webtpi.tecnicaturas@gmail.com";
            string mensaje = "";
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
                obServer.ConnectType = SmtpConnectType.ConnectTryTLS;

                SmtpClient obClient = new SmtpClient();
                await obClient.SendMailAsync(obServer, obCorreo);

                return mensaje = "OK";

            }catch (Exception exception) {
                PropertyInfo[] properties = exception.GetType()
                            .GetProperties();
                List<string> fields = new List<string>();
                foreach (PropertyInfo property in properties) {
                    object value = property.GetValue(exception, null);
                    fields.Add(String.Format(
                                     "{0} = {1}",
                                     property.Name,
                                     value != null ? value.ToString() : String.Empty
                    ));
                }
                return String.Join("\n", fields.ToArray());
            }
        }
    }
}
