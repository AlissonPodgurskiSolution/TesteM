using TesteM.Domain.Entities;
using TesteM.Domain.Interfaces.Repositories;

namespace TesteM.Infra.Data.Repositories
{
    public class ClienteRepository : RepositoryBase<Cliente>, IClienteRepository
    {
    }
}
