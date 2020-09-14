
using System.ComponentModel.DataAnnotations;

namespace NetFramework.Models
{
    public class Cep
    {
        [Required(ErrorMessage = "Digite um CEP válido")]
        [MaxLength(9, ErrorMessage = "CEP deve estar no formato 00000-000")]
        [MinLength(9, ErrorMessage = "CEP deve estar no formato 00000-000")]
        [DisplayFormat(DataFormatString = "00000-000")]
        public string Codigo { get; set; }
    }
}