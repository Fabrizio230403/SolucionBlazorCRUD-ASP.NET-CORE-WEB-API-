using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class LoginResponseDTO
    {
        public string Token { get; set; }
        public UsuarioDTO Usuario { get; set; }
    }
}
