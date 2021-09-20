using System;
using System.Collections.Generic;

#nullable disable

namespace senai_gufi_webAPI.Domains
{
    public partial class Evento
    {
        public Evento()
        {
            Inscricaos = new HashSet<Inscricao>();
        }

        public int IdEvento { get; set; }
        public int? IdTipoEvento { get; set; }
        public int? IdInstituicao { get; set; }
        public string NomeEvento { get; set; }
        public string DescricaoEvento { get; set; }
        public DateTime DataEvento { get; set; }
        public bool? AcessoLivre { get; set; }

        public virtual Instituicao IdInstituicaoNavigation { get; set; }
        public virtual TipoEvento IdTipoEventoNavigation { get; set; }
        public virtual ICollection<Inscricao> Inscricaos { get; set; }
    }
}
