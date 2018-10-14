using System.Linq;
using System.Web.Mvc;
using TesteM.Application.Interfaces;
using TesteM.Application.ViewModels;

namespace TesteM.Web.MVC.Controllers
{
    public class RelatorioServicoPrestadoController : Controller
    {
        private readonly IServicoPrestadoAppService _servicoPrestadoAppService;

        public RelatorioServicoPrestadoController(IServicoPrestadoAppService servicoPrestadoAppService)
        {
            _servicoPrestadoAppService = servicoPrestadoAppService;
        }

        [Authorize]
        public PartialViewResult Pesquisar(FiltroRelatorioViewModel filtroRelatorioViewModel)
        {
            var servicoPrestadoViewModels = _servicoPrestadoAppService
                .ObterServicoPrestadoPorFiltro(filtroRelatorioViewModel).ToList();
            return PartialView("TabelaRelatorio", servicoPrestadoViewModels);
        }

        // GET: ServicoPrestadoViewModels
        [Authorize]
        public ActionResult Index()
        {
            var servicoPrestadoViewModels = _servicoPrestadoAppService.ObterServicoPrestados().ToList();
            return View("Index", servicoPrestadoViewModels);
        }

        [Authorize]
        [HttpGet]
        public JsonResult ListarBairros() // its a GET, not a POST
        {
            var clientesViewModels =
                _servicoPrestadoAppService.ObterClientes().Select(x => new {x.Bairro}).Distinct().ToList();
            return Json(clientesViewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult ListarCidades() // its a GET, not a POST
        {
            var clientesViewModels =
                _servicoPrestadoAppService.ObterClientes().Select(x => new {x.Cidade}).Distinct().ToList();
            return Json(clientesViewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult ListarEstados() // its a GET, not a POST
        {
            var clientesViewModels =
                _servicoPrestadoAppService.ObterClientes().Select(x => new {x.Estado}).Distinct().ToList();
            return Json(clientesViewModels, JsonRequestBehavior.AllowGet);
        }
    }
}