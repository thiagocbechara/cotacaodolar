using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CotacaoDolar.Models;
using CotacaoDolar.Infra;

namespace CotacaoDolar.Controllers
{
    public class HomeController : Controller
    {
        private readonly CotacaoRequest _cotacaoRequest;

        public HomeController(CotacaoRequest cotacaoRequest)
        {
            _cotacaoRequest = cotacaoRequest;
        }

        [ResponseCache(Duration = 60 * 60, NoStore = false, Location = ResponseCacheLocation.Client)]
        public async Task<IActionResult> Index()
        {
            return View(await _cotacaoRequest.GetCotacao());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}