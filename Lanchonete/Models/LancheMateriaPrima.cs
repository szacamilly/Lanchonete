using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lanchonete.Models
{
    [Table("LanchesMateriasPrimas")]
    public class LancheMateriaPrima
    {
        public int LancheId { get; set; }
        public int MateriaPrimaId { get; set; }

        [Required(ErrorMessage = "A quantidade utilizada da matéria-prima é obrigatória.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "A quantidade utilizada deve ser maior que zero.")]
        public double QuantidadeUtilizada { get; set; }

        public virtual Lanche Lanche { get; set; }
        public virtual MateriaPrima MateriaPrima { get; set; }
    }
}