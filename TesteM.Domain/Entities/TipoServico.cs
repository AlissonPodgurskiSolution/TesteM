using System.ComponentModel.DataAnnotations;

namespace TesteM.Domain.Entities
{
    public class TipoServico
    {
        [Key]
        public int Id { get; set; }
        public string Tipo { get; set; }
    }
}