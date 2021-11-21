using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Red
    {
        public int IdRed { get; set; }
        public string Descripcion { get; set; }
        public int IdProyecto { get; set; }
        public int IdTipoRed { get; set; }

        public virtual Proyecto IdProyectoNavigation { get; set; }
        public virtual TipoRed IdTipoRedNavigation { get; set; }
    }
}
