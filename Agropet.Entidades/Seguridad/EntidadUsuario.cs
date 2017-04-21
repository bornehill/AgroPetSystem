using System;

namespace AgroPET.Entidades.Seguridad
{
 
    public class EntidadUsuario
    {
        public int? idusuario { get; set; }
        public int? idperfil { get; set; }
        public string NombrePerfil { get; set; }
        public string nombreusr { get; set; }
        public string claveusr { get; set; }
        public string passwordusr { get; set; }
        public int? activo { get; set; }
        public int? UsuarioCreo { get; set; }
        public string FechaCreacion { get; set; }
        public Int64? UsuarioModif { get; set; }
        public string FechaUltModif { get; set; }
    }
}
