using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AgroPET.Entidades.Consultas
{
    public class ConsultaUsuarios
    {
        public int? IdUsuario { get; set; }
        public int? IdPerfil { get; set; }
        public string NombrePerfil { get; set; }
        public string NombreUsr { get; set; }
        public string ClaveUsr { get; set; }
        public string PasswordUsr { get; set; }
        public bool? Activo { get; set; }
        public string Estatus { get; set; }
        public int? IdUsuarioCreo { get; set; }
        public DateTime? FechaCreacion { get; set; }
        public string UsuarioCreo { get; set; }
        public int? IdUsuarioModif { get; set; }
        public DateTime? FechaUltModif { get; set; }
        public string UsuarioUltModif { get; set; }
    }
}
