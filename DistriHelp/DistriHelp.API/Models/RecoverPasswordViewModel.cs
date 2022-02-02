using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Models
{
    public class RecoverPasswordViewModel
    {
        [EmailAddress(ErrorMessage = "El campo {0} es obligatorio")]
        [Required(ErrorMessage = "Debes introducir un email válido")]
        public string Email { get; set; }
    }
}
