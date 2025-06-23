using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lanchonete.Models
{
    [Table("RegistrosCaixaDiario")]
    public class RegistroCaixaDiario
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "A data do caixa é obrigatória")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data do Caixa")]
        public DateTime DataCaixa { get; set; } = DateTime.Today; //hoje

        [Required(ErrorMessage = "O saldo de abertura é obrigatório")]
        [Column(TypeName = "decimal(18, 2)")]
        [Range(0, 999999.99, ErrorMessage = "O saldo de abertura deve ser um valor válido.")]
        [Display(Name = "Saldo de Abertura")]
        public decimal SaldoAbertura { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Total de Entradas")]
        public decimal TotalEntradas { get; set; } = 0.00m;

        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Total de Saídas")]
        public decimal TotalSaidas { get; set; } = 0.00m;

        [Column(TypeName = "decimal(18, 2)")]
        [Display(Name = "Saldo de Fechamento")]
        public decimal SaldoFechamento { get; set; } = 0.00m;

        [Display(Name = "Caixa Fechado?")]
        public bool CaixaFechado { get; set; } = false;
    }
}