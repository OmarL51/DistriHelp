using DistriHelp.API.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Models
{
    public class UserViewModel : User
    {
      
        [Display(Name = "Área")]
        [Range(1, int.MaxValue, ErrorMessage ="Debe seleccionar un área")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int AreaId { get; set; }
        public IEnumerable<SelectListItem> Areas { get; set; }

      
    }
}
