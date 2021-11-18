using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teste.WebMotors.Entidades.Entidades;

namespace Teste.WebMotors.Aplicacao.Interfaces
{
    public interface IAplicacaoAnuncio
    {
        Task IncluirAnuncio(Anuncio anuncio);
        Task AtualizaAnuncio(Anuncio anuncio);
        Task RemoverAnuncio(Anuncio anuncio);
        Task<Anuncio> ConsultarAnuncio(int Id);
        Task<List<Anuncio>> ListarAnuncios();

    }
}
