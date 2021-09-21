using Microsoft.EntityFrameworkCore;
using senai_gufi_webAPI.Context;
using senai_gufi_webAPI.Domains;
using senai_gufi_webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_gufi_webAPI.Repositories
{
    public class InscricaoRepository : IInscricaoRepository
    {
        GufiContext ctx = new GufiContext();
        public void AprovarOuRecusar(int idInscricao, string status)
        {
            Inscricao inscricaoBuscada = ctx.Inscricaos
                .FirstOrDefault(i => i.IdInscricao == idInscricao);

            switch (status)
            {
                case "1":
                    inscricaoBuscada.IdSituacao = 1;
                    break;
                case "2":
                    inscricaoBuscada.IdSituacao = 2;
                    break;
                case "3":
                    inscricaoBuscada.IdSituacao = 3;
                    break;
                default:
                    inscricaoBuscada.IdSituacao = inscricaoBuscada.IdSituacao;
                    break;
            }

            ctx.Inscricaos.Update(inscricaoBuscada);

            ctx.SaveChanges();
        }

        public void Inscrever(Inscricao inscricao)
        {
            inscricao.IdSituacao = 1;

            ctx.Inscricaos.Add(inscricao);

            ctx.SaveChanges();
        }

        public List<Inscricao> ListarMinhas(int idUsuario)
        {
            //Retorna uma lista com todas as informações das presenças
            return ctx.Inscricaos
                .Include(i => i.IdEventoNavigation)
                .Include(i => i.IdEventoNavigation.IdTipoEventoNavigation)
                .Include(i => i.IdEventoNavigation.IdInstituicaoNavigation)
                .Include("IdSituacaoNavigation")
                .Include(i => i.IdUsuarioNavigation)
                //Estabelece como parâmetro de consulta o Id do usuário recebido
                .Where(i => i.IdUsuario == idUsuario)
                .ToList();
        }
    }
}
