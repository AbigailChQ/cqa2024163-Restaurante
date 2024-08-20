using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Restaurante.Models
{
    public class Comida
    {
        [Key]
        public int Id { get; set; }  // Identificador único de la comida

        public string Nombre { get; set; }  // Nombre de la comida

        public string Descripcion { get; set; }  // Descripción de la comida

        public decimal Precio { get; set; }  // Precio de la comidas
        public ICollection<Orden>? Ordenes { get; set; }
    }
}
