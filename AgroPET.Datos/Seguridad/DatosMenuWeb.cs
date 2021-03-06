﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AgroPET.Entidades.Seguridad;
using AgroPET.Datos.Comun;
using AgroPET.Datos.Catalogos;
using AgroPET.Entidades.Consultas;
using Agropet.Entidades.Consultas;
using AccesoDatos.Comun;
using System.Data;

namespace AgroPET.Datos.Seguridad
{
    public class DatosMenuWeb : DatosBase
    {
        #region Propiedades
        public string Error
        {
            get;
            set;
        }
        #endregion

        #region Metodos
        public List<EntidadMenuWeb> ObtenerMenus()
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "usp_MenuWebObtener";
            accesoDatos.parametros.Agrega("@MenuId", DBNull.Value, true);
            accesoDatos.parametros.Agrega("@Menu", DBNull.Value, true);
            return accesoDatos.ConsultaDataList<EntidadMenuWeb>();
        }

        public List<EntidadMenuWeb> ObtenerMenus(string buscar)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "usp_MenuWebObtener";
            accesoDatos.parametros.Agrega("@MenuId", DBNull.Value, true);
            accesoDatos.parametros.Agrega("@Menu", buscar, true);
            return accesoDatos.ConsultaDataList<EntidadMenuWeb>();
        }

        public List<EntidadMenuWeb> ObtenerMenus(int ibuscar)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "usp_MenuWebObtener";
            accesoDatos.parametros.Agrega("@MenuId", ibuscar, true);
            accesoDatos.parametros.Agrega("@Menu", DBNull.Value, true);
            return accesoDatos.ConsultaDataList<EntidadMenuWeb>();
        }

        public bool InsertarMenu(EntidadMenuWeb menu)
        {
            try
            {
                accesoDatos.parametros.listaParametros.Clear();
                accesoDatos.comandoSP = "uspMenuWeb_Alta";
                accesoDatos.parametros.Agrega("@Menu", menu.Menu, true);
                accesoDatos.parametros.Agrega("@MenuUrl", menu.MenuUrl, true);
                accesoDatos.parametros.Agrega("@Padre", menu.Padre, true);
                accesoDatos.parametros.Agrega("@Activo", menu.Activo, true);
                accesoDatos.parametros.Agrega("@CreacionUsuarioId", menu.CreacionUsuarioId, true);
                int afectados = accesoDatos.EjecutaNQuery();

                if (afectados>=1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Error = " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
                return false;
            }
        }

        public bool ModificarMenu(EntidadMenuWeb menu)
        {
            try
            {
                accesoDatos.parametros.listaParametros.Clear();
                accesoDatos.comandoSP = "uspMenuWeb_Actualiza";
                accesoDatos.parametros.Agrega("@Id", menu.MenuId, true);
                accesoDatos.parametros.Agrega("@Menu", menu.Menu, true);
                accesoDatos.parametros.Agrega("@MenuUrl", menu.MenuUrl, true);
                accesoDatos.parametros.Agrega("@Orden", menu.Orden, true);
                accesoDatos.parametros.Agrega("@Padre", menu.Padre, true);
                accesoDatos.parametros.Agrega("@Activo", menu.Activo, true);
                accesoDatos.parametros.Agrega("@ModificacionUsuarioId", menu.CreacionUsuarioId, true);
                int afectados = accesoDatos.EjecutaNQuery();

                if (afectados >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Error = " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
                return false;
            }
        }

        public void AsignarArticulosMicrosip(List<EntidadMenuArticulos> ent)
        {
            List<Parametros> param = new List<Parametros>();

            accesoDatos.comandoSP = "uspAsignarArticulosMenuWeb_Insertar";
            accesoDatos.parametros.listaParametros.Clear();

            DataTable dtLineas = new DatosGruposLineas().Firebird_ObtenerIdLineaArticuloTodos();
            DataTable dtGposLineas = new DatosGruposLineas().Firebird_ObtenerIdGrupoLineaTodos();

            foreach (var item in ent)
            {
                Parametros p = new Parametros();
                 //int Linea_articulo_id = new DatosGruposLineas().Firebird_ObtenerIdLineaArticulo(item.IdArticulo);
                 //int grupo_linea_id = new DatosGruposLineas().Firebird_ObtenerIdGrupoLinea(Linea_articulo_id);

                var LineaArticuloId = dtLineas.AsEnumerable().Where(f => f.Field<int>("ARTICULO_ID") == item.IdArticulo).Select(s => s.Field<int>("LINEA_ARTICULO_ID")).ToList().FirstOrDefault(); //from Linea_articulo_id in dtLineas.AsEnumerable() where Linea_articulo_id.Field<int>("ARTICULO_ID") == item.IdArticulo select Linea_articulo_id;
                var Grupolineaid = dtGposLineas.AsEnumerable().Where(f=> f.Field<int>("LINEA_ARTICULO_ID") == LineaArticuloId).Select(i => i.Field<int>("GRUPO_LINEA_ID")).ToList().FirstOrDefault();      //from grupo_linea_id in dtGposLineas.AsEnumerable() where Linea_articulo_id .Field<int>("LINEA_ARTICULO_ID") == LineaArticuloId select grupo_linea_id;

                // int  = new DatosGruposLineas().Firebird_ObtenerIdGrupoLinea();

                p.Agrega("@MenId", item.MenuId, true);
                p.Agrega("@idGpo_Lin", Grupolineaid, true);
                p.Agrega("@idLin_Art", LineaArticuloId, true);
                p.Agrega("@idArt", item.IdArticulo, true);

                param.Add(p);
            }

            int afectados = accesoDatos.EjecutaNQuery(param);
        }

        public bool EliminarMenu(int idMenu)
        {
            try
            {
                accesoDatos.parametros.listaParametros.Clear();
                accesoDatos.comandoSP = "uspMenuWeb_Eliminar";
                accesoDatos.parametros.Agrega("@idMenu", idMenu, true);
                int afectados = accesoDatos.EjecutaNQuery();

                if (afectados >= 1)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                Error = " Sourcer ::" + ex.Source + " Error Message :: " + ex.Message;
                return false;
            }
        }
        //uspDesAsignarArticulosMenuWeb_Delete
        public void DesAsignarArticulosMicrosip(EntidadMenuArticulos ent, bool esLibre)
        {
            int Linea_articulo_id = new DatosGruposLineas().Firebird_ObtenerIdLineaArticulo(ent.IdArticulo);
            int grupo_linea_id = new DatosGruposLineas().Firebird_ObtenerIdGrupoLinea(Linea_articulo_id);

            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspDesAsignarArticulosMenuWeb_Delete";
            accesoDatos.parametros.Agrega("@MenId", ent.MenuId, true);
            accesoDatos.parametros.Agrega("@idGpo_Lin", grupo_linea_id, true);
            accesoDatos.parametros.Agrega("@idLin_Art", Linea_articulo_id, true);
            accesoDatos.parametros.Agrega("@idArt", ent.IdArticulo, true);
            accesoDatos.parametros.Agrega("@esLibre", esLibre, true);
            int afectados = accesoDatos.EjecutaNQuery();
        }

        public List<ConsultaRelacionMenuArticulo> ConsultaRelacionMenuArticulos(int idMenu)
        {
            accesoDatos.parametros.listaParametros.Clear();
            accesoDatos.comandoSP = "uspRelacionMenuArticulosObtener";
            accesoDatos.parametros.Agrega("@MenuId", idMenu, true);
            return accesoDatos.ConsultaDataList<ConsultaRelacionMenuArticulo>();
        }

        #endregion
    }
}
