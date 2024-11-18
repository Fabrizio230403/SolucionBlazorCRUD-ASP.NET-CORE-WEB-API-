using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class RestablecerContraseñaDTO
    {
        public string Token { get; set; } // Token que se pasó por correo
        public string NuevaClave { get; set; } // Nueva contraseña del usuario
    }
}
