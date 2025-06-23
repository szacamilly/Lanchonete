using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Lanchonete.Context;
using Lanchonete.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace Lanchonete.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdminRegistroCaixaDiariosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public AdminRegistroCaixaDiariosController(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }


        public async Task<IActionResult> Index(DateTime? dataCaixa)
        {

            var dataBusca = dataCaixa ?? DateTime.Today;


            var registro = await _context.RegistrosCaixaDiario
                                        .FirstOrDefaultAsync(r => r.DataCaixa.Date == dataBusca.Date);


            registro = registro ?? new RegistroCaixaDiario { DataCaixa = dataBusca, SaldoAbertura = 0.00m };


            if (!registro.CaixaFechado)
            {
                var transacoesDoDia = await _context.TransacoesFinanceiras
                                                    .Where(t => t.DataTransacao.Date == dataBusca.Date)
                                                    .ToListAsync();

                registro.TotalEntradas = transacoesDoDia.Where(t => t.Tipo == TipoTransacao.Entrada).Sum(t => t.Valor);
                registro.TotalSaidas = transacoesDoDia.Where(t => t.Tipo == TipoTransacao.Saida).Sum(t => t.Valor);
                registro.SaldoFechamento = registro.SaldoAbertura + registro.TotalEntradas - registro.TotalSaidas;
            }

            ViewBag.DataCaixaSelecionada = dataBusca.ToString("yyyy-MM-dd");

            if (registro.Id == 0)
            {
                ViewBag.Mensagem = "Caixa não aberto para esta data.";
            }
            else if (registro.CaixaFechado)
            {
                ViewBag.Mensagem = $"Caixa para {registro.DataCaixa.ToShortDateString()} está FECHADO. Saldo Final: {registro.SaldoFechamento:C2}";
            }

            return View(registro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AbrirCaixa([Bind("DataCaixa,SaldoAbertura")] RegistroCaixaDiario novoRegistro)
        {
            var caixaExistente = await _context.RegistrosCaixaDiario
                                                .AnyAsync(r => r.DataCaixa.Date == novoRegistro.DataCaixa.Date);

            if (caixaExistente)
            {
                ModelState.AddModelError("", "Já existe um caixa aberto para esta data.");
            }

            if (ModelState.IsValid)
            {
                _context.Add(novoRegistro);
                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = $"Caixa aberto para {novoRegistro.DataCaixa.ToShortDateString()} com saldo inicial de {novoRegistro.SaldoAbertura:C2}.";
                return RedirectToAction(nameof(Index), new { dataCaixa = novoRegistro.DataCaixa.ToString("yyyy-MM-dd") });
            }

            ViewBag.DataCaixaSelecionada = novoRegistro.DataCaixa.ToString("yyyy-MM-dd");
            return View("Index", novoRegistro);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FecharCaixa([Bind("Id")] RegistroCaixaDiario registroParaFechar)
        {

            var registroDB = await _context.RegistrosCaixaDiario.FindAsync(registroParaFechar.Id);

            if (registroDB == null)
            {
                return NotFound();
            }

            if (registroDB.CaixaFechado)
            {
                ModelState.AddModelError("", "O caixa para esta data já está fechado.");
            }


            if (ModelState.IsValid && !registroDB.CaixaFechado)
            {
                var transacoesDoDia = await _context.TransacoesFinanceiras
                                                    .Where(t => t.DataTransacao.Date == registroDB.DataCaixa.Date)
                                                    .ToListAsync();

                registroDB.TotalEntradas = transacoesDoDia.Where(t => t.Tipo == TipoTransacao.Entrada).Sum(t => t.Valor);
                registroDB.TotalSaidas = transacoesDoDia.Where(t => t.Tipo == TipoTransacao.Saida).Sum(t => t.Valor);
                registroDB.SaldoFechamento = registroDB.SaldoAbertura + registroDB.TotalEntradas - registroDB.TotalSaidas;
                registroDB.CaixaFechado = true;

                _context.Update(registroDB);
                await _context.SaveChangesAsync();

                TempData["MensagemSucesso"] = $"Caixa de {registroDB.DataCaixa.ToShortDateString()} fechado com sucesso. Saldo Final: {registroDB.SaldoFechamento:C2}.";
                return RedirectToAction(nameof(Index), new { dataCaixa = registroDB.DataCaixa.ToString("yyyy-MM-dd") });
            }
            var transacoesDoDiaAtual = await _context.TransacoesFinanceiras
                                                    .Where(t => t.DataTransacao.Date == registroDB.DataCaixa.Date)
                                                    .ToListAsync();
            registroDB.TotalEntradas = transacoesDoDiaAtual.Where(t => t.Tipo == TipoTransacao.Entrada).Sum(t => t.Valor);
            registroDB.TotalSaidas = transacoesDoDiaAtual.Where(t => t.Tipo == TipoTransacao.Saida).Sum(t => t.Valor);
            registroDB.SaldoFechamento = registroDB.SaldoAbertura + registroDB.TotalEntradas - registroDB.TotalSaidas;

            ViewBag.DataCaixaSelecionada = registroDB.DataCaixa.ToString("yyyy-MM-dd"); 
            return View("Index", registroDB);
        }

        public async Task<IActionResult> Historico()
        {
            var historico = await _context.RegistrosCaixaDiario.OrderByDescending(r => r.DataCaixa).ToListAsync();
            return View(historico);
        }
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.RegistrosCaixaDiario == null) { return NotFound(); }
            var registroCaixaDiario = await _context.RegistrosCaixaDiario.FirstOrDefaultAsync(m => m.Id == id);
            if (registroCaixaDiario == null) { return NotFound(); }
            return View(registroCaixaDiario);
        }

        public IActionResult Create() { return View(); }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,DataCaixa,SaldoAbertura,TotalEntradas,TotalSaidas,SaldoFechamento,CaixaFechado")] RegistroCaixaDiario registroCaixaDiario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(registroCaixaDiario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(registroCaixaDiario);
        }
        public async Task<IActionResult> Edit(int? id) { /* ... */ return View(); }
        [HttpPost]
        public async Task<IActionResult> Edit(int id, [Bind("Id,DataCaixa,SaldoAbertura,TotalEntradas,TotalSaidas,SaldoFechamento,CaixaFechado")] RegistroCaixaDiario registroCaixaDiario) { /* ... */ return View(); }
        public async Task<IActionResult> Delete(int? id) { /* ... */ return View(); }
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id) { /* ... */ return RedirectToAction(nameof(Index)); }

        private bool RegistroCaixaDiarioExists(int id)
        {
            return (_context.RegistrosCaixaDiario?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}