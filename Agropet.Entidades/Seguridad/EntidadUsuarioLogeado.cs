using System;

namespace AgroPET.Entidades.Seguridad
{
    public class EntidadUsuarioLogeado
    {
        public Int64 idusuario { get; set; }
        public Int64 idperfil { get; set; }
        public string nombre { get; set; }
        public string claveusuario { get; set; }
        public string passwdusr { get; set; }
        public bool activo { get; set; }
        public string nombreperfil { get; set; }
    }
}
