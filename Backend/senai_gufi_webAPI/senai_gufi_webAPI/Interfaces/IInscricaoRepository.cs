using senai_gufi_webAPI.Domains;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace senai_gufi_webAPI.Interfaces
{
    /// <summary>
    /// Interface responsável pelo InscricaooRepository
    /// </summary>
    interface IInscricaoRepository
    {
        /// <summary>
        /// Lista todos os eventos de um determinado usuário
        /// </summary>
        /// <param name="idUsuario">Id do usuário que participa dos eventos</param>
        /// <returns>Uma lista de presenças com os dados dos eventos</returns>
        List<Inscricao> ListarMinhas(int idUsuario);

        /// <summary>
        /// Cria uma nova presença
        /// </summary>
        /// <param name="inscricao">Objeto com as infos da inscricao</param>
        void Inscrever(Inscricao inscricao);

        /// <summary>
        /// Altera o status de uma presença
        /// </summary>
        /// <param name="idInscricao">id da inscrição a ter a situação alterada</param>
        /// <param name="status">Parametro que atualiza a situação da presença para 1- Aguardando, 2- aprovada, 3- recusada</param>
        void AprovarOuRecusar(int idInscricao, string status);
    }
}
