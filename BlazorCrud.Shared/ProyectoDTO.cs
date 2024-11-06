using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;


namespace BlazorCrud.Shared
{
    public class ProyectoDTO
    {

        public int ProyectoId { get; set; }

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

        [StringLength(20, ErrorMessage = "La prioridad no puede exceder los {1} caracteres.")]
        public string? Prioridad { get; set; }

        [StringLength(20, ErrorMessage = "El estado no puede exceder los {1} caracteres.")]
        public string? Estado { get; set; }

        [Required(ErrorMessage = "El campo GerenteId es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El GerenteId debe ser mayor que 0.")]
        public int? GerenteId { get; set; }

        [Range(0, 100, ErrorMessage = "El porcentaje completo debe estar entre {1} y {2}.")]
        public decimal? PorcentajeCompleto { get; set; }

        public UsuarioDTO? Gerente { get; set; }
    }
}
 

 

 

 

 

 

 