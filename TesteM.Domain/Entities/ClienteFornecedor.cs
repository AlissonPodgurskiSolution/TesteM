using System.Collections.Generic;

namespace TesteM.Domain.Entities
{
    public class ClienteFornecedor
    {
        public ClienteFornecedor(int clienteId, int fornecedorId)
        {
            ClienteId = clienteId;
            FornecedorId = fornecedorId;
        }

        public ClienteFornecedor()
        {
        }

        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int FornecedorId { get; set; }
        public virtual Cliente Cliente { get; set; }
        public virtual Fornecedor Fornecedor { get; set; }
    }
}