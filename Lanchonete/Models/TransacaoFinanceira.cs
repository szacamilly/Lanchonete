using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lanchonete.Models
{
    [Table("TransacoesFinanceiras")]
    public class TransacaoFinanceira
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "O tipo da transação é obrigatório.")]
        [Display(Name = "Tipo")]
        public TipoTransacao Tipo { get; set; }

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        [StringLength(200, ErrorMessage = "A descrição deve ter no máximo {1} caracteres.")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; }

        [Required(ErrorMessage = "O valor é obrigatório.")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0.01, 999999.99, ErrorMessage = "O valor deve ser positivo.")]
        [Display(Name = "Valor")]
        public decimal Valor { get; set; }

        [Required(ErrorMessage = "A data é obrigatória.")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data")]
        public DateTime DataTransacao { get; set; } = DateTime.Now;

        public int? PedidoId { get; set; }
        public virtual Pedido? Pedido { get; set; }


        public int? CategoriaFinanceiraId { get; set; }
        public virtual CategoriaFinanceira? CategoriaFinanceira { get; set; } // Use '?'
    }
}