using System.ComponentModel.DataAnnotations;

namespace Teste.WebMotors.Web.Models
{
    public class AnuncioViewModel
    {
        public int Id { get; set; }
        public int MarcaId { get; set; }
        [Required]
        public string Marca { get; set; }
        public int ModeloId { get; set; }
        [Required]
        public string Modelo { get; set; }
        public int VersaoId { get; set; }
        [Required]
        public string Versao { get; set; }
        [Required]
        public int Ano { get; set; }
        [Required]
        public int Quilometragem { get; set; }
        public string Observacao { get; set; }
    }
}
