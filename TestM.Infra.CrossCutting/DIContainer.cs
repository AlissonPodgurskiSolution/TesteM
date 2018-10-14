using SimpleInjector;
using TesteM.Application;
using TesteM.Application.Interfaces;
using TesteM.Domain.Interfaces.Repositories;
using TesteM.Domain.Interfaces.Services;
using TesteM.Domain.Services;
using TesteM.Infra.Data.Repositories;

namespace TestM.Infra.CrossCutting
{
    public static class DiContainer
    {
        public static Container RegisterDependencies()
        {
            var container = new Container();

            container.Register(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            container.Register<IClienteRepository, ClienteRepository>();
            container.Register<IServicoPrestadoRepository, ServicoPrestadoRepository>();
            container.Register<ITipoServicoRepository, TipoServicoRepository>();
            container.Register<IFornecedorRepository, FornecedorRepository>();
            container.Register<IClienteFornecedorRepository, ClienteFornecedorRepository>();

            container.Register(typeof(IServiceBase<>), typeof(ServiceBase<>));
            container.Register<IClienteService, ClienteService>();
            container.Register<IClienteFornecedorService, ClienteFornecedorService>();
            container.Register<IFornecedorService, FornecedorService>();
            container.Register<IServicoPrestadoService, ServicoPrestadoService>();
            container.Register<ITipoServicoService, TipoServicoService>();

            container.Register(typeof(IAppServiceBase<>), typeof(AppServiceBase<>));
            container.Register<IServicoPrestadoAppService, ServicoPrestadoAppService>();

            return container;
        }
    }
}