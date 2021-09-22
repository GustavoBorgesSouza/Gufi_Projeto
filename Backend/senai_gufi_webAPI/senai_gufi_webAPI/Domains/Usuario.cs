using System;
using System.Collections.Generic;

#nullable disable

namespace senai_gufi_webAPI.Domains
{
    public partial class Usuario
    {
        public Usuario()
        {
            Inscricaos = new HashSet<Inscricao>();
        }

        public int IdUsuario { get; set; }
        public int? IdTipoUsuario { get; set; }
        public string NomeUsuario { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }

        public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; }
        public virtual ImagemUsuario ImagemUsuario { get; set; }
        public virtual ICollection<Inscricao> Inscricaos { get; set; }
    }
}
