﻿using Lanchonete.Context;
using Lanchonete.Models;
using Lanchonete.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Lanchonete.Repositories
{
    public class LancheRepository : ILancheRepository
    {
        private readonly AppDbContext _context;
        public LancheRepository(AppDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);
        public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(l => l.IsLanchePreferido)
                                                                        .Include(c => c.Categoria);

        public Lanche GetLancheById(int lancheId)
        {
            return _context.Lanches.Include(l => l.LanchesMateriasPrimas)
                                   .ThenInclude(lmp => lmp.MateriaPrima)
                                   .FirstOrDefault(l => l.LancheId == lancheId);
        }
    }
}
