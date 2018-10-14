using TesteM.Domain.Entities;
using TesteM.Domain.Interfaces.Repositories;
using TesteM.Domain.Interfaces.Services;

namespace TesteM.Domain.Services
{
    public class ClienteService : ServiceBase<Cliente>, IClienteService
    {
        private readonly IClienteRepository _clienteRepository;

        public ClienteService(IClienteRepository clienteRepository)
            : base(clienteRepository)
        {
            _clienteRepository = clienteRepository;
        }
    }
}