using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using EASendMail;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Mail
{
    public class EnviarMail
    {
        private readonly IConfiguration Configuration;

        public EnviarMail(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public  async Task<String> envio(string correoDestino, string clave,string nombre, string codCursada) 
        {
            string mensaje = "";
            try{
                SmtpMail obCorreo = new SmtpMail("TryIt");
                obCorreo.From = Configuration["EmailSettings:correoOrigen"];
                obCorreo.To = correoDestino;
                if (!codCursada.Equals(null)) {
                    obCorreo.Subject = Configuration["EmailSettings:SubjectAlta"] + "-" + codCursada;
                    obCorreo.TextBody = Configuration["EmailSettings:TextSaludo"] + nombre + Configuration["EmailSettings:TextBodyAlta"] +
                        "\nUsuario: " + correoDestino + 
                        "\nPassword: " + clave +"\n" + Configuration["EmailSettings:UrlAcceso"];
                }
                else {
                    obCorreo.Subject = Configuration["EmailSettings:SubjectRecuPass"];
                    obCorreo.TextBody = Configuration["EmailSettings:TextBodyRecuPass"] + clave;
                }
                
                SmtpServer obServer = new SmtpServer(Configuration["EmailSettings:SmtpServer"]);
                obServer.User = Configuration["EmailSettings:correoOrigen"];
                obServer.Password = Configuration["EmailSettings:Password"];
                obServer.Port = Int32.Parse(Configuration["EmailSettings:Puerto"]);

                if ("ConnectSSLAuto".Equals(Configuration["EmailSettings:ConnectType"])) {
                    obServer.ConnectType = SmtpConnectType.ConnectSSLAuto;
                }
                else if ("ConnectTryTLS".Equals(Configuration["EmailSettings:ConnectType"])) {
                    obServer.ConnectType = SmtpConnectType.ConnectTryTLS;
                }
                else if ("ConnectDirectSSL".Equals(Configuration["EmailSettings:ConnectType"])) {
                    obServer.ConnectType = SmtpConnectType.ConnectDirectSSL;
                }
                else if ("ConnectSTARTTLS".Equals(Configuration["EmailSettings:ConnectType"])) {
                    obServer.ConnectType = SmtpConnectType.ConnectSTARTTLS;
                }
                else {
                    obServer.ConnectType = SmtpConnectType.ConnectNormal;
                }   


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
