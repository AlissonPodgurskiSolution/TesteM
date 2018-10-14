using TesteM.Domain.Entities;
using TesteM.Domain.Interfaces.Repositories;
using TesteM.Domain.Interfaces.Services;

namespace TesteM.Domain.Services
{
    public class TipoServicoService : ServiceBase<TipoServico>, ITipoServicoService
    {
        private readonly ITipoServicoRepository _tipoServicoRepository;

        public TipoServicoService(ITipoServicoRepository tipoServicoRepository)
            : base(tipoServicoRepository)
        {
            _tipoServicoRepository = tipoServicoRepository;
        }
    }
}