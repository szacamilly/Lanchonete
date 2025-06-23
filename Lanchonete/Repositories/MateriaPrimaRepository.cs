using Lanchonete.Context;
using Lanchonete.Models;
using Lanchonete.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq; // Necessário para FirstOrDefault, Where, etc.
using Microsoft.EntityFrameworkCore; // Se precisar de Includes futuros, embora para MateriaPrima possa não ser tão comum no início

namespace Lanchonete.Repositories
{
    public class MateriaPrimaRepository : IMateriaPrimaRepository
    {
        private readonly AppDbContext _context;

        public MateriaPrimaRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<MateriaPrima> MateriasPrimas => _context.MateriasPrimas.OrderBy(mp => mp.Nome);

        public MateriaPrima GetMateriaPrimaById(int materiaPrimaId)
        {
            return _context.MateriasPrimas.Include(mp => mp.LanchesMateriasPrimas)
                                          .ThenInclude(lmp => lmp.Lanche)
                                          .FirstOrDefault(mp => mp.MateriaPrimaId == materiaPrimaId);
        }
    }
}