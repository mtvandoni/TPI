using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Cursadum
    {
        public Cursadum()
        {
            Personas = new HashSet<Persona>();
        }

        public int IdCursada { get; set; }
        public string CodCursada { get; set; }
        public string Descripcion { get; set; }
        public DateTime? FechaInicio { get; set; }
        public DateTime? FechaFin { get; set; }

        public virtual ICollection<Persona> Personas { get; set; }
    }
}
