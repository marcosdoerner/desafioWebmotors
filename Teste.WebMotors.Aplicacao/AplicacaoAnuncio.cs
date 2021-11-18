using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teste.WebMotors.Aplicacao.Interfaces;
using Teste.WebMotors.Dominio.Interfaces;
using Teste.WebMotors.Dominio.Interfaces.InterfaceRepositorio;
using Teste.WebMotors.Dominio.Interfaces.InterfaceServicos;
using Teste.WebMotors.Entidades.Entidades;

namespace Teste.WebMotors.Aplicacao
{
    public class AplicacaoAnuncio : IAplicacaoAnuncio
    {
        IServicoAnuncio _IServicoAnuncio;
        IRepositorioAnuncio _IRepositorioAnuncio;

        public AplicacaoAnuncio(IServicoAnuncio servicoAnuncio, IRepositorioAnuncio repositorioAnuncio)
        {
            _IServicoAnuncio = servicoAnuncio;
            _IRepositorioAnuncio = repositorioAnuncio;
        }

        public async Task IncluirAnuncio(Anuncio anuncio)
        {
            await _IServicoAnuncio.IncluirAnuncio(anuncio);
        }

        public async Task AtualizaAnuncio(Anuncio anuncio)
        {
            await _IServicoAnuncio.AtualizarAnuncio(anuncio);
        }

        public async Task RemoverAnuncio(Anuncio anuncio)
        {
            await _IServicoAnuncio.RemoverAnuncio(anuncio);
        }


        public async Task<Anuncio> ConsultarAnuncio(int Id)
        {
            return await _IServicoAnuncio.ConsultarAnuncio(Id);
        }

        public async Task<List<Anuncio>> ListarAnuncios()
        {
            return await _IServicoAnuncio.ListarAnuncios();
        }

    }
}
