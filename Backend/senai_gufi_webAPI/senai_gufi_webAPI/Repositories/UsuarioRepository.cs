using Microsoft.AspNetCore.Http;
using senai_gufi_webAPI.Context;
using senai_gufi_webAPI.Domains;
using senai_gufi_webAPI.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace senai_gufi_webAPI.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        GufiContext ctx = new GufiContext();

        public string ConsultarPerfilBD(int idUsuario)
        {
            ImagemUsuario imagemUsuario = new ImagemUsuario();
            imagemUsuario.IdUsuario = idUsuario;

            imagemUsuario = ctx.ImagemUsuarios.FirstOrDefault(i => i.IdUsuario == idUsuario);

            if (imagemUsuario != null)
            {
                return Convert.ToBase64String(imagemUsuario.Binario);
            }

            return null;
        }

        public string ConsultarPerfilDir(int idUsuario)
        {
            string nomeNovo = idUsuario.ToString() + ".png";

            string caminho = Path.Combine("Perfil", nomeNovo);

            //analisa se o caminho existe
            if (File.Exists(caminho))
            {
                //recupera o array de bytes
                byte[] bytesArquivo = File.ReadAllBytes(caminho);

                //converte em base64
                return Convert.ToBase64String(bytesArquivo);
            }

            return null;
        }

        public Usuario Login(string email, string senha)
        {
            return ctx.Usuarios.FirstOrDefault(u => u.Email == email && u.Senha == senha);
        }

        public void SalvarPerfilBD(IFormFile foto, int idUsuario)
        {
            //Instanciou o objeto ImagemUsuario para gravar o arquivo no BD
            ImagemUsuario imagemUsuario = new ImagemUsuario();

            using (var ms = new MemoryStream())
            {
                //Copia a imagem enviada para a memória
                foto.CopyTo(ms);

                //ToArray = Todos os bytes da imagem
                imagemUsuario.Binario = ms.ToArray();

                //Nome do Arquivo
                imagemUsuario.NomeAquivo = foto.FileName;

                //extensão do arquivo = ex: .png
                imagemUsuario.MimeType = foto.FileName.Split('.').Last();

                //IdUsuario
                imagemUsuario.IdUsuario = idUsuario;
            }

            //ANALISAR SE O USUARIO JÁ POSSUI FOTO DE PERFIL

            ImagemUsuario fotoexistente = new ImagemUsuario();
            fotoexistente = ctx.ImagemUsuarios.FirstOrDefault(i => i.IdUsuario == idUsuario);

            if (fotoexistente != null)
            {
                fotoexistente.Binario = imagemUsuario.Binario;
                fotoexistente.NomeAquivo = imagemUsuario.NomeAquivo;
                fotoexistente.MimeType = imagemUsuario.MimeType;
                fotoexistente.IdUsuario = imagemUsuario.IdUsuario;

                //Atualiza a imagem do perfil do usuário
                ctx.ImagemUsuarios.Update(fotoexistente);
            }
            else
            {
                // se não tem ele pega e coloca a foto
                ctx.ImagemUsuarios.Add(imagemUsuario);
            }

            //Salvar Modificações
            ctx.SaveChanges();
        }

        public void SalvarPerfilDir(IFormFile foto, int idUsuario)
        {
            //Define o nome do arquivo ocm o Id do Usuairo + .png
            string nomeNovo = idUsuario.ToString() + ".png";

            //FileStream fornece uma exibicao para uma sequencia de bytes, dando suporte para leitura e gravação
            using (var stream = new FileStream(Path.Combine("Perfil", nomeNovo), FileMode.Create))
            {
                //copipa todos os elementos(array de bytes)  para o caminho indicado
                foto.CopyTo(stream);
            }
        }
    }
}
