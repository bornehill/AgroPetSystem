using AdminAgropet.Base;
using AgroPET.Entidades;
using AgroPET.Entidades.CatABCs;
using AgropPET.Negocio.Catalogos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminAgropet
{
    public partial class ABCImgsBanners : BaseCatalogo
    {
        private const string cgs_EncabezadoModulo = " Imagenes Banners";
        private static string repositorio = System.Web.Configuration.WebConfigurationManager.AppSettings["rutaImagenesBanners"]; // @"C:\Temporal\Repositorio\ImagenesBanner\";
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
                mvwBanners.SetActiveView(vwConsulta);
               // LLenaDropDown();
                Buscar();
            }
        }

        #region MetodosGenerales

        public void LimpiarDatos()
        {
            txtDescripcionBanner.Value = string.Empty;
            txtRutaBannerDetalle.Value = string.Empty;
            txtOrdenDetalle.Text = string.Empty;
            txtTiltulo.Text = string.Empty;
            txtSubtitulo.Text = string.Empty;
        }

        #endregion MetodosGenerales

        #region VistaConsulta

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            Buscar();
        }

        protected void btnNuevo_Click(object sender, EventArgs e)
        {
            BEdicion = false;
            LimpiarDatos();
            mvwBanners.SetActiveView(vwNuevo);
        }

        protected void gvwConsulta_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int nFila = int.Parse(e.CommandArgument.ToString()) % gvwConsulta.PageSize;
            if (e.CommandName.Equals("Detalles"))
            {
                hfIdBanner.Value = gvwConsulta.DataKeys[nFila].Values[0].ToString();
                BuscarDetalle((int)gvwConsulta.DataKeys[nFila].Values[0]);
                mvwBanners.SetActiveView(vwDetalle);
            }
            if (e.CommandName.Equals("Editar"))
            {
                hfIdBannerEditar.Value = gvwConsulta.DataKeys[nFila].Values[0].ToString();
                txtDescripcionBanner.Value = gvwConsulta.Rows[nFila].Cells[5].Text;
                dtpFechaInicioApp.Value = gvwConsulta.Rows[nFila].Cells[8].Text;
                dtpFechaFinApp.Value = gvwConsulta.Rows[nFila].Cells[9].Text;

                BEdicion = true;

                mvwBanners.SetActiveView(vwNuevo);
            }
        }

        protected void gvwConsulta_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                if(e.Row.Cells[7].Text == DateTime.MinValue.ToString())
                    e.Row.Cells[7].Text = string.Empty;
            }
        }

        protected void gvwConsulta_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwConsulta.PageIndex = e.NewPageIndex;
            Buscar();
        }

        public void Buscar()
        {
            List<EntImgenesBanners> lstBannersConsulta = new CatalogoImagenesBanners().ObtenerBanners(txtBannerBuscar.Value);
            gvwConsulta.DataSource = lstBannersConsulta;
            gvwConsulta.DataBind();
        }

        #endregion VistaConsulta

        #region VistaNuevo

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            Insertar();
        }

        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        public void Insertar()
        {
            if (!string.IsNullOrEmpty(txtDescripcionBanner.Value) && !string.IsNullOrEmpty(dtpFechaInicioApp.Value) && !string.IsNullOrEmpty(dtpFechaFinApp.Value))
            {
                DateTime fehcaInicioApp = Convert.ToDateTime(dtpFechaInicioApp.Value);
                DateTime fechaFinApp = Convert.ToDateTime(dtpFechaFinApp.Value);

                int idUsuario = Convert.ToInt32(UsuarioLogeado.idusuario);

                bool correcto;
                if (BEdicion)
                {
                    //ACTUALIZA
                    int idBanner = Convert.ToInt32(hfIdBannerEditar.Value);
                    correcto = new CatalogoImagenesBanners().ActualizarImagenesBanner(idBanner, idUsuario, txtDescripcionBanner.Value, fehcaInicioApp, fechaFinApp);
                }
                else
                {
                    //INSERTA 
                    correcto = new CatalogoImagenesBanners().InsertaImagenesBanners(idUsuario, fehcaInicioApp, fechaFinApp, txtDescripcionBanner.Value);
                }

                if (!correcto)
                {
                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Ocurrio un error al intentar guardar la información: ", 2);
                }
                else
                {
                    Buscar();
                    mvwBanners.SetActiveView(vwConsulta);
                }
            }
        }

        private void Cancelar()
        {
            Buscar();
            mvwBanners.SetActiveView(vwConsulta);
        }

        #endregion VistaNuevo

        #region VistaDetalle

        protected void btnNuevoDetalle_Click(object sender, EventArgs e)
        {
            BEdicion = false;
            LimpiarDatos();
            mvwBanners.SetActiveView(vwDetalleNuevo);
        }

        protected void btnCancelarDetalle_Click(object sender, EventArgs e)
        {
            Cancelar();
        }

        protected void gvwConsultaDetalle_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int nFila = int.Parse(e.CommandArgument.ToString()) % gvwConsultaDetalle.PageSize;
            if (e.CommandName.Equals("VerImagen"))
            {
                hfIdBannerDetalle.Value = gvwConsultaDetalle.DataKeys[nFila].Values[0].ToString();
                string ruta = gvwConsultaDetalle.Rows[nFila].Cells[6].Text;
                ScriptManager.RegisterStartupScript(this, this.GetType(), "Pop", "openModal('ImagenesBanner/" + ruta + "');", true);
            }
            if (e.CommandName.Equals("Editar"))
            {
                hfIdBannerDetalle.Value = gvwConsultaDetalle.DataKeys[nFila].Values[0].ToString();
                txtRutaBannerDetalle.Value = gvwConsultaDetalle.Rows[nFila].Cells[6].Text;
                txtOrdenDetalle.Text = gvwConsultaDetalle.Rows[nFila].Cells[5].Text;
                txtTiltulo.Text = gvwConsultaDetalle.Rows[nFila].Cells[7].Text;
                txtSubtitulo.Text = gvwConsultaDetalle.Rows[nFila].Cells[8].Text;
                BEdicion = true;

                mvwBanners.SetActiveView(vwDetalleNuevo);
            }
            if (e.CommandName.Equals("Eliminar"))
            {
                int idBannerDetalle = Convert.ToInt32(gvwConsultaDetalle.DataKeys[nFila].Values[0]);
                int idBanner = Convert.ToInt32(gvwConsultaDetalle.DataKeys[nFila].Values[1]);
                bool correcto = BorrarDetalle(idBanner, idBannerDetalle);
                BuscarDetalle(idBanner);

                if (!correcto)
                {
                    MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                    "Ocurrio un error al intentar guardar la información: ", 2);
                }
            }
        }

        protected void gvwConsultaDetalle_RowDataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void gvwConsultaDetalle_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvwConsultaDetalle.PageIndex = e.NewPageIndex;
            BuscarDetalle(Convert.ToInt32(hfIdBanner.Value));
        }

        private void BuscarDetalle(int idBanner)
        {
            List<EntImagenesBannersDetalle> lstBannersConsultaDetalle = new CatalogoImagenesBanners().ObtenerImagenesBannersDetalle(idBanner);
            gvwConsultaDetalle.DataSource = lstBannersConsultaDetalle;
            gvwConsultaDetalle.DataBind();
        }

        private void CancelarDetalle()
        {
            LimpiarDatos();
            BuscarDetalle(Convert.ToInt32(hfIdBanner.Value));
            mvwBanners.SetActiveView(vwDetalle);
        }

        public bool BorrarDetalle(int idBanner, int idDetBanners)
        {
            int idUsuario = Convert.ToInt32(UsuarioLogeado.idusuario);
            return new CatalogoImagenesBanners().EliminaImagenesBannerDetalle(idBanner, idDetBanners, idUsuario);
        }

        #endregion VistaDetalle

        #region VistaNuevoDetalle

        protected void btnGuardarDetalle_Click(object sender, EventArgs e)
        {
            if (inputfile.PostedFile != null && !string.IsNullOrEmpty(txtOrdenDetalle.Text) && !string.IsNullOrEmpty(txtRutaBannerDetalle.Value))
            {
                // File was sent
                if (inputfile.PostedFile.ContentLength > 0)
                {
                    var postedFile = inputfile.PostedFile;
                    //int dataLength = postedFile.ContentLength;
                    //byte[] myData = new byte[dataLength];
                    //postedFile.InputStream.Read(myData, 0, dataLength);
                    postedFile.SaveAs(@"" + repositorio + txtRutaBannerDetalle.Value);
                }

                InsertarDetalle();
            }
            else
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                 "Es Necesario ingresar todos los datos: ", 2);
            }
           
        }

        protected void btnRegresarDetalleNuevo_Click(object sender, EventArgs e)
        {
            CancelarDetalle();
        }

        public void InsertarDetalle()
        {
            int idBanner = Convert.ToInt32(hfIdBanner.Value);
            int idUsuario = Convert.ToInt32(UsuarioLogeado.idusuario);

            bool correcto;
            if (BEdicion)
            {
                //ACTUALIZA
                int idBannerDetalle = Convert.ToInt32(hfIdBannerDetalle.Value);
                correcto = new CatalogoImagenesBanners().ActualizarImagenesBannerDetalle(idBannerDetalle, idBanner, idUsuario, txtRutaBannerDetalle.Value, txtOrdenDetalle.Text, txtTiltulo.Text, txtSubtitulo.Text);
            }
            else
            {
                //INSERTA 
                correcto = new CatalogoImagenesBanners().InsertaImagenesBannerDetalle(idBanner, idUsuario, txtRutaBannerDetalle.Value, txtOrdenDetalle.Text, txtTiltulo.Text, txtSubtitulo.Text);
            }

            if (!correcto)
            {
                MostrarMensaje(Page, string.Concat(cgs_ScriptsMensajes, cgs_EncabezadoModulo.Replace(" ", "_")),
                "Ocurrio un error al intentar guardar la información: ", 2);
            }

            BuscarDetalle(idBanner);
            mvwBanners.SetActiveView(vwDetalle);
        }



        #endregion VistaNuevoDetalle
    }
}