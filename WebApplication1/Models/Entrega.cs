using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Entrega
    {
        public int IdEntrega { get; set; }
        public string Descripcion { get; set; }
        public int? IdProyecto { get; set; }

        public virtual Proyecto IdProyectoNavigation { get; set; }
    }
}
