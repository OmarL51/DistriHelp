using System.ComponentModel.DataAnnotations;

namespace DistriHelp.API.Data.Entities
{
    public class RequestType
    {
        public int Id { get; set; }

        [Display(Name = "Tipo de solicitud")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Description { get; set; }
    }
}