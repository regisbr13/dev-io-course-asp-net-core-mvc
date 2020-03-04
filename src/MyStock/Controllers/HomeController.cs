using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyStock.ViewModels;

namespace MyStock.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [Route("Erro/{id:length(3,3)}")]
        public IActionResult Error(int id)
        {
            var error = new ErrorViewModel();

            if (id == 500)
            {
                error.Title = "Ocorreu um erro!";
                error.Message = "Tente novamente mais tarde ou contate nosso suporte.";
                error.Code = id;
            }
            else if (id == 404)
            {
                error.Title = "Página não encontrada!";
                error.Message = "A página procurada não existe.";
                error.Code = id;
            }
            else if (id == 403)
            {
                error.Title = "Acesso negado!";
                error.Message = "Você não tem permissão para acessar este conteúdo.";
                error.Code = id;
            }
            else
            {
                return StatusCode(404);
            }

            return View("Error", error);
        }
    }
}
