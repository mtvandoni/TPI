using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class TipoPersona
    {
        public TipoPersona()
        {
            Personas = new HashSet<Persona>();
        }

        public byte IdTipo { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<Persona> Personas { get; set; }
    }
}
