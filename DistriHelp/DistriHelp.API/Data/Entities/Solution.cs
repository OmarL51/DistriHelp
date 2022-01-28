using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Data.Entities
{
    public class Solution
    {
        public int Id { get; set; }

        [Display(Name = "Titulo")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Tittle { get; set; }

        [Display(Name = "Descripción")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Description { get; set; }
    }
}
