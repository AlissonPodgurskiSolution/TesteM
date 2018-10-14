using AutoMapper;
using TesteM.Application.ViewModels;
using TesteM.Domain.Entities;

namespace TesteM.Web.MVC.AutoMapper
{
    public class AutoMapperWebProfile : Profile
    {
        public AutoMapperWebProfile() {
            CreateMap<ClienteFornecedor, ClienteFornecedorViewModel>();
            CreateMap<TipoServico, TipoServicoViewModel>();
            CreateMap<ServicoPrestado, ServicoPrestadoViewModel>();
        }
    }
}