using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lanchonete.Models
{
    [Table("MateriasPrimas")]
    public class MateriaPrima
    {
        [Key]
        public int MateriaPrimaId { get; set; }

        [Required]
        [StringLength(80)]
        public string Nome { get; set; }

        [Required]
        [StringLength(20)]
        public string UnidadeMedida { get; set; }

        public double EstoqueAtual { get; set; }
        public double EstoqueMinimo { get; set; }

        public List<LancheMateriaPrima> LanchesMateriasPrimas { get; set; }
    }
}