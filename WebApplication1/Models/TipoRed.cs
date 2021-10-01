using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class TipoRed
    {
        public int IdTipoRed { get; set; }
        public string Descripcion { get; set; }
        public int IdRed { get; set; }

        public virtual Red IdRedNavigation { get; set; }
    }
}
