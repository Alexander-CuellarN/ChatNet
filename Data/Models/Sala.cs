using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class Sala
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int SalaID { get; set; }
        [Required]
        [MaxLength(250)]
        public string Title { get; set; }
        [MaxLength(500)]
        public string? Descripcion { get; set; }
        public int LimiteUsuarios { get; set; }
        public Collection<Mensaje> mensajes { get; set; }
    }
}
