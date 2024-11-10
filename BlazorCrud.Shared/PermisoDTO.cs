using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorCrud.Shared
{
    public class PermisoDTO
    {

        public int PermisoID { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Descripcion { get; set; }

    }
}
