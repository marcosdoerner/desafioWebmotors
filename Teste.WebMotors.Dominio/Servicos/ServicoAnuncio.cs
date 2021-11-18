using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Teste.WebMotors.Dominio.Interfaces.InterfaceRepositorio;
using Teste.WebMotors.Dominio.Interfaces.InterfaceServicos;
using Teste.WebMotors.Entidades.Entidades;

namespace Teste.WebMotors.Dominio.Servicos
{
    public class ServicoAnuncio : IServicoAnuncio
    {
        private readonly IRepositorioAnuncio _IRepositorioAnuncio;

        public ServicoAnuncio(IRepositorioAnuncio iRepositorioAnuncio)
        {
            _IRepositorioAnuncio = iRepositorioAnuncio;
        }

        public async Task AtualizarAnuncio(Anuncio anuncio)
        {
            var validarMarca = anuncio.ValidarPropriedadeString(anuncio.Marca, "Marca");
            var validarModelo = anuncio.ValidarPropriedadeString(anuncio.Modelo, "Modelo");
            var validarVersao = anuncio.ValidarPropriedadeString(anuncio.Versao, "Versão");
            var validarAno = anuncio.ValidarPropriedadeDecimal(anuncio.Ano, "Ano");
            var validarQuilometragem = anuncio.ValidarPropriedadeDecimal(anuncio.Quilometragem, "Quilometragem");

            if (validarMarca && validarModelo && validarVersao && validarAno && validarQuilometragem)
                await _IRepositorioAnuncio.Atualizar(anuncio);
        }

        public async Task<Anuncio> ConsultarAnuncio(int ID)
        {
            return await _IRepositorioAnuncio.Consultar(ID);
        }

        public async Task IncluirAnuncio(Anuncio anuncio)
        {
            var validarMarca = anuncio.ValidarPropriedadeString(anuncio.Marca, "Marca");
            var validarModelo = anuncio.ValidarPropriedadeString(anuncio.Modelo, "Modelo");
            var validarVersao = anuncio.ValidarPropriedadeString(anuncio.Versao, "Versão");
            var validarAno = anuncio.ValidarPropriedadeDecimal(anuncio.Ano, "Ano");
            var validarQuilometragem = anuncio.ValidarPropriedadeDecimal(anuncio.Quilometragem, "Quilometragem");

            if (validarMarca && validarModelo && validarVersao && validarAno && validarQuilometragem)
                await _IRepositorioAnuncio.Incluir(anuncio);
        }


        public async Task<List<Anuncio>> ListarAnuncios()
        {
            return await _IRepositorioAnuncio.Listar();
        }

        public async Task RemoverAnuncio(Anuncio anuncio)
        {
            await _IRepositorioAnuncio.Remover(anuncio);
        }
    }
}
