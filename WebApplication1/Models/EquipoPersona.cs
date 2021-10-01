using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class EquipoPersona
    {
        public int IdEquipo { get; set; }
        public int IdPersona { get; set; }

        public virtual Equipo IdEquipoNavigation { get; set; }
        public virtual Persona IdPersonaNavigation { get; set; }
    }
}
