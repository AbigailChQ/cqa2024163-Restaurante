using System.ComponentModel.DataAnnotations;
namespace Restaurante.Models
{
    public class Rol
    {
        [Key]
        public int id_rol { get; set; }
        [Required(ErrorMessage = "El nombre de rol es obligatorio.")]
        [StringLength(50,ErrorMessage = "El nombre de rol debe ser menor a 50 caracteres.")]
        public string nombre_rol { get; set; }
        public bool eliminado_rol { get; set; }
        public virtual Usuario? Usuario { get; set; }
    }
}
