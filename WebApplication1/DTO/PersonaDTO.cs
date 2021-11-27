using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.DTO
{
    public class PersonaDTO
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? Dni { get; set; }
        public string Email { get; set; }
        public string EmailUnlam { get; set; }
        public string Carrera { get; set; }
        public string Password { get; set; }
        public byte? Edad { get; set; }
        public string Descripcion { get; set; }
        public string Avatar { get; set; }
        public string token { get; set; }
        public int IdTipo { get; set; }
        public string Estado { get; set; }
    }
}
