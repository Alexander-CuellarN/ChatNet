using Data.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.ModelsView
{
    public class SalaModelView
    {
        public int? SalaID { get; set; }

        [Required(ErrorMessage = "El nombre de sala es obligatorio")]
        [MaxLength(250, ErrorMessage = "El nombre no puede superar los 250 caracteres")]
        [Display(Name = "Nombre de la Sala")]
        public string Title { get; set; }

        [MaxLength(500, ErrorMessage = "La Descripción no puede superar los 500 caracteres")]
        [Required(ErrorMessage = "la Descripción de la sala es obligatorio")]
        [Display(Name = "Descripción")]
        public string? Descripcion { get; set; }
        public int? LimiteUsuarios { get; set; }
    }
}
