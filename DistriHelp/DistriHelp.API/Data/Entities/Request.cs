using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DistriHelp.API.Data.Entities
{
    public class Request
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
        public Category Category { get; set; }

        [Display(Name = "Prioridad")]
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        public RequestType RequesType { get; set; }


        [Display(Name = "Estado")]
        public Status Status { get; set; }

        [Display(Name = "Asignado a:")]
        public User User { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [Display(Name = "Fecha inicio")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime DateI { get; set; }


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
