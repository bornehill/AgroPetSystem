using AdminAgropet.Base;
using AgroPET.Entidades.CatABCs;
using AgroPET.Entidades.Consultas;
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
        private static string repositorio = System.Web.Configuration.WebConfigurationManager.AppSettings["rutaImagenesArticulos"];// @"C:\Temporal\Repositorio\ImagenesArticulos\";

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
            ddlArticulos.Enabled = true;

            BEdicion = false;
            LimpiarDatos();
            ddlArticulos.SelectedIndex = 0;
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        public void Buscar()
        {
            List<EntImgenesArticulos> lstArticulosConsulta = new CatalogoImagenesArticulos().ObtenerImagenesArticulos(txtArticuloBuscar.Value);
            gvwConsulta.DataSource = lstArticulosConsulta;
            gvwConsulta.DataBind();
        }

        public void LimpiarDatos()
        {
            txtImagenArticulo.Value = string.Empty;
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
            int nFila = int.Parse(e.CommandArgument.ToString()) % gvwConsulta.PageSize;
            if (e.CommandName.Equals("Editar"))
            {
                hfIdImagenArticulo.Value = gvwConsulta.DataKeys[nFila].Values[0].ToString();
                txtImagenArticulo.Value = gvwConsulta.Rows[nFila].Cells[9].Text.Replace("&nbsp;","");
                ddlArticulos.SelectedValue = gvwConsulta.Rows[nFila].Cells[3].Text;
                ddlArticulos.Enabled = false;
                ddlArticulos.ControlStyle.CssClass = "form-control";
                BEdicion = true;

                mvwArticulos.SetActiveView(vwNuevo);
            }
            if (e.CommandName.Equals("Eliminar"))
            {
                int IdImagenArticulo = Convert.ToInt32(gvwConsulta.DataKeys[nFila].Values[0]);
                bool correcto = Borrar(IdImagenArticulo);
                Buscar();

                if (!correcto)
                {
                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Ocurrio un error al intentar guardar la información: ", 2);
                }
            }
        }

        public bool Borrar(int IdImagenArticulo)
        {
            int idUsuario = Convert.ToInt32(UsuarioLogeado.idusuario);
            return new CatalogoImagenesArticulos().EliminaImagenesArticulos(IdImagenArticulo);
        }

        protected void gvwConsulta_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

       
        protected void gvwConsultaDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        //protected void gvwConsultaDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int nFila = int.Parse(e.CommandArgument.ToString()) % gvwConsultaDetalle.PageSize;
        //    if (e.CommandName.Equals("Editar"))
        //    {
        //        hfIdImagenArticuloDetalle.Value = gvwConsultaDetalle.DataKeys[nFila].Values[0].ToString();
        //        txtImagenArticulo.Value = gvwConsultaDetalle.Rows[nFila].Cells[4].Text;
        //        BEdicion = true;

        //        mvwArticulos.SetActiveView(vwDetalleNuevo);
        //    }
        //    if (e.CommandName.Equals("Eliminar"))
        //    {
        //        int IdDetImagenArticulo = Convert.ToInt32(gvwConsultaDetalle.DataKeys[nFila].Values[0]);
        //        int IdImagenArticulo = Convert.ToInt32(gvwConsultaDetalle.DataKeys[nFila].Values[1]);
        //        bool correcto = BorrarDetalle(IdDetImagenArticulo, IdImagenArticulo);
        //        BuscarDetalle(IdImagenArticulo);

        //        if (!correcto)
        //        {
        //            MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
        //            "Ocurrio un error al intentar guardar la información: ", 2);
        //        }
        //    }
        //}

        //protected void btnGuardarDetalle_Click(object sender, EventArgs e)
        //{
        //    if (inputfile.PostedFile != null && !string.IsNullOrEmpty(txtImagenArticulo.Value))
        //    {
        //        // File was sent
        //        var postedFile = inputfile.PostedFile;
        //        //int dataLength = postedFile.ContentLength;
        //        //byte[] myData = new byte[dataLength];
        //        //postedFile.InputStream.Read(myData, 0, dataLength);
        //        postedFile.SaveAs(@"" + repositorio + txtImagenArticulo.Value);

        //        InsertarDetalle();
        //    }
        //    else
        //    {
        //        MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
        //         "Es Necesario ingresar todos los datos: ", 2);
        //    }
        //}


        //protected void btnNuevoDetalle_ServerClick(object sender, EventArgs e)
        //{
        //    BEdicion = false;
        //    LimpiarDatos();
        //    mvwArticulos.SetActiveView(vwDetalleNuevo);
        //}

        //protected void btnCancelarDetalle_ServerClick(object sender, EventArgs e)
        //{
        //    Cancelar();
        //}

        //protected void btnRegresarDetalleNuevo_Click(object sender, EventArgs e)
        //{
        //    CancelarDetalle();
        //}
     

        #region Metdos

        private void LLenaDropDown()
        {
            List<ConsultaArticulos> lstArticulos = new CatalogoArticulos().ObtenerArticulosDropDown();
            LlenarDropDown(ddlArticulos, lstArticulos, "Nombre", "ID_ART", PrimerElemento.ValorPropio, "Selecciona Articulo a Insertar...");
        }

      

        //public void BuscarDetalle(int idImagenArticulo)
        //{
        //    List<EntImagenesArticulosDetalle> lstArticulosConsultaDetalle = new CatalogoImagenesArticulos().ObtenerImagenesArticulosDetalle(idImagenArticulo);
        //    gvwConsultaDetalle.DataSource = lstArticulosConsultaDetalle;
        //    gvwConsultaDetalle.DataBind();
        //}

        public void Insertar()
        {
            if (ddlArticulos.SelectedIndex != 0 && !string.IsNullOrEmpty(txtImagenArticulo.Value))
            {
                int idArticulo = Convert.ToInt32(ddlArticulos.SelectedItem.Value);
                int idUsuario = Convert.ToInt32(UsuarioLogeado.idusuario);

                string alertaError = string.Empty;
                bool correcto;
                if (BEdicion)
                {
                    //ACTUALIZA
                    int idImagenArticulo = Convert.ToInt32(hfIdImagenArticulo.Value);
                    correcto = new CatalogoImagenesArticulos().ActualizarImagenesArticulos(idImagenArticulo, idUsuario, txtImagenArticulo.Value);
                    alertaError = "No es posible actualizar el Articulo";
                }
                else
                {
                    //INSERTA 
                    correcto = new CatalogoImagenesArticulos().InsertaImagenesArticulos(idArticulo, idUsuario, txtImagenArticulo.Value);
                    alertaError = "No es posible insertar el articulo es posible que ya exista";
                }


                if (!correcto)
                {
                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    alertaError, 2);
                }
                else
                {
                    Buscar();
                    mvwArticulos.SetActiveView(vwConsulta);
                }
            }
            else
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Es necesario ingresar todos los datos", 2);
            }

        }

        private void Cancelar()
        {
            //Buscar();
            mvwArticulos.SetActiveView(vwConsulta);
        }

        //private void CancelarDetalle()
        //{
        //    LimpiarDatos();
        //    BuscarDetalle(Convert.ToInt32(hfIdImagenArticulo.Value));
        //    mvwArticulos.SetActiveView(vwDetalle);
        //}

      

        //public void InsertarDetalle()
        //{
        //    if (!string.IsNullOrEmpty(txtImagenArticulo.Value))
        //    {
        //        int idImagenArticulo = Convert.ToInt32(hfIdImagenArticulo.Value);
        //        int idUsuario = Convert.ToInt32(UsuarioLogeado.idusuario);

        //        bool correcto;
        //        if (BEdicion)
        //        {
        //            //ACTUALIZA
        //            int idImagenArticuloDetalle = Convert.ToInt32(hfIdImagenArticuloDetalle.Value);
        //            correcto = new CatalogoImagenesArticulos().ActualizarImagenesArticulosDetalle(idImagenArticulo, idUsuario, txtImagenArticulo.Value, idImagenArticuloDetalle);
        //        }
        //        else
        //        {
        //            //INSERTA 
        //            correcto = new CatalogoImagenesArticulos().InsertaImagenesArticulosDetalle(idImagenArticulo, idUsuario, txtImagenArticulo.Value);
        //        }


        //        if (!correcto)
        //        {
        //            MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
        //            "Ocurrio un error al intentar guardar la información: ", 2);
        //        }

        //        BuscarDetalle(idImagenArticulo);
        //        mvwArticulos.SetActiveView(vwDetalle);
        //    }
        //}

        //public bool BorrarDetalle(int IdDetImagenArticulo, int IdImagenArticulo)
        //{
        //    int idUsuario = Convert.ToInt32(UsuarioLogeado.idusuario);
        //    return new CatalogoImagenesArticulos().EliminaImagenesArticulosDetalle(IdDetImagenArticulo, IdImagenArticulo, idUsuario);
        //}


        #endregion Metdos

        protected void gvwConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwConsulta.PageIndex = e.NewPageIndex;
            Buscar();
        }

        //protected void gvwConsultaDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    gvwConsultaDetalle.PageIndex = e.NewPageIndex;
        //    BuscarDetalle(Convert.ToInt32(hfIdImagenArticulo.Value));
        //}
    }
}