using System.Collections.Generic;
using TesteM.Domain.Entities;

namespace TesteM.Domain.Interfaces.Services
{
    public interface IServicoPrestadoService : IServiceBase<ServicoPrestado>
    {
        IEnumerable<ServicoPrestado> ObterServicoPrestados();
    }
}