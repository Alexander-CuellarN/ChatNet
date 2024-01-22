using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Mensaje
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int MensajeId { get; set; }

        [Required]
        [MaxLength(500)]
        public string Contenido { get; set; }

        [ForeignKey("SalaId")]
        public int SalaId { get; set; }
        [ForeignKey("UsuarioId")]
        public int UsuarioId { get; set; }

        public Sala salaNavigation { get; set; }
        public Usuario UsuarioNavigation { get; set; }
    }
}
