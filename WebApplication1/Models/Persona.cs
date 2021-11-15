﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebApplication1.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Comentarios = new HashSet<Comentario>();
            EquipoPersonas = new HashSet<EquipoPersona>();
            Novedads = new HashSet<Novedad>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public int? Dni { get; set; }
        public string Email { get; set; }
        public string EmailUnlam { get; set; }
        public string Carrera { get; set; }
        public string Password { get; set; }
        public byte? Edad { get; set; }
        public string Descripcion { get; set; }
        public string Avatar { get; set; }
        public byte? IdTipo { get; set; }
        public int? IdCursada { get; set; }

        public virtual Cursadum IdCursadaNavigation { get; set; }
        public virtual TipoPersona IdTipoNavigation { get; set; }
        public virtual ICollection<Comentario> Comentarios { get; set; }
        public virtual ICollection<EquipoPersona> EquipoPersonas { get; set; }
        public virtual ICollection<Novedad> Novedads { get; set; }
    }
}
