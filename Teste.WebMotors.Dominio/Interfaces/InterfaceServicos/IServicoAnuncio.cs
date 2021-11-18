using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teste.WebMotors.Entidades.Entidades;

namespace Teste.WebMotors.Dominio.Interfaces.InterfaceServicos
{
    public interface IServicoAnuncio
    {
        Task IncluirAnuncio(Anuncio anuncio);
        Task AtualizarAnuncio(Anuncio anuncio);
        Task RemoverAnuncio(Anuncio anuncio);
        Task<Anuncio> ConsultarAnuncio(int ID);
        Task<List<Anuncio>> ListarAnuncios();
    }
}
