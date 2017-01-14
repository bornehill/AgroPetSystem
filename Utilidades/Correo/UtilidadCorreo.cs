using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Mail;


namespace Utilidades.Correo
{
    using Errores;

    /// <summary>
    /// Clase que envía correos de notificación
    /// </summary>
    public static class UtilidadCorreo
    {
        #region Campos

        private const string DireccionMail = "SICER@nissan.com.mx";
        private const string NombreRemitente = "Administrador SICER";
        private const string ServidorSmtp = "relaysmtp.ndc.nna";
        //private const string ServidorSmtp = "10.92.8.25";
        //private const string ServidorSmtp = "192.168.0.1";
        private const int ServidorSmtpPuerto = 25;

        #endregion
        #region Métodos
        /// <summary>
        /// Envía un correo electrónico de notificación cuando un documento cambia de estatus
        /// </summary>
        /// <param name="folio">Folio del documento</param>
        /// <param name="tipoDocumento">Tipo de documento</param>
        /// <param name="estado">Estado del documento</param>
        /// <param name="usuarioAutorizador">Nombre del usuario autorizador</param>
        /// <param name="comentarios">Comentarios de la autorización</param>
        /// <param name="correoDestinatarios">Lista de destinatarios</param>
        /// 
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void EnviarCorreoGeneral(string VtextoMensaje,List<string>Parametros,List<String>Destinatarios,string Subject)
        {

            Object[] ParametroObjects = new string[Parametros.Count];
            int position = 0;
            foreach (string s in Parametros)
            {                
                ParametroObjects[position] = s;
                position++;
            }
            StringBuilder sbMensaje = new StringBuilder();
            sbMensaje.AppendFormat(string.Format(VtextoMensaje, ParametroObjects));
            UtilidadCorreo.EnviarCorreo(Destinatarios, Subject, sbMensaje.ToString());
            sbMensaje = null;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void EnviarCorreoDocumentoCambioEstado(string folio, string tipoDocumento, string estado, string usuarioAutorizador, string comentarios, List<string> correoDestinatarios)
        {
            StringBuilder sbMensaje = new StringBuilder();

            sbMensaje.AppendFormat("Fecha: '{0}'", DateTime.Now.ToString("yyyy/MM/dd"));
            sbMensaje.AppendLine("<br />");
            sbMensaje.AppendFormat("El documento con el folio: '{0} - {1}' ha cambiado de estado.", folio, tipoDocumento);
            sbMensaje.AppendLine("<br />");
            sbMensaje.AppendFormat("Autorizador: {0}.", usuarioAutorizador);
            sbMensaje.AppendLine("<br /><br />");
            sbMensaje.AppendFormat("Detalles:</br> <b>-{0}-</b>.", comentarios);
            string sAsunto = string.Format("SC - {0} {1} - {2}", folio, tipoDocumento, estado);

            UtilidadCorreo.EnviarCorreo(correoDestinatarios, sAsunto, sbMensaje.ToString());

            sbMensaje = null;
        }

        /// <summary>
        /// Envía un correo electrónico de notifiación cuando un documento es dado de alta (estado inicial)
        /// </summary>
        /// <param name="folio">Folio del documento</param>
        /// <param name="tipoDocumento">Tipo de documento</param>
        /// <param name="estado">Estado del documento</param>
        /// <param name="correoDestinatarios">Lista de destinatarios</param>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void EnviarCorreoDocumentoInicial(string folio, string tipoDocumento, string estado, List<string> correoDestinatarios)
        {
            StringBuilder sbMensaje = new StringBuilder();

            sbMensaje.AppendFormat("Fecha: '{0}'", DateTime.Now.ToString("yyyy/MM/dd"));
            sbMensaje.AppendLine("<br />");
            sbMensaje.AppendFormat("El documento con el folio: '{0} - {1}' esta pendiente por ser revisada por usted.", folio, tipoDocumento);

            string sAsunto = string.Format("PEA - {0} {1} - {2}", folio, tipoDocumento, estado);

            UtilidadCorreo.EnviarCorreo(correoDestinatarios, sAsunto, sbMensaje.ToString());

            sbMensaje = null;
        }

        /// <summary>
        /// Envía un correo electrónico de notificación
        /// </summary>
        /// <param name="correoDestinatarios">Lista de destinatarios</param>
        /// <param name="asunto">Asunto del correo</param>
        /// <param name="mensaje">Mensaje del correo</param>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void EnviarCorreo(List<string> correoDestinatarios, string asunto, string mensaje)
        {
            StringBuilder sbHtml = new StringBuilder();

            //sbHtml.AppendLine("<html><head><style>BODY {font-size: 8pt; color: black; font-family: Tahoma;BACKGROUND-COLOR: white;}");
            //sbHtml.AppendLine(".NORMAL {font-weight: bold; font-size: 10pt; color: white; font-family: Tahoma; background-color: #6699cc; text-align: center;}");
            //sbHtml.AppendLine(".ALTA {font-weight: bold; font-size: 10pt; color: white; font-family: Tahoma; background-color: #ca0000; text-align: center;}");
            //sbHtml.AppendLine("table {font-size: 10pt; font-family: Tahoma}</style></head>");
            //sbHtml.AppendLine("<html><body><table cellSpacing=\"0\" cellPadding=\"0\" width=\"70%\" align=\"center\" border=\"0\" style=\"border: 1px solid #000000;\"><tr><td><table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
            //sbHtml.AppendFormat("{0} {1}", "<tbody><tr><td vAlign=\"top\" align=\"right\"><b><font face=\"Tahoma\" size=\"2\">", DateTime.Now.DayOfWeek.ToString(), DateTime.Now.Date.ToShortDateString(), "</b></font></td></tr>\\n");
            //sbHtml.AppendLine("</td></tr></table></td></tr></tbody></table><br />");
            //sbHtml.AppendLine("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"70%\" align=\"center\" border=\"0\" style=\"border: 1px solid #000000\">");
            //sbHtml.AppendFormat("<tr><td><table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\"><tr class=\"NORMAL\"><td><br />{0}<br />&nbsp;</td></tr></table>", mensaje);
            sbHtml.AppendLine(mensaje);
            using (MailMessage mailMensaje = new MailMessage())
            {
                foreach (string sCorreo in correoDestinatarios)
                {
                    mailMensaje.To.Add(new MailAddress(sCorreo));
                }
                mailMensaje.From = new MailAddress(DireccionMail, NombreRemitente);
                mailMensaje.Subject = asunto;
                mailMensaje.IsBodyHtml = true;
                mailMensaje.Body = sbHtml.ToString();
                SmtpClient smtpCliente = new SmtpClient(ServidorSmtp, ServidorSmtpPuerto);
                try
                {
                    smtpCliente.Send(mailMensaje);
                }
                catch (Exception ex)
                {
                    ControlErrores.Generar_Error(string.Format("Error al intentar enviar correos: {0}", ex.Message));
                }
            }

            sbHtml = null;
        }

        /// <summary>
        /// Envía un correo electrónico de notificación y regresa true o false segun termino el proceso
        /// </summary>
        /// <param name="correoDestinatarios">Lista de destinatarios</param>
        /// <param name="asunto">Asunto del correo</param>
        /// <param name="mensaje">Mensaje del correo</param>
        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public static void EnviarCorreo(List<string> correoDestinatarios, string asunto, string mensaje, ref bool bCorreoEnviado)
        {
            bCorreoEnviado = true;
            StringBuilder sbHtml = new StringBuilder();

            //sbHtml.AppendLine("<html><head><style>BODY {font-size: 8pt; color: black; font-family: Tahoma;BACKGROUND-COLOR: white;}");
            //sbHtml.AppendLine(".NORMAL {font-weight: bold; font-size: 10pt; color: white; font-family: Tahoma; background-color: #6699cc; text-align: center;}");
            //sbHtml.AppendLine(".ALTA {font-weight: bold; font-size: 10pt; color: white; font-family: Tahoma; background-color: #ca0000; text-align: center;}");
            //sbHtml.AppendLine("table {font-size: 10pt; font-family: Tahoma}</style></head>");
            //sbHtml.AppendLine("<html><body><table cellSpacing=\"0\" cellPadding=\"0\" width=\"70%\" align=\"center\" border=\"0\" style=\"border: 1px solid #000000;\"><tr><td><table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
            //sbHtml.AppendFormat("{0} {1}", "<tbody><tr><td vAlign=\"top\" align=\"right\"><b><font face=\"Tahoma\" size=\"2\">", DateTime.Now.DayOfWeek.ToString(), DateTime.Now.Date.ToShortDateString(), "</b></font></td></tr>\\n");
            //sbHtml.AppendLine("</td></tr></table></td></tr></tbody></table><br />");
            //sbHtml.AppendLine("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"70%\" align=\"center\" border=\"0\" style=\"border: 1px solid #000000\">");
            //sbHtml.AppendFormat("<tr><td><table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\"><tr class=\"NORMAL\"><td><br />{0}<br />&nbsp;</td></tr></table>", mensaje);
            sbHtml.AppendLine(mensaje);
            using (MailMessage mailMensaje = new MailMessage())
            {
                foreach (string sCorreo in correoDestinatarios)
                {
                    mailMensaje.To.Add(new MailAddress(sCorreo));
                }
                mailMensaje.From = new MailAddress(DireccionMail, NombreRemitente);
                mailMensaje.Subject = asunto;
                mailMensaje.IsBodyHtml = true;
                mailMensaje.Body = sbHtml.ToString();
                SmtpClient smtpCliente = new SmtpClient(ServidorSmtp, ServidorSmtpPuerto);
                try
                {
                    smtpCliente.Send(mailMensaje);
                }
                catch (Exception ex)
                {
                    bCorreoEnviado = false;
                    ControlErrores.Generar_Error(string.Format("Error al intentar enviar correos: {0}", ex.Message));
                }
            }

            sbHtml = null;
        }

        /// <summary>
        /// Envía un correo electrónico de notificación y regresa true o false segun termino el proceso
        /// </summary>
        /// <param name="correoDestinatarios">Lista de destinatarios</param>
        /// <param name="asunto">Asunto del correo</param>
        /// <param name="mensaje">Mensaje del correo</param>        
        public static void EnviarCorreo(List<string> correoDestinatarios, string asunto, string mensaje, ref bool bCorreoEnviado, Attachment[] adjuntos)
        {
            bCorreoEnviado = true;
            StringBuilder sbHtml = new StringBuilder();

            //sbHtml.AppendLine("<html><head><style>BODY {font-size: 8pt; color: black; font-family: Tahoma;BACKGROUND-COLOR: white;}");
            //sbHtml.AppendLine(".NORMAL {font-weight: bold; font-size: 10pt; color: white; font-family: Tahoma; background-color: #6699cc; text-align: center;}");
            //sbHtml.AppendLine(".ALTA {font-weight: bold; font-size: 10pt; color: white; font-family: Tahoma; background-color: #ca0000; text-align: center;}");
            //sbHtml.AppendLine("table {font-size: 10pt; font-family: Tahoma}</style></head>");
            //sbHtml.AppendLine("<html><body><table cellSpacing=\"0\" cellPadding=\"0\" width=\"70%\" align=\"center\" border=\"0\" style=\"border: 1px solid #000000;\"><tr><td><table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\" border=\"0\">");
            //sbHtml.AppendFormat("{0} {1}", "<tbody><tr><td vAlign=\"top\" align=\"right\"><b><font face=\"Tahoma\" size=\"2\">", DateTime.Now.DayOfWeek.ToString(), DateTime.Now.Date.ToShortDateString(), "</b></font></td></tr>\\n");
            //sbHtml.AppendLine("</td></tr></table></td></tr></tbody></table><br />");
            //sbHtml.AppendLine("<table cellSpacing=\"0\" cellPadding=\"0\" width=\"70%\" align=\"center\" border=\"0\" style=\"border: 1px solid #000000\">");
            //sbHtml.AppendFormat("<tr><td><table cellSpacing=\"0\" cellPadding=\"0\" width=\"100%\"><tr class=\"NORMAL\"><td><br />{0}<br />&nbsp;</td></tr></table>", mensaje);
            sbHtml.AppendLine(mensaje);
            using (MailMessage mailMensaje = new MailMessage())
            {
                foreach (string sCorreo in correoDestinatarios)
                {
                    mailMensaje.To.Add(new MailAddress(sCorreo));
                }
                mailMensaje.From = new MailAddress(DireccionMail, NombreRemitente);
                mailMensaje.Subject = asunto;
                mailMensaje.IsBodyHtml = true;
                mailMensaje.Body = sbHtml.ToString();
                foreach (Attachment adjunto in adjuntos)
                {
                    mailMensaje.Attachments.Add(adjunto);
                }

                SmtpClient smtpCliente = new SmtpClient(ServidorSmtp, ServidorSmtpPuerto);
                try
                {
                    smtpCliente.Send(mailMensaje);
                }
                catch (Exception ex)
                {
                    bCorreoEnviado = false;
                    ControlErrores.Generar_Error(string.Format("Error al intentar enviar correos: {0}", ex.Message));
                }
            }

            sbHtml = null;
        }

        [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]        
        public static void CorreoNotificacion(string sEncargado, string tipoDocumento, string estado, string Sistema,
            string comentarios, List<string> correoDestinatarios) {
                        StringBuilder sbMensaje = new StringBuilder();

            sbMensaje.AppendFormat("Fecha: '{0}'", DateTime.Now.ToString("yyyy/MM/dd"));
            sbMensaje.AppendLine("<br />");
            sbMensaje.AppendFormat("El documento con el folio: '{0} - {1}' ha cambiado de estado.", sEncargado, tipoDocumento);
            sbMensaje.AppendLine("<br />");
            sbMensaje.AppendFormat("Autorizador: {0}.", Sistema);
            sbMensaje.AppendLine("<br /><br />");
            sbMensaje.AppendFormat("Detalles:</br> <b>-{0}-</b>.", comentarios);

            string sAsunto = string.Format("SC - {0} {1} - {2}", sEncargado, tipoDocumento, estado);

            UtilidadCorreo.EnviarCorreo(correoDestinatarios, sAsunto, sbMensaje.ToString());

            sbMensaje = null;
        

        }

        #endregion
    }
}