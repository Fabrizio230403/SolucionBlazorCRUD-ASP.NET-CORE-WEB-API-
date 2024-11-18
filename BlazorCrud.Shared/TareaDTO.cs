using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class TareaDTO
    {
        public int TareaID { get; set; }

        [Required(ErrorMessage = "El campo nombre es requerido.")]
        [StringLength(100, ErrorMessage = "El nombre no puede exceder los {1} caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo Descripción es requerido.")]
        public string? Descripcion { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? FechaInicio { get; set; }

        [DataType(DataType.Date)]
        public DateOnly? FechaFin { get; set; }

        [StringLength(50, ErrorMessage = "La prioridad no puede exceder los {1} caracteres.")]
        public string? Prioridad { get; set; }

        [StringLength(50, ErrorMessage = "El estado no puede exceder los {1} caracteres.")]
        public string? Estado { get; set; }

        public int ProyectoID { get; set; }

        public int UsuarioAsignadoID { get; set; }

        public UsuarioDTO? Usuario { get; set; }

        public ProyectoDTO? Proyecto { get; set; }

    }
}
