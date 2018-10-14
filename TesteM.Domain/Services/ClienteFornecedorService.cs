using TesteM.Domain.Entities;
using TesteM.Domain.Interfaces.Repositories;
using TesteM.Domain.Interfaces.Services;

namespace TesteM.Domain.Services
{
    public class ClienteFornecedorService : ServiceBase<ClienteFornecedor>, IClienteFornecedorService
    {
        private readonly IClienteFornecedorRepository _clienteFornecedorRepository;

        public ClienteFornecedorService(IClienteFornecedorRepository clienteFornecedorRepository)
            : base(clienteFornecedorRepository)
        {
            _clienteFornecedorRepository = clienteFornecedorRepository;
        }

    }
}