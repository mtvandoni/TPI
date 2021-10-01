using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class TipoProyect
    {
        public TipoProyect()
        {
            Proyectos = new HashSet<Proyecto>();
        }

        public int IdTipoProyecto { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Proyecto> Proyectos { get; set; }
    }
}
