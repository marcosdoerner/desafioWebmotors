using Microsoft.EntityFrameworkCore;
using Teste.WebMotors.Dominio.Interfaces.InterfaceRepositorio;
using Teste.WebMotors.Entidades.Entidades;
using Teste.WebMotors.Infraestrutura.Configuracoes;

namespace Teste.WebMotors.Infraestrutura.Repositorio
{
    public class RepositorioAnuncio : RepositorioGenerico<Anuncio>, IRepositorioAnuncio
    {
        private readonly DbContextOptions<Contexto> _optionsbuilder;

        public RepositorioAnuncio()
        {
            _optionsbuilder = new DbContextOptions<Contexto>();
        }
    }
}
