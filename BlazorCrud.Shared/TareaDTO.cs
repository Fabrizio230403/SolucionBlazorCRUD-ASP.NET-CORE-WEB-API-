using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class TareaDTO
    {
        public int TareaId { get; set; }

        [Required(ErrorMessage = "El campo ProyectoId es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El ProyectoId debe ser mayor que 0.")]
        public int? ProyectoId { get; set; }

        [Required(ErrorMessage = "El campo Nombre es requerido.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los {1} caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo Descripción es requerido.")]
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder los {1} caracteres.")]
        public string? Descripcion { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El campo Fecha de Inicio es requerido.")]
        public DateOnly? FechaInicio { get; set; }

        [DataType(DataType.Date)]
        [Required(ErrorMessage = "El campo Fecha de Fin es requerido.")]
        public DateOnly? FechaFin { get; set; }

        [StringLength(20, ErrorMessage = "El estado no puede exceder los {1} caracteres.")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "El campo UsuarioAsignadoId es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El UsuarioAsignadoId debe ser mayor que 0.")]
        public int? UsuarioAsignadoId { get; set; }

        public UsuarioDTO? Usuario { get; set; }

        public ProyectoDTO? Proyecto { get; set; }
    }
}
