using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Lanchonete.Context;
using Lanchonete.Models;
using Microsoft.AspNetCore.Authorization;

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
    }
}