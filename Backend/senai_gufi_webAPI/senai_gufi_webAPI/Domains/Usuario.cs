using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

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

        [Required(ErrorMessage = "O campo email é obrigatorio")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório")]
        [StringLength(10, MinimumLength = 3, ErrorMessage ="A senha deve ter de 3 a 10 caracteres")]
        public string Senha { get; set; }

        public virtual TipoUsuario IdTipoUsuarioNavigation { get; set; }
        public virtual ICollection<Inscricao> Inscricaos { get; set; }
    }
}
