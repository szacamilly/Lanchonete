using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lanchonete.Context;
using Lanchonete.Models;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;
using Lanchonete.ViewModels;

namespace Lanchonete.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminLanchesController : Controller
    {
        private readonly AppDbContext _context;

        public AdminLanchesController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var resultado = _context.Lanches.Include(l => l.Categoria).AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(p => p.Nome.Contains(filter));
            }

            var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };
            return View(model);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanches
                .Include(l => l.Categoria)
                .Include(l => l.LanchesMateriasPrimas)
                .ThenInclude(lmp => lmp.MateriaPrima)
                .FirstOrDefaultAsync(m => m.LancheId == id);
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.CategoriaId = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome");

            var todasMateriasPrimas = await _context.MateriasPrimas
                                                    .OrderBy(mp => mp.Nome)
                                                    .ToListAsync();

            var materiasPrimasParaSelecao = todasMateriasPrimas.Select(mp => new MateriaPrimaItemViewModel
            {
                MateriaPrimaId = mp.MateriaPrimaId,
                NomeMateriaPrima = mp.Nome,
                UnidadeMedida = mp.UnidadeMedida,
                QuantidadeUtilizada = 0,
                Selecionado = false
            }).ToList();

            var viewModel = new LancheViewModel
            {
                Lanche = new Lanche(),
                MateriasPrimasParaSelecao = materiasPrimasParaSelecao
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LancheViewModel lancheViewModel)
        {
            ModelState.Remove("Lanche.Categoria");
            ModelState.Remove("Lanche.LanchesMateriasPrimas");
            ModelState.Remove("MateriasPrimasParaSelecao");

            if (ModelState.IsValid)
            {
                var lanche = lancheViewModel.Lanche;

                _context.Add(lanche);
                await _context.SaveChangesAsync();

                if (lancheViewModel.MateriasPrimasParaSelecao != null)
                {
                    foreach (var item in lancheViewModel.MateriasPrimasParaSelecao)
                    {
                        if (item.Selecionado && item.QuantidadeUtilizada > 0)
                        {
                            var lancheMateriaPrima = new LancheMateriaPrima
                            {
                                LancheId = lanche.LancheId,
                                MateriaPrimaId = item.MateriaPrimaId,
                                QuantidadeUtilizada = item.QuantidadeUtilizada
                            };
                            _context.LanchesMateriasPrimas.Add(lancheMateriaPrima);
                        }
                    }
                    await _context.SaveChangesAsync();
                }

                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoriaId = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", lancheViewModel.Lanche.CategoriaId);

            var todasMateriasPrimas = await _context.MateriasPrimas
                                                    .OrderBy(mp => mp.Nome)
                                                    .ToListAsync();

            var submittedMateriasPrimas = lancheViewModel.MateriasPrimasParaSelecao ?? new List<MateriaPrimaItemViewModel>();

            lancheViewModel.MateriasPrimasParaSelecao = todasMateriasPrimas.Select(mp => new MateriaPrimaItemViewModel
            {
                MateriaPrimaId = mp.MateriaPrimaId,
                NomeMateriaPrima = mp.Nome,
                UnidadeMedida = mp.UnidadeMedida,
                QuantidadeUtilizada = submittedMateriasPrimas.FirstOrDefault(item => item.MateriaPrimaId == mp.MateriaPrimaId)?.QuantidadeUtilizada ?? 0,
                Selecionado = submittedMateriasPrimas.Any(item => item.MateriaPrimaId == mp.MateriaPrimaId && item.Selecionado)
            }).ToList();

            return View(lancheViewModel);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanches
                                       .Include(l => l.Categoria)
                                       .Include(l => l.LanchesMateriasPrimas)
                                           .ThenInclude(lmp => lmp.MateriaPrima)
                                       .FirstOrDefaultAsync(m => m.LancheId == id);

            if (lanche == null)
            {
                return NotFound();
            }

            ViewBag.CategoriaId = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", lanche.CategoriaId);

            var todasMateriasPrimas = await _context.MateriasPrimas.OrderBy(mp => mp.Nome).ToListAsync();
            var materiasPrimasParaSelecao = todasMateriasPrimas.Select(mp => new MateriaPrimaItemViewModel
            {
                MateriaPrimaId = mp.MateriaPrimaId,
                NomeMateriaPrima = mp.Nome,
                UnidadeMedida = mp.UnidadeMedida,
                QuantidadeUtilizada = lanche.LanchesMateriasPrimas
                                             .FirstOrDefault(lmp => lmp.MateriaPrimaId == mp.MateriaPrimaId)?.QuantidadeUtilizada ?? 0,
                Selecionado = lanche.LanchesMateriasPrimas.Any(lmp => lmp.MateriaPrimaId == mp.MateriaPrimaId)
            }).ToList();

            var viewModel = new LancheViewModel
            {
                Lanche = lanche,
                MateriasPrimasParaSelecao = materiasPrimasParaSelecao
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, LancheViewModel lancheViewModel)
        {
            if (id != lancheViewModel.Lanche.LancheId)
            {
                return NotFound();
            }

            ModelState.Remove("Lanche.Categoria");
            ModelState.Remove("Lanche.LanchesMateriasPrimas");
            ModelState.Remove("MateriasPrimasParaSelecao");

            if (ModelState.IsValid)
            {
                try
                {
                    var lancheOriginal = await _context.Lanches
                                                      .Include(l => l.LanchesMateriasPrimas)
                                                      .FirstOrDefaultAsync(l => l.LancheId == id);

                    if (lancheOriginal == null)
                    {
                        return NotFound();
                    }

                    _context.Entry(lancheOriginal).CurrentValues.SetValues(lancheViewModel.Lanche);

                    _context.LanchesMateriasPrimas.RemoveRange(lancheOriginal.LanchesMateriasPrimas);

                    var newLancheMateriasPrimas = new List<LancheMateriaPrima>();
                    if (lancheViewModel.MateriasPrimasParaSelecao != null)
                    {
                        foreach (var item in lancheViewModel.MateriasPrimasParaSelecao)
                        {
                            if (item.Selecionado && item.QuantidadeUtilizada > 0)
                            {
                                newLancheMateriasPrimas.Add(new LancheMateriaPrima
                                {
                                    LancheId = lancheOriginal.LancheId,
                                    MateriaPrimaId = item.MateriaPrimaId,
                                    QuantidadeUtilizada = item.QuantidadeUtilizada
                                });
                            }
                        }
                    }

                    _context.LanchesMateriasPrimas.AddRange(newLancheMateriasPrimas);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LancheExists(lancheViewModel.Lanche.LancheId))
                    {
                        return NotFound();
                    }
                    // Adicione um retorno aqui para garantir que todos os caminhos retornam um valor
                    // Se o Lanche não existe, retornamos NotFound. Se existe, mas há concorrência, relançamos.
                    // O compilador está pedindo um retorno explícito para este caminho do catch.
                    // Se você quer que o usuário veja os erros, retorne a view. Se for um erro que impede
                    // continuar, lance a exceção.
                    // Para o erro de compilação, podemos adicionar um return View(lancheViewModel); aqui.
                    return View(lancheViewModel); // <--- Adicionado este retorno para satisfazer o compilador
                }
                return RedirectToAction(nameof(Index));
            }

            ViewBag.CategoriaId = new SelectList(_context.Categorias, "CategoriaId", "CategoriaNome", lancheViewModel.Lanche.CategoriaId);

            var todasMateriasPrimas = await _context.MateriasPrimas
                                                    .OrderBy(mp => mp.Nome)
                                                    .ToListAsync();

            var submittedMateriasPrimas = lancheViewModel.MateriasPrimasParaSelecao ?? new List<MateriaPrimaItemViewModel>();

            lancheViewModel.MateriasPrimasParaSelecao = todasMateriasPrimas.Select(mp => new MateriaPrimaItemViewModel
            {
                MateriaPrimaId = mp.MateriaPrimaId,
                NomeMateriaPrima = mp.Nome,
                UnidadeMedida = mp.UnidadeMedida,
                QuantidadeUtilizada = submittedMateriasPrimas.FirstOrDefault(item => item.MateriaPrimaId == mp.MateriaPrimaId)?.QuantidadeUtilizada ?? 0,
                Selecionado = submittedMateriasPrimas.Any(item => item.MateriaPrimaId == mp.MateriaPrimaId && item.Selecionado)
            }).ToList();

            return View(lancheViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lanche = await _context.Lanches
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.LancheId == id);
            if (lanche == null)
            {
                return NotFound();
            }

            return View(lanche);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lanche = await _context.Lanches.FindAsync(id);
            _context.Lanches.Remove(lanche);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LancheExists(int id)
        {
            return (_context.Lanches?.Any(e => e.LancheId == id)).GetValueOrDefault();
        }
    }
}