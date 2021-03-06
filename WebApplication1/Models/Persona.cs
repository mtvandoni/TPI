using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Comentarios = new HashSet<Comentario>();
            EquipoPersonas = new HashSet<EquipoPersona>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Mail { get; set; }
        public string NombreUsuario { get; set; }
        public string Password { get; set; }
        public byte Edad { get; set; }
        public string Descripcion { get; set; }
        public string Avatar { get; set; }
        public byte? IdTipo { get; set; }
        public int? IdCursada { get; set; }
        public string CodCursada { get; set; }

        public virtual Cursadum Cursadum { get; set; }
        public virtual TipoPersona IdTipoNavigation { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<EquipoPersona> EquipoPersonas { get; set; }
    }
}
