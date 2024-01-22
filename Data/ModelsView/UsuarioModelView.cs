using System.ComponentModel.DataAnnotations;

namespace Data.ModelsView
{
    public class UsuarioModelView
    {
        public int UsurioId { get; set; }
        [Required(ErrorMessage = "Se requiere un nick para comenzar")]
        [Display(Name = "Nick namme")]
        public string nickName { get; set; }
    }
}
