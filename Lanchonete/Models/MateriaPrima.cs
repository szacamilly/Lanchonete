namespace Lanchonete.Models
{
    public class MateriaPrima
    {
        public int MateriaPrimaId { get; set; }
        public string Nome { get; set; }
        public string UnidadeMedida { get; set; }
        public int EstoqueMinimo { get; set; }

        public int LancheId { get; set; }
        public virtual Lanche Lanche { get; set; }
    }
}
