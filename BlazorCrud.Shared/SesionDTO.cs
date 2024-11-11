using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class SesionDTO
    {
        public int UsuarioId { get; set;  }
        public string Nombre { get; set;  }
        public string Correo { get; set;  }
        public string Rol { get; set;  }

    }
}
