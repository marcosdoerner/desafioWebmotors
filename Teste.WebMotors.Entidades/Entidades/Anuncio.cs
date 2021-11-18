using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Teste.WebMotors.Entidades.Notificacoes;

namespace Teste.WebMotors.Entidades.Entidades
{
    [Table("tb_AnuncioWebmotors")]
    public class Anuncio : Notifica
    {
        [Column("ID")]
        public int Id { get; set; }
        [Column("marca")]
        [MaxLength(45)]
        public string Marca { get; set; }
        [Column("modelo")]
        [MaxLength(45)]
        public string Modelo { get; set; }
        [Column("versao")]
        [MaxLength(45)]
        public string Versao { get; set; }
        [Column("ano")]
        public int Ano { get; set; }
        [Column("quilometragem")]
        public int Quilometragem { get; set; }
        [Column("observacao")]
        public string Observacao { get; set; }
    }
}
