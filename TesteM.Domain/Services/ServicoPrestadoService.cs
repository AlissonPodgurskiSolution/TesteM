using System.Collections.Generic;
using TesteM.Domain.Entities;
using TesteM.Domain.Interfaces.Repositories;
using TesteM.Domain.Interfaces.Services;

namespace TesteM.Domain.Services
{
    public class ServicoPrestadoService : ServiceBase<ServicoPrestado>, IServicoPrestadoService
    {
        private readonly IClienteFornecedorRepository _clienteFornecedorRepository;
        private readonly IServicoPrestadoRepository _servicoPrestadoRepository;

        public ServicoPrestadoService(IServicoPrestadoRepository servicoPrestadoRepository,
            IClienteFornecedorRepository clienteFornecedorRepository)
            : base(servicoPrestadoRepository)
        {
            _servicoPrestadoRepository = servicoPrestadoRepository;
            _clienteFornecedorRepository = clienteFornecedorRepository;
        }

        public IEnumerable<ServicoPrestado> ObterServicoPrestados()
        {
            var servicoPrestados = _servicoPrestadoRepository.GetAll();
            return servicoPrestados;
        }
    }
}