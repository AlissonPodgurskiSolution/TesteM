using System.Collections.Generic;
using TesteM.Application.ViewModels;
using TesteM.Domain.Entities;

namespace TesteM.Application.Interfaces
{
    public interface IServicoPrestadoAppService : IAppServiceBase<ServicoPrestado>
    {
        IEnumerable<ServicoPrestadoViewModel> ObterServicoPrestados();

        IEnumerable<FornecedorViewModel> ObterFornecedores();

        IEnumerable<ClienteViewModel> ObterClientes();

        IEnumerable<TipoServicoViewModel> ObterTiposServicos();

        ServicoPrestadoViewModel InserirServicoPrestado(ServicoPrestadoViewModel servicoPrestadoViewModel);

        bool RemoverServicoPrestado(int id);

        IEnumerable<ServicoPrestadoViewModel> ObterServicoPrestadoPorFiltro(FiltroRelatorioViewModel filtroRelatorioViewModel);

        List<QuadroInformacoesTresClientesMaisGastaramMesViewModel>
            ListarQuadroInformacoesTresClientesMaisGastaramMesViewModel();

        List<QuadroInformacoesFornecedorTipoServicoViewModel>
            ListarQuadroInformacoesTresFornecedoreMesMedia();

        List<FornecedorMesNaoTrabalhadoViewModel>
            ListarQuadroInformacoesFornecedoresSemPrestarServicoViewModel();
    }
}