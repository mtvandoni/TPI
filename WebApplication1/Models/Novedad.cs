using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Novedad
    {
        public int IdNovedad { get; set; }
        public string Descripcion { get; set; }
        public string RutaFoto { get; set; }
        public string RutaVideo { get; set; }
    }
}
