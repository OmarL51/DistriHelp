using DistriHelp.API.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Models
{
    public class RequestViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Solicitante:")]
        public string Userr { get; set; }

        [Display(Name = "Titulo")]
        [MaxLength(50, ErrorMessage = "El campo {0} no puede tener más de {1} carácteres")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Tittle { get; set; }

        [Display(Name = "Descripción")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public string Description { get; set; }

        [Display(Name = "Categoria")]
        [DataType(DataType.MultilineText)]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; }

        [Display(Name = "Prioridad")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar una prioridad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public int RequestTypeId { get; set; }
        public IEnumerable<SelectListItem> RequestTypes { get; set; }

        [Display(Name = "Estado")]
        [Range(1, int.MaxValue, ErrorMessage = "Debe seleccionar un estado")]
        public int StatusId { get; set; }
        public IEnumerable<SelectListItem> Statuses { get; set; }

        [Display(Name = "Asignado a:")]
        public string UserId { get; set; }
        public IEnumerable<SelectListItem> Users { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateI { get; set; }

        public RequestViewModel()
        {
            DateI = DateTime.Now;
            DateF = DateTime.Now;
        }


        [Display(Name = "Fecha resolución")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateF { get; set; }


        [Display(Name = "Resolución")]
        [DataType(DataType.MultilineText)]
        public string Resolution { get; set; }

        //[Display(Name = "Adjuntos")]
        //public int Attachments { get; set; }
    }
}
