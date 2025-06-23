﻿using Lanchonete.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Lanchonete.Components
{
    public class CategoriaMenu : ViewComponent
    {
        public readonly ICategoriaRepository _categoriaRepository;

        public CategoriaMenu(ICategoriaRepository categoriaRepository)
        {
            _categoriaRepository = categoriaRepository;
        }
        public IViewComponentResult Invoke()
        {
            var categorias = _categoriaRepository.Categorias.OrderBy(c => c.CategoriaNome);
            return View(categorias);
        }
    }
}
