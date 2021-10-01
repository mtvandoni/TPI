using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Red
    {
        public Red()
        {
            TipoReds = new HashSet<TipoRed>();
        }

        public int IdRed { get; set; }
        public string Descripcion { get; set; }
        public int IdProyecto { get; set; }

        public virtual Proyecto IdProyectoNavigation { get; set; }
        public virtual ICollection<TipoRed> TipoReds { get; set; }
    }
}
