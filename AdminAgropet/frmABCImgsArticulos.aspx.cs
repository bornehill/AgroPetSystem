using AdminAgropet.Base;
using Agropet.Entidades.CatABCs;
using Agropet.Entidades.Consultas;
using AgropPET.Negocio.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminAgropet
{
    public partial class ABCImgsArticulos : BaseCatalogo
    {
        //private const string cgs_ScriptsMensajes = "Mensaje_Seguridad_";
        private const string cgs_EncabezadoModulo = " Imagenes Modulo";
        private const string repositorio = @"C:\Temporal\Repositorio\ImagenesArticulos\";

        private static bool _bEdicion;
        public static bool BEdicion
        {
            get { return _bEdicion; }
            set { _bEdicion = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                mvwArticulos.SetActiveView(vwConsulta);
                LLenaDropDown();
                Buscar();
            }
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            mvwArticulos.SetActiveView(vwNuevo);
            BEdicion = false;
            LimpiarDatos();
            ddlArticulos.SelectedIndex = 0;
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Insertar();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        protected void gvwConsulta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int nFila = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("Detalles"))
            {
                hfIdImagenArticulo.Value = gvwConsulta.DataKeys[nFila].Values[0].ToString();
                BuscarDetalle((int)gvwConsulta.DataKeys[nFila].Values[0]);
                mvwArticulos.SetActiveView(vwDetalle);
            }
        }

        protected void gvwConsulta_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        protected void gvwConsultaDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvwConsultaDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int nFila = int.Parse(e.CommandArgument.ToString());
            if (e.CommandName.Equals("Editar"))
            {
                hfIdImagenArticuloDetalle.Value = gvwConsultaDetalle.DataKeys[nFila].Values[0].ToString();
                txtImagenArticulo.Text = gvwConsultaDetalle.Rows[nFila].Cells[4].Text;
                BEdicion = true;

                mvwArticulos.SetActiveView(vwDetalleNuevo);
            }
            if (e.CommandName.Equals("Eliminar"))
            {
                int IdDetImagenArticulo = Convert.ToInt32(gvwConsultaDetalle.DataKeys[nFila].Values[0]);
                int IdImagenArticulo = Convert.ToInt32(gvwConsultaDetalle.DataKeys[nFila].Values[1]);
                bool correcto = BorrarDetalle(IdDetImagenArticulo, IdImagenArticulo);
                BuscarDetalle(IdImagenArticulo);

                if (!correcto)
                {
                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Ocurrio un error al intentar guardar la información: ", 2);
                }
            }
        }

        protected void btnGuardarDetalle_Click(object sender, EventArgs e)
        {
            InsertarDetalle();
        }


        protected void btnNuevoDetalle_ServerClick(object sender, EventArgs e)
        {
            BEdicion = false;
            LimpiarDatos();
            mvwArticulos.SetActiveView(vwDetalleNuevo);
        }

        protected void btnCancelarDetalle_ServerClick(object sender, EventArgs e)
        {
            Cancelar();
        }

        protected void btnRegresarDetalleNuevo_Click(object sender, EventArgs e)
        {
            CancelarDetalle();
        }
     

        #region Metdos

        private void LLenaDropDown()
        {
            List<ConsultaArticulos> lstArticulos = new CatalogoArticulos().ObtenerArticulosDropDown();
            LlenarDropDown(ddlArticulos, lstArticulos, "Nombre", "ID_ART", PrimerElemento.ValorPropio, "Selecciona Articulo a Insertar...");
        }

        public void Buscar()
        {
            List<EntImgenesArticulos> lstArticulosConsulta = new CatalogoImagenesArticulos().ObtenerImagenesArticulos(txtArticuloBuscar.Value);
            gvwConsulta.DataSource = lstArticulosConsulta;
            gvwConsulta.DataBind();
        }

        public void BuscarDetalle(int idImagenArticulo)
        {
            List<EntImagenesArticulosDetalle> lstArticulosConsultaDetalle = new CatalogoImagenesArticulos().ObtenerImagenesArticulosDetalle(idImagenArticulo);
            gvwConsultaDetalle.DataSource = lstArticulosConsultaDetalle;
            gvwConsultaDetalle.DataBind();
        }

        public void Insertar()
        {
            if (ddlArticulos.SelectedIndex != 0)
            {
                int idArticulo = Convert.ToInt32(ddlArticulos.SelectedItem.Value);
                int idUsuario = Convert.ToInt32(UsuarioLogeado.idusuario);

                bool correcto = new CatalogoImagenesArticulos().InsertaImagenesArticulos(idArticulo, idUsuario);

                if (!correcto)
                {
                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Ocurrio un error al intentar guardar la información: ", 2);
                }
                else
                {
                    Buscar();
                    mvwArticulos.SetActiveView(vwConsulta);
                }
            }
        }

        private void Cancelar()
        {
            Buscar();
            mvwArticulos.SetActiveView(vwConsulta);
        }

        private void CancelarDetalle()
        {
            LimpiarDatos();
            BuscarDetalle(Convert.ToInt32(hfIdImagenArticulo.Value));
            mvwArticulos.SetActiveView(vwDetalle);
        }

        public void LimpiarDatos()
        {
            txtImagenArticulo.Text = string.Empty;
        }

        public void InsertarDetalle()
        {
            int idImagenArticulo = Convert.ToInt32(hfIdImagenArticulo.Value);
            int idUsuario = Convert.ToInt32(UsuarioLogeado.idusuario);

            bool correcto;
            if (BEdicion)
            {
                //ACTUALIZA
                int idImagenArticuloDetalle = Convert.ToInt32(hfIdImagenArticuloDetalle.Value);
                correcto = new CatalogoImagenesArticulos().ActualizarImagenesArticulosDetalle(idImagenArticulo, idUsuario, txtImagenArticulo.Text, idImagenArticuloDetalle);
            }
            else
            {
                //INSERTA 
                correcto = new CatalogoImagenesArticulos().InsertaImagenesArticulosDetalle(idImagenArticulo, idUsuario, txtImagenArticulo.Text);
            }


            if (!correcto)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                "Ocurrio un error al intentar guardar la información: ", 2);
            }
          
            BuscarDetalle(idImagenArticulo);
            mvwArticulos.SetActiveView(vwDetalle);

        }

        public bool BorrarDetalle(int IdDetImagenArticulo, int IdImagenArticulo)
        {
            int idUsuario = Convert.ToInt32(UsuarioLogeado.idusuario);
            return new CatalogoImagenesArticulos().EliminaImagenesArticulosDetalle(IdDetImagenArticulo, IdImagenArticulo, idUsuario);
        }

        #endregion Metdos


    }
}