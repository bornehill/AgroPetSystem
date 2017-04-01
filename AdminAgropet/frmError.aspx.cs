using Agropet.Entidades.Seguridad;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AdminAgropet
{
    public partial class frmError : System.Web.UI.Page
    {
        private EntidadUsuarioLogeado usuarioLogeado;
        public EntidadUsuarioLogeado UsuarioLogeado
        {
            get { return usuarioLogeado = (EntidadUsuarioLogeado)Session["UsuarioSesion"]; }
            set { usuarioLogeado = value; }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MuestraError();
            }
        }

        private void MuestraError()
        {
            //Obtenemos el error
            Exception ex = Server.GetLastError();

            lbltextoError.Text = ex.Message;

            //Limpiamos el error 
            Server.ClearError();
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/frmMenuModuloAdmin.aspx", false);
        }

        //private void EnviaCorreoError()
        //{
        //    try
        //    {
        //        //Obtenemos el error
        //        Exception ex = Server.GetLastError();

        //        //Formateamos el correo con los datos del error
        //        string bodyMail = "Error en Aplicación<br/>";
        //        bodyMail += "<br/>Fecha: " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString();
        //        bodyMail += "<br/>Descripción: " + ex.Message;
        //        bodyMail += "<br/>Origen: " + ex.Source;
        //        bodyMail += "<br/>Pila: " + ex.StackTrace;

        //        System.Net.Mail.MailMessage oMsg = new System.Net.Mail.MailMessage();
        //        oMsg.From = new System.Net.Mail.MailAddress("error@mydomain.com");
        //        oMsg.To.Add("webmaster@mydomain.com");
        //        oMsg.Subject = "Error en Aplicación";
        //        oMsg.Body = bodyMail;
        //        oMsg.IsBodyHtml = true;

        //        System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient("mail.mydomain.com");
        //        smtp.Credentials = new System.Net.NetworkCredential("error@mydomain.com", "passCuentaCorreo");

        //        smtp.Send(oMsg);

        //        //Limpiamos el error 
        //        Server.ClearError();
        //    }
        //    catch (Exception errorEnEnvioDeError)
        //    {
        //        //En caso de fallar el envío de error podíamos probar un método alternativo como guardar "bodyMail" en un fichero de texto o en base de datos
        //    }
        //}
    }
}