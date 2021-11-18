using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Teste.WebMotors.Api.Models;
using Teste.WebMotors.Aplicacao;
using Teste.WebMotors.Aplicacao.Interfaces;
using Teste.WebMotors.Entidades.Entidades;
using Teste.WebMotors.Entidades.Notificacoes;

namespace Teste.WebMotors.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnuncioController : ControllerBase
    {

        private readonly IAplicacaoAnuncio _IAplicacaoAnuncio;

        public AnuncioController(IAplicacaoAnuncio iAplicacaoAnuncio)
        {
            _IAplicacaoAnuncio = iAplicacaoAnuncio;
        }

        [Produces("application/json")]
        [HttpPost("/api/IncluirAnuncio")]
        public async Task<List<Notifica>> IncluirAnuncio(AnuncioDTO anuncioDto)
        {
            var anuncio = new Anuncio()
            {
                Marca = anuncioDto.Marca,
                Modelo = anuncioDto.Modelo,
                Versao = anuncioDto.Versao,
                Ano = anuncioDto.Ano,
                Quilometragem = anuncioDto.Quilometragem,
                Observacao = anuncioDto.Observacao
            };

            await _IAplicacaoAnuncio.IncluirAnuncio(anuncio);

            return anuncio.Notificacoes;
        }

        [Produces("application/json")]
        [HttpPost("/api/AtualizaAnuncio")]
        public async Task<List<Notifica>> AtualizaAnuncio(AnuncioDTO anuncioDto)
        {
            var anuncio = await _IAplicacaoAnuncio.ConsultarAnuncio(anuncioDto.Id);

            anuncio.Marca = anuncioDto.Marca;
            anuncio.Modelo = anuncioDto.Modelo;
            anuncio.Versao = anuncioDto.Versao;
            anuncio.Ano = anuncioDto.Ano;
            anuncio.Quilometragem = anuncioDto.Quilometragem;
            anuncio.Observacao = anuncioDto.Observacao;

            await _IAplicacaoAnuncio.AtualizaAnuncio(anuncio);

            return anuncio.Notificacoes;
        }

        [Produces("application/json")]
        [HttpDelete("/api/RemoverAnuncio")]
        public async Task<List<Notifica>> RemoverAnuncio(int id)
        {
            var anuncio = await _IAplicacaoAnuncio.ConsultarAnuncio(id);
            await _IAplicacaoAnuncio.RemoverAnuncio(anuncio);
            return anuncio.Notificacoes;

        }

        [Produces("application/json")]
        [HttpGet("/api/ConsultarAnuncio")]
        public async Task<Anuncio> ConsultarAnuncio(int id)
        {
            var anuncio = await _IAplicacaoAnuncio.ConsultarAnuncio(id);

            return anuncio;
        }

        [Produces("application/json")]
        [HttpGet("/api/ListarAnuncios")]
        public async Task<List<Anuncio>> ListarAnuncios()
        {
            var anuncios = await _IAplicacaoAnuncio.ListarAnuncios();

            return anuncios;
        }
    }
}
