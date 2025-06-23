using Lanchonete.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Lanchonete.ViewModels
{
    public class MateriaPrimaItemViewModel
    {
        public int MateriaPrimaId { get; set; }

        public string NomeMateriaPrima { get; set; }
        public string UnidadeMedida { get; set; }

        public double QuantidadeUtilizada { get; set; }
        public bool Selecionado { get; set; }
    }

    public class LancheViewModel
    {
        public Lanche Lanche { get; set; }

        [BindNever]
        public List<MateriaPrimaItemViewModel> MateriasPrimasParaSelecao { get; set; }
    }
}