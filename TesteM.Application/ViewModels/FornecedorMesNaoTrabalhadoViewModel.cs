namespace TesteM.Application.ViewModels
{
    public class FornecedorMesNaoTrabalhadoViewModel
    {
        public FornecedorMesNaoTrabalhadoViewModel(int mes, string fornecedor)
        {
            Mes = mes;
            Fornecedor = fornecedor;
        }

        public int Mes { get; set; }
        public string Fornecedor { get; set; }
    }
}