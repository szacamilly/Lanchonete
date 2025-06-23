using System.ComponentModel.DataAnnotations.Schema;

namespace Lanchonete.Models
{
    [Table("LanchesMateriasPrimas")]
    public class LancheMateriaPrima
    {
        public int LancheId { get; set; }
        public int MateriaPrimaId { get; set; }

        public double QuantidadeUtilizada { get; set; }

        public virtual Lanche Lanche { get; set; }
        public virtual MateriaPrima MateriaPrima { get; set; }
    }
}