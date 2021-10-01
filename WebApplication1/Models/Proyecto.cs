using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Proyecto
    {
        public Proyecto()
        {
            Comentarios = new HashSet<Comentario>();
            Entregas = new HashSet<Entrega>();
            Reds = new HashSet<Red>();
        }

        public int IdProyecto { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Repositorio { get; set; }
        public int CantMeGusta { get; set; }
        public string RutaFoto { get; set; }
        public string RutaVideo { get; set; }
        public int IdRed { get; set; }
        public int? IdTipoProyecto { get; set; }

        public virtual TipoProyect IdTipoProyectoNavigation { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<Entrega> Entregas { get; set; }
        public virtual ICollection<Red> Reds { get; set; }
    }
}
