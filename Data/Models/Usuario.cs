using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models
{
    public class Usuario
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int UsuarioID { get; set; }
        [Required]
        [MaxLength(250)]
        public String NickName { get; set; }
        public Collection<Mensaje> Mensajes { get; set; } = null;

    }
}
