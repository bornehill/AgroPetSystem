using System;

namespace AgroPET.Entidades.Seguridad
{
    public class EntidadMenuWeb
    {
        public int MenuId
        {
            get;
            set;
        }

        public string Menu
        {
            get;
            set;
        }

        public string MenuUrl
        {
            get;
            set;
        }

        public int Padre
        {
            get;
            set;
        }

        public int Nivel
        {
            get;
            set;
        }

        public int Orden
        {
            get;
            set;
        }

        public bool Activo
        {
            get;
            set;
        }

        public long CreacionUsuarioId
        {
            get;
            set;
        }

        public DateTime? FechaCreacion
        {
            get;
            set;
        }

        public long ModificacionUsuarioId
        {
            get;
            set;
        }

        public DateTime? FechaModificacion
        {
            get;
            set;
        }

        public string PadreNom
        {
            get;
            set;
        }
    }
}
