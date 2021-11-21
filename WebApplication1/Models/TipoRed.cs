using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class TipoRed
    {
        public TipoRed()
        {
            Reds = new HashSet<Red>();
        }

        public int IdTipoRed { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Red> Reds { get; set; }
    }
}
