using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CompanySite.Domain.Entities
{
    public class ServiceItem:EntityBase
    {
        [Required(ErrorMessage ="Заполните название услуги")]
        public string CodeWord { get; set; }

        [Display(Name = "Название услуги")]
        public override string Title { get; set; }
        
        [Display(Name = "Краткое описание услуги")]
        public override string Subtitle { get; set; }

        [Display(Name = "Cодержание страницы")]
        public override string Text { get; set; } 
    }
}
