using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class RecursoDTO
    {
        public int RecursoId { get; set; }

        [Required(ErrorMessage = "El campo ProyectoId es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ProyectoId debe ser mayor que 0.")]
        public int? ProyectoId { get; set; }

        [StringLength(20, ErrorMessage = "La prioridad no puede exceder los {1} caracteres.")]
        public string? Tipo { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los {1} caracteres.")]
        public string Nombre { get; set; } = null!;

        [Range(0, double.MaxValue, ErrorMessage = "El Costo debe ser mayor o igual a 0.")]
        public decimal? Costo { get; set; }
        
        [Range(0, 10000, ErrorMessage = "El TiempoAsignado debe ser mayor o igual a 0 y no exceder un valor máximo de 10000 horas.")]
        public decimal? TiempoAsignado { get; set; }

        [Required(ErrorMessage = "El campo UsuarioId es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El UsuarioId debe ser mayor que 0.")]
        public int? UsuarioId { get; set; }

        public UsuarioDTO? Usuario { get; set; }

        public ProyectoDTO? Proyecto { get; set; }
    }
}
