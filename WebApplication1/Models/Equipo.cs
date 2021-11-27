using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Equipo
    {
        public Equipo()
        {
            EquipoPersonas = new HashSet<EquipoPersona>();
        }

        public int IdEquipo { get; set; }
        public string Nombre { get; set; }
        public string Estado { get; set; }
        public int? IdProyecto { get; set; }

        public virtual Proyecto IdProyectoNavigation { get; set; }
        public virtual ICollection<EquipoPersona> EquipoPersonas { get; set; }
    }
}
