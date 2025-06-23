using Lanchonete.Models;
using System.Collections.Generic;

namespace Lanchonete.Repositories.Interfaces
{
    public interface IMateriaPrimaRepository
    {
        IEnumerable<MateriaPrima> MateriasPrimas { get; }

        MateriaPrima GetMateriaPrimaById(int materiaPrimaId);
    }
}