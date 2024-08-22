using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Restaurante.Models
{
    public class Orden
    {
        [Key]
        public int Id { get; set; }

        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int ComidaId { get; set; }
        public Comida Comida { get; set; }

        public int Cantidad { get; set; }

        public DateTime FechaOrden { get; set; }
    }
}
