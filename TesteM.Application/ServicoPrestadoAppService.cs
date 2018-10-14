using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TesteM.Application.Interfaces;
using TesteM.Application.ViewModels;
using TesteM.Domain.Entities;
using TesteM.Domain.Interfaces.Services;

namespace TesteM.Application
{
    public class ServicoPrestadoAppService : AppServiceBase<ServicoPrestado>, IServicoPrestadoAppService
    {
        private readonly IClienteFornecedorService _clienteFornecedorService;
        private readonly IClienteService _clienteService;
        private readonly IFornecedorService _fornecedorService;
        private readonly IServicoPrestadoService _servicoPrestadoService;
        private readonly ITipoServicoService _tipoServicoService;

        public ServicoPrestadoAppService(IServicoPrestadoService servicoPrestadoService,
            IFornecedorService fornecedorService, IClienteService clienteService,
            ITipoServicoService tipoServicoService, IClienteFornecedorService clienteFornecedorService)
            : base(servicoPrestadoService)
        {
            _servicoPrestadoService = servicoPrestadoService;
            _fornecedorService = fornecedorService;
            _clienteService = clienteService;
            _tipoServicoService = tipoServicoService;
            _clienteFornecedorService = clienteFornecedorService;
        }

        public IEnumerable<ServicoPrestadoViewModel> ObterServicoPrestados()
        {
            return Mapper.Map<IEnumerable<ServicoPrestado>, IEnumerable<ServicoPrestadoViewModel>>(
                _servicoPrestadoService.ObterServicoPrestados());
        }

        public ServicoPrestadoViewModel InserirServicoPrestado(ServicoPrestadoViewModel servicoPrestadoViewModel)
        {
            ClienteFornecedor clienteFornecedor = null;
            var prestadoViewModel = Mapper.Map<ServicoPrestadoViewModel, ServicoPrestado>(servicoPrestadoViewModel);
            clienteFornecedor = _clienteFornecedorService.GetAll().FirstOrDefault(x =>
                x.FornecedorId == servicoPrestadoViewModel.FornecedorId &&
                x.ClienteId == servicoPrestadoViewModel.ClienteId);

            if (clienteFornecedor != null)
            {
                prestadoViewModel.ClienteFornecedorId = clienteFornecedor.Id;
            }
            else
            {
                _clienteFornecedorService.Add(new ClienteFornecedor(servicoPrestadoViewModel.ClienteId,
                    servicoPrestadoViewModel.FornecedorId));
                prestadoViewModel.ClienteFornecedorId = _clienteFornecedorService.GetAll().FirstOrDefault(x =>
                    x.FornecedorId == servicoPrestadoViewModel.FornecedorId &&
                    x.ClienteId == servicoPrestadoViewModel.ClienteId).Id;
            }
            _servicoPrestadoService.Add(prestadoViewModel);
            return Mapper.Map<ServicoPrestado, ServicoPrestadoViewModel>(
                _servicoPrestadoService.GetById(prestadoViewModel.Id));
        }

        public bool RemoverServicoPrestado(int id)
        {
            try
            {
                _servicoPrestadoService.Remove(_servicoPrestadoService.GetById(id));
                return true;
            }
            catch (Exception e)
            {
                return false;
            }
        }

        public IEnumerable<ServicoPrestadoViewModel> ObterServicoPrestadoPorFiltro(
            FiltroRelatorioViewModel filtroRelatorioViewModel)
        {
            var query = _servicoPrestadoService.GetAll().AsQueryable();

            if (filtroRelatorioViewModel.PeriodoDe != null)
                query = query.Where(x => x.DataAtendimento >= filtroRelatorioViewModel.PeriodoDe);

            if (filtroRelatorioViewModel.PeriodoAte != null)
                query = query.Where(x => x.DataAtendimento <= filtroRelatorioViewModel.PeriodoAte.Value.AddHours(23).AddMinutes(59).AddSeconds(59));

            if (!string.IsNullOrEmpty(filtroRelatorioViewModel.Bairro))
                query = query.Where(x => x.ClienteFornecedor.Cliente.Bairro.Contains(filtroRelatorioViewModel.Bairro));

            if (!string.IsNullOrEmpty(filtroRelatorioViewModel.Cidade))
                query = query.Where(x => x.ClienteFornecedor.Cliente.Cidade.Contains(filtroRelatorioViewModel.Cidade));

            if (!string.IsNullOrEmpty(filtroRelatorioViewModel.Estado))
                query = query.Where(x => x.ClienteFornecedor.Cliente.Estado.Contains(filtroRelatorioViewModel.Estado));

            if (filtroRelatorioViewModel.ClienteId != null)
                query = query.Where(x => x.ClienteFornecedor.Cliente.Id == filtroRelatorioViewModel.ClienteId);

            if (filtroRelatorioViewModel.ValorMaximo != null)
                query = query.Where(x => x.ValorServico <= filtroRelatorioViewModel.ValorMaximo);

            if (filtroRelatorioViewModel.ValorMinimo != null)
                query = query.Where(x => x.ValorServico >= filtroRelatorioViewModel.ValorMinimo);

            if (filtroRelatorioViewModel.TipoServicoId != null)
                query = query.Where(x => x.TipoServico.Id == filtroRelatorioViewModel.TipoServicoId);

            if (filtroRelatorioViewModel.FornecedorId != null)
                query = query.Where(x => x.ClienteFornecedor.Fornecedor.Id == filtroRelatorioViewModel.FornecedorId);

            var servicoPrestados = query.ToList();
            return Mapper.Map<IEnumerable<ServicoPrestado>, IEnumerable<ServicoPrestadoViewModel>>(servicoPrestados);
        }

        public IEnumerable<FornecedorViewModel> ObterFornecedores()
        {
            return Mapper.Map<IEnumerable<Fornecedor>, IEnumerable<FornecedorViewModel>>(_fornecedorService.GetAll());
        }

        public IEnumerable<ClienteViewModel> ObterClientes()
        {
            return Mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(_clienteService.GetAll());
        }

        public IEnumerable<TipoServicoViewModel> ObterTiposServicos()
        {
            return Mapper
                .Map<IEnumerable<TipoServico>, IEnumerable<TipoServicoViewModel>>(_tipoServicoService.GetAll());
        }

        public List<QuadroInformacoesTresClientesMaisGastaramMesViewModel>
            ListarQuadroInformacoesTresClientesMaisGastaramMesViewModel()
        {
            var lista = new List<QuadroInformacoesTresClientesMaisGastaramMesViewModel>();

            var servicoPrestadoViewModels = _servicoPrestadoService.ObterServicoPrestados().ToList();
            var listaAgrupada = servicoPrestadoViewModels.Where(x => x.DataAtendimento.Year == DateTime.Now.Year)
                .GroupBy(x =>
                    new {x.DataAtendimento.Month, x.ClienteFornecedor.Cliente.Nome})
                .Select(x => new QuadroInformacoesTresClientesMaisGastaramMesViewModel
                    {ValorTotal = x.Sum(c => c.ValorServico).ToString("N2"), Mes = x.Key.Month, Nome = x.Key.Nome})
                .OrderBy(x => x.Mes)
                .Distinct();

            foreach (var mes in listaAgrupada.Select(x => x.Mes).Distinct())
                lista.AddRange(listaAgrupada.Where(x => x.Mes == mes).OrderByDescending(x => (decimal.Parse(x.ValorTotal))).Take(3));

            return lista.ToList();
        }

        public List<QuadroInformacoesFornecedorTipoServicoViewModel>
            ListarQuadroInformacoesTresFornecedoreMesMedia()
        {
            var lista = new List<QuadroInformacoesFornecedorTipoServicoViewModel>();

            var servicoPrestadoViewModels = _servicoPrestadoService.ObterServicoPrestados().ToList();
            var listaAgrupada = servicoPrestadoViewModels.Where(x => x.DataAtendimento.Year == DateTime.Now.Year)
                .GroupBy(x =>
                    new {x.TipoServico.Tipo, x.ClienteFornecedor.Fornecedor.Nome})
                .Select(x => new QuadroInformacoesFornecedorTipoServicoViewModel
                {
                    ValorTotal = x.Average(c => c.ValorServico).ToString("N2"),
                    TipoServico = x.Key.Tipo,
                    Fornecedor = x.Key.Nome
                }).OrderBy(x => x.Fornecedor)
                .Distinct();

            return listaAgrupada.ToList();
        }

        public List<FornecedorMesNaoTrabalhadoViewModel> ListarQuadroInformacoesFornecedoresSemPrestarServicoViewModel()
        {
            var fornecedores = _fornecedorService.GetAll();

            List<FornecedorMesNaoTrabalhadoViewModel> listaFornecedorMesNaoTrabalhadoViewModels =
                new List<FornecedorMesNaoTrabalhadoViewModel>();

            for (int mes = 1; mes < 13; mes++)
            {
                foreach (var fornecedor in fornecedores)
                {
                    if (_servicoPrestadoService.ObterServicoPrestados().Where(y => y.DataAtendimento.Month == mes && y.DataAtendimento.Year == DateTime.Now.Year).All(x => x.ClienteFornecedor.FornecedorId != fornecedor.Id))
                    {
                        listaFornecedorMesNaoTrabalhadoViewModels.Add(new FornecedorMesNaoTrabalhadoViewModel(mes, fornecedor.Nome));
                    }

                }
            }
            return listaFornecedorMesNaoTrabalhadoViewModels;
        }
    }
}