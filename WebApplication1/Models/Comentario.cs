using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Comentario
    {
        public int IdComentario { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
        public int IdProyecto { get; set; }
        public int IdPersona { get; set; }

        public virtual Persona IdPersonaNavigation { get; set; }
        public virtual Proyecto IdProyectoNavigation { get; set; }
    }
}
