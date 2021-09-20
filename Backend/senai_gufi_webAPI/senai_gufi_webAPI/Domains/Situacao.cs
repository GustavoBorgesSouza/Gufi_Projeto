using System;
using System.Collections.Generic;

#nullable disable

namespace senai_gufi_webAPI.Domains
{
    public partial class Situacao
    {
        public Situacao()
        {
            Inscricaos = new HashSet<Inscricao>();
        }

        public int IdSituacao { get; set; }
        public string Descricao { get; set; }

        public virtual ICollection<Inscricao> Inscricaos { get; set; }
    }
}
