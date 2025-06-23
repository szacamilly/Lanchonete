using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lanchonete.Context;
using Lanchonete.Models;
using Microsoft.AspNetCore.Authorization;
using ReflectionIT.Mvc.Paging;

namespace Lanchonete.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminMateriaPrimasController : Controller
    {
        private readonly AppDbContext _context;

        public AdminMateriaPrimasController(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index(string filter, int pageindex = 1, string sort = "Nome")
        {
            var resultado = _context.MateriasPrimas.AsNoTracking().AsQueryable();

            if (!string.IsNullOrWhiteSpace(filter))
            {
                resultado = resultado.Where(mp => mp.Nome.Contains(filter));
            }
            var model = await PagingList.CreateAsync(resultado, 5, pageindex, sort, "Nome");
            model.RouteValue = new RouteValueDictionary { { "filter", filter } };

            return View(model);
        }

        public async Task<IActionResult> RegistrarCompra()
        {
            ViewBag.MateriasPrimas = new SelectList(await _context.MateriasPrimas.OrderBy(mp => mp.Nome).ToListAsync(), "MateriaPrimaId", "Nome");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegistrarCompra(int MateriaPrimaId, double QuantidadeComprada, double CustoTotal)
        {
            if (MateriaPrimaId <= 0)
            {
                ModelState.AddModelError("MateriaPrimaId", "Selecione uma matéria-prima válida.");
            }
            if (QuantidadeComprada <= 0)
            {
                ModelState.AddModelError("QuantidadeComprada", "A quantidade comprada deve ser maior que zero.");
            }
            if (CustoTotal <= 0)
            {
                ModelState.AddModelError("CustoTotal", "O custo total deve ser maior que zero.");
            }

            if (ModelState.IsValid)
            {
                var materiaPrima = await _context.MateriasPrimas.FindAsync(MateriaPrimaId);

                if (materiaPrima == null)
                {
                    ModelState.AddModelError("", "Matéria-prima não encontrada.");
                    ViewBag.MateriasPrimas = new SelectList(await _context.MateriasPrimas.OrderBy(mp => mp.Nome).ToListAsync(), "MateriaPrimaId", "Nome");
                    return View();
                }

                materiaPrima.EstoqueAtual += QuantidadeComprada;
                _context.Update(materiaPrima);

                var transacaoCompra = new TransacaoFinanceira
                {
                    Tipo = TipoTransacao.Saida,
                    DataTransacao = DateTime.Today,
                    Descricao = $"Compra de {materiaPrima.Nome} ({QuantidadeComprada} {materiaPrima.UnidadeMedida})",
                    Valor = (decimal)CustoTotal,
                };
                _context.TransacoesFinanceiras.Add(transacaoCompra);

                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = $"Compra de {materiaPrima.Nome} registrada. Estoque atualizado e saída de {CustoTotal:C2} lançada no caixa.";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.MateriasPrimas = new SelectList(await _context.MateriasPrimas.OrderBy(mp => mp.Nome).ToListAsync(), "MateriaPrimaId", "Nome");
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MateriaPrimaId,Nome,UnidadeMedida,EstoqueAtual,EstoqueMinimo")] MateriaPrima materiaPrima)
        {
            if (ModelState.IsValid)
            {
                _context.Add(materiaPrima);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = $"Matéria-prima '{materiaPrima.Nome}' criada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(materiaPrima);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.MateriasPrimas == null)
            {
                return NotFound();
            }

            var materiaPrima = await _context.MateriasPrimas
                .FirstOrDefaultAsync(m => m.MateriaPrimaId == id);
            if (materiaPrima == null)
            {
                return NotFound();
            }

            return View(materiaPrima);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.MateriasPrimas == null)
            {
                return NotFound();
            }

            var materiaPrima = await _context.MateriasPrimas.FindAsync(id);
            if (materiaPrima == null)
            {
                return NotFound();
            }
            return View(materiaPrima);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MateriaPrimaId,Nome,UnidadeMedida,EstoqueAtual,EstoqueMinimo")] MateriaPrima materiaPrima)
        {
            if (id != materiaPrima.MateriaPrimaId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(materiaPrima);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MateriaPrimaExists(materiaPrima.MateriaPrimaId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                TempData["MensagemSucesso"] = $"Matéria-prima '{materiaPrima.Nome}' atualizada com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(materiaPrima);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.MateriasPrimas == null)
            {
                return NotFound();
            }

            var materiaPrima = await _context.MateriasPrimas
                .FirstOrDefaultAsync(m => m.MateriaPrimaId == id);
            if (materiaPrima == null)
            {
                return NotFound();
            }

            return View(materiaPrima);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.MateriasPrimas == null)
            {
                return Problem("Entity set 'AppDbContext.MateriasPrimas' is null.");
            }
            var materiaPrima = await _context.MateriasPrimas.FindAsync(id);
            if (materiaPrima != null)
            {
                _context.MateriasPrimas.Remove(materiaPrima);
            }

            await _context.SaveChangesAsync();
            TempData["MensagemSucesso"] = $"Matéria-prima '{materiaPrima?.Nome ?? "Item"}' excluída com sucesso!";
            return RedirectToAction(nameof(Index));
        }

        private bool MateriaPrimaExists(int id)
        {
            return (_context.MateriasPrimas?.Any(e => e.MateriaPrimaId == id)).GetValueOrDefault();
        }
    }
}