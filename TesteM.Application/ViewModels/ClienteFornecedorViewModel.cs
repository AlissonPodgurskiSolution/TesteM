namespace TesteM.Application.ViewModels
{
    public class ClienteFornecedorViewModel
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public int FornecedorId { get; set; }
        public virtual ClienteViewModel Cliente { get; set; }
        public virtual FornecedorViewModel Fornecedor { get; set; }
    }
}