using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace BlazorCrud.Shared
{
    public class UsuarioDTO
    {
         
        public int UsuarioId { get; set; }

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(100, ErrorMessage = "El campo {0} debe tener un máximo de {1} caracteres.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [EmailAddress(ErrorMessage = "El campo {0} debe ser una dirección de correo electrónico válida.")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [StringLength(200, ErrorMessage = "El campo {0} debe tener un máximo de {1} caracteres.")]
        public string PasswordHash { get; set; } = null!;

        [Required(ErrorMessage = "El campo {0} es requerido.")]
        [Range(1, int.MaxValue, ErrorMessage = "El campo {0} debe ser mayor que cero.")]
        public int? RolId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FechaCreacion { get; set; }

        public RolDTO? Rol { get; set; }
    }
}
