using DistriHelp.API.Data.Entities;
using DistriHelp.Common.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Models
{
    public class UserViewModel
    {
        public string Id { get; set; }



        [Display(Name = "Nombres")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string FirstName { get; set; }

        [Display(Name = "Apellidos")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string LastName { get; set; }

        [Display(Name = "Tipo de usuario")]
        public UserType UserType { get; set; }

        [Display(Name = "Usuario")]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Área")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un área")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int AreaId { get; set; }
        public IEnumerable<SelectListItem> Areas { get; set; }

        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Ingrese un email valido")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Email { get; set; }





    }
}
