using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lanchonete.Models
{
    [Table("CategoriasFinanceiras")]
    public class CategoriaFinanceira
    {
        [Key]
        public int CategoriaFinanceiraId { get; set; }
        [Required, StringLength(100)]
        public string Nome { get; set; }
        [StringLength(200)]
        public string Descricao { get; set; }
    }
}