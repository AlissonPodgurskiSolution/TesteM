using System;
using System.Linq;
using System.Web.Mvc;
using TesteM.Application.Interfaces;
using TesteM.Application.ViewModels;

namespace TesteM.Web.MVC.Controllers
{
    public class ServicoPrestadoController : Controller
    {
        private readonly IServicoPrestadoAppService _servicoPrestadoAppService;

        public ServicoPrestadoController(IServicoPrestadoAppService servicoPrestadoAppService)
        {
            _servicoPrestadoAppService = servicoPrestadoAppService;
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
        public JsonResult ListarFornecedores() // its a GET, not a POST
        {
            var fornecedorViewModels = _servicoPrestadoAppService.ObterFornecedores().ToList();
            return Json(fornecedorViewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult ListarClientes() // its a GET, not a POST
        {
            var clientesViewModels =
                _servicoPrestadoAppService.ObterClientes().Select(x => new {x.Id, x.Nome}).ToList();
            return Json(clientesViewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpGet]
        public JsonResult ListarTiposServicos() // its a GET, not a POST
        {
            var tiposServicosViewModels =
                _servicoPrestadoAppService.ObterTiposServicos().Select(x => new {x.Id, x.Tipo}).ToList();
            return Json(tiposServicosViewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult InserirServicoPrestado(string descricaoServico, DateTime dataAtendimento,
            decimal valorServico, int clienteId, int fornecedorId, int tipoServicoId) // its a GET, not a POST
        {
            var servicoPrestadoViewModel =
                new ServicoPrestadoViewModel
                {
                    DescricaoServico = descricaoServico,
                    DataAtendimento = dataAtendimento,
                    ValorServico = valorServico,
                    ClienteId = clienteId,
                    FornecedorId = fornecedorId,
                    TipoServicoId = tipoServicoId
                };


            var tiposServicosViewModels = _servicoPrestadoAppService.InserirServicoPrestado(servicoPrestadoViewModel);
            return Json(tiposServicosViewModels, JsonRequestBehavior.AllowGet);
        }

        [Authorize]
        [HttpPost]
        public JsonResult RemoverServicoPrestado(int id) // its a GET, not a POST
        {
            return Json(_servicoPrestadoAppService.RemoverServicoPrestado(id), JsonRequestBehavior.AllowGet);
        }
    }
}