using System.Collections.Generic;
using Antlr.Runtime.Misc;
using TesteM.Application.ViewModels;

namespace TesteM.Web.MVC.Models
{
    public class HomeViewModel
    {
        public List<QuadroInformacoesTresClientesMaisGastaramMesViewModel>
            ListaQuadroInformacoesTresClientesMaisGastaramMesViewModel
        { get; set; }

        public List<QuadroInformacoesFornecedorTipoServicoViewModel> ListaQuadroInformacoesTresFornecedoreMesMedia
        {
            get;
            set;
        }

        public List<FornecedorMesNaoTrabalhadoViewModel> ListaFornecedorMesNaoTrabalhadoViewModel
        {
            get;
            set;
        }

        public HomeViewModel()
        {
            ListaQuadroInformacoesTresClientesMaisGastaramMesViewModel = new List<QuadroInformacoesTresClientesMaisGastaramMesViewModel>();
            ListaQuadroInformacoesTresFornecedoreMesMedia = new ListStack<QuadroInformacoesFornecedorTipoServicoViewModel>();
            ListaFornecedorMesNaoTrabalhadoViewModel = new List<FornecedorMesNaoTrabalhadoViewModel>();
        }
    }
}