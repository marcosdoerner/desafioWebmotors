using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Teste.WebMotors.Dominio.Interfaces.InterfaceRepositorio
{
    public interface IRepositorioGenerico<T> where T : class
    {
        Task Incluir(T Objeto);
        Task Atualizar(T Objeto);
        Task Remover(T Objeto);
        Task<T> Consultar(int Id);
        Task<List<T>> Listar();
    }
}
