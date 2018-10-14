using TesteM.Domain.Entities;
using TesteM.Domain.Interfaces.Repositories;
using TesteM.Domain.Interfaces.Services;

namespace TesteM.Domain.Services
{
    public class FornecedorService : ServiceBase<Fornecedor>, IFornecedorService
    {
        private readonly IFornecedorRepository _fornecedorRepository;

        public FornecedorService(IFornecedorRepository fornecedorRepository)
            : base(fornecedorRepository)
        {
            _fornecedorRepository = fornecedorRepository;
        }

    }
}