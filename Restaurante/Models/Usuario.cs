using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Restaurante.Models
{
    public class Usuario
    {
        [Key]
        [Range(0, int.MaxValue, ErrorMessage = "El valor debe ser mayor o igual que cero.")]
        public int id_usuario { get; set; }

        [Required(ErrorMessage = "El nombre de la persona es obligatorio")]
        [MaxLength(50, ErrorMessage = "El nombre de la persona debe ser menor a 50 caracteres.")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El apellido de la persona es obligatorio")]
        [MaxLength(50, ErrorMessage = "El apellido de la persona debe ser menor a 50 caracteres.")]
        public string apellido { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "El valor debe ser mayor a 1.")]
        public int telefono { get; set; }
        [Required(ErrorMessage = "El nombre de usuario de la persona es obligatorio")]
        [MaxLength(50, ErrorMessage = "El nombre de usuario debe ser menor a 50 caracteres.")]
        public string nombre_u { get; set; }
        [Required(ErrorMessage = "La contraseña es obligatorio")]
        [MaxLength(50, ErrorMessage = "La contraseña debe ser menor a 50 caracteres.")]
        public string contraseña { get; set; }
        public bool eliminado { get; set; }
        //public virtual Historial? Historial { get; set; }
        //[ForeignKey("Rol")]
        public virtual int? id_rol { get; set; }
        public virtual ICollection<Rol>? Rol { get; set; }
        public ICollection<Orden>? Ordenes { get; set; }

    }
}
