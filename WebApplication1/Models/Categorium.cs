using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Categorium
    {
        public Categorium()
        {
            Proyectos = new HashSet<Proyecto>();
        }

        public int IdCategoria { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Proyecto> Proyectos { get; set; }
    }
}
