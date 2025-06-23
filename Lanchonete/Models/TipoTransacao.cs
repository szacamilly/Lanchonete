using System.ComponentModel.DataAnnotations;

namespace Lanchonete.Models
{
    public enum TipoTransacao
    {
        [Display(Name = "Entrada")]
        Entrada = 1,
        [Display(Name = "Saída")]
        Saida = 2
    }
}