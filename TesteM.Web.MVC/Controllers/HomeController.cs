using System.Collections.Generic;
using System.Web.Mvc;
using TesteM.Application.Interfaces;
using TesteM.Application.ViewModels;
using TesteM.Web.MVC.Models;

namespace TesteM.Web.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IServicoPrestadoAppService _servicoPrestadoAppService;

        public HomeController(IServicoPrestadoAppService servicoPrestadoAppService)
        {
            _servicoPrestadoAppService = servicoPrestadoAppService;
        }

        // GET: ServicoPrestadoViewModels
        public ActionResult Index()
        {
            var homeViewModel = new HomeViewModel
            {
                ListaQuadroInformacoesTresClientesMaisGastaramMesViewModel = _servicoPrestadoAppService
                    .ListarQuadroInformacoesTresClientesMaisGastaramMesViewModel()
            };

            homeViewModel.ListaQuadroInformacoesTresFornecedoreMesMedia = _servicoPrestadoAppService.ListarQuadroInformacoesTresFornecedoreMesMedia();
            homeViewModel.ListaFornecedorMesNaoTrabalhadoViewModel = _servicoPrestadoAppService.ListarQuadroInformacoesFornecedoresSemPrestarServicoViewModel();

            return View("Index", homeViewModel);
        }
    }
}
