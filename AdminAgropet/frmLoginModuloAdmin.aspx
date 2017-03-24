<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmLoginModuloAdmin.aspx.cs" Inherits="AdminAgropet.frmLoginModuloAdmin" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE7"/>
    <link rel="stylesheet" type="text/css" href="<%# ResolveUrl("~/CSS/Estilo.css") %>" />
    <script src="<%# ResolveUrl("~/JS/jquery-1.8.3.min.js") %>" type="text/javascript"></script>
    <script src="<%# ResolveUrl("~/JS/funcionesJS.js") %>" type="text/javascript"></script>
    <script src="<%# ResolveUrl("~/JS/jquery.topbar.js") %>" type="text/javascript"></script>
    <script type="text/javascript" language="javascript">
        function MostrarMensaje() {
            $("#div_Mensaje").showTopbarMessage({ background: "#808080", close: 3000, foreColor: "#FFFFFF", fontSize: "10pt" });
        }
    </script>
    <title>AgroPET  -  Acceso al módulo de administración </title>
</head>
<body style="background-color:white;">
    <div id="div_Mensaje" runat="server" enableviewstate="false" style="display: none;"> </div>

    <form id="form1" runat="server" style="border: solid 1px #000000;">
    <div class="Header">
        <!-- Inicia Panel de encabezado linea roja -->
        <!-- Termina Panel de encabezado linea roja -->
        <!-- Inicia Panel de encabezado bloques -->
        <table border="0" cellpadding="0" cellspacing="0" style="width:100%;">
            <!-- Titulo y cuadros de colores --> 
            <tr>
                <td style="width: 40%; height: 60px;">
                    <div style="vertical-align:middle; margin-left:10px;">
                        <%--Imagen--%>
                        <img id="imgLogo" src="<%= ResolveUrl("~/Imagenes/petceterablanco_small.jpg") %>" alt="Logo" />
                        <%--Titulo--%>
                    </div>
                    <div style="vertical-align: central;">
                        <span style="text-align: right">Módulo de administración  </span>
                    </div>
                </td>
                <td style="width: 60%; height: 60px;">
                    <%--Tabla de cuadros de colores Header--%>
                    <table style="width:100%; height:100%;" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <td class="CuadroEncabezado" bgcolor="White">&nbsp;</td>
                            <td class="CuadroEncabezado" bgcolor="White">&nbsp;</td>
                            <td class="CuadroEncabezado" bgcolor="White">&nbsp;</td>
                            <td class="CuadroEncabezado" bgcolor="White">&nbsp;</td>
                            <td class="CuadroEncabezado" bgcolor="White" style=" text-align:center;">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <!-- Línea verde -->
            <tr>
                <td colspan="2" style="height: 7px; background-color: #00ff00;"></td>
            </tr>
            <!-- Inicia Datos de Sesion -->
            <tr>
                <td colspan="2" style="height: 30px; background-color: white;">
                    <table border="0" cellpadding="0" cellspacing="0" style="width:100%">
                        <tr>
                            <td style="width:60%">&nbsp;</td>
                            <td style="width:20%">&nbsp;</td>
                            <td style="width:20%">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <!-- Termina Datos de Sesion -->
        </table>
        <!-- Termina Panel de encabezado bloques -->
    </div>
    <!-- Inicia contenido -->
        <br />
        <br />
    <div class="CenterContainer">
        <center>
            <div style="border: #000000 1px solid; background-color:gray; width:500px; height:250px;" >
                <table runat="server" width="100%">
                <tr>
                    <td align="center" colspan="3" style="height: 80px;"><span class="fuenteLoginBox" style=" color: #000067;">Ingreso de Usuarios</span></td>
                </tr>
                <tr>
                    <td align="center">
                        <span style="font-family: Verdana; font-size: 12px; color: #000067;">Usuario:</span>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txt_Usuario" runat="server" MaxLength="12" onpaste="return false;" Width="150px"></asp:TextBox>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td></td>
                    <td align="center">
                        <asp:RequiredFieldValidator ID="req_txt_Usuario" runat="server" ControlToValidate="txt_Usuario" ErrorMessage="*">
                            <span style="font-family: Verdana; font-size: 12px; color: #C71444;">Ingrese su usuario</span>
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center">
                        <span style="font-family: Verdana; font-size: 12px; color: #000067;">Contrase&ntilde;a:</span>
                    </td>
                    <td align="center">
                        <asp:TextBox ID="txt_Contrasena" runat="server" TextMode="Password" Width="150px" onpaste="return false;"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td align="center">
                        <asp:RequiredFieldValidator ID="req_txt_Contrasena" runat="server" 
                            ControlToValidate="txt_Contrasena" ErrorMessage="*" Enabled="false">
                            <span style="font-family: Verdana; font-size: 12px; color: #C71444;">Ingrese su contrase&ntilde;a</span>
                        </asp:RequiredFieldValidator>
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <asp:Button ID="btn_Aceptar" runat="server" Text="Aceptar" BackColor="white" ForeColor="DarkBlue" Font-Bold="true"
                             Width="150px" Height="30px" onclick="btn_Aceptar_Click" />
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="3">
                        <span style="font-family: Verdana; font-size: 12px; color: #00008b;">Introduzca nombre de usuario y Password para acceder al sistema</span>
                    </td>
                </tr>
                </table>
            </div>
        </center>
    </div>
        <br />
    <!-- Termina contenido -->
    <!-- Inicia pie de pagina -->
    <div class="Footer">
        <asp:Panel runat="server" ID="pnlFooter" EnableViewState="false">
            <table id="Table1" cellpadding="0" cellspacing="0" width="100%">
                <tr>
                    <td width="20" height="10"></td>
                    <td width="260"></td>
                    <td width="1"><spacer type="block" width="1" height="1" /></td>
                    <td width="10"></td>
                    <td width="60px" style=" text-align:left;  " class="FooterText"><a href="#" class="FooterText"></a></td>
                    <td width="1"><spacer type="block" width="1" height="1" /></td>
                    <td width="10" class="FooterText"> </td>
                    <td width="83px" class="FooterText"><a href="#" class="FooterText"></a></td>
                    <td width="1"><spacer type="block" width="1" height="1" /></td>
                    <td width="10" class="FooterText"> <spacer /></td>
                    <td width="125" class="FooterText"><a href="#" class="FooterText"></a></td>
                    <td width="84"><spacer type="block" width="1" height="1" /></td>
                    <td style=" text-align:right;" ><asp:Image runat="server" ID="imgFooter1"  ImageUrl="~/Imagenes/g_nissanFooter.gif" Height="10px" AlternateText="Nissan" EnableViewState="false" /></td>
                </tr>
            </table>  
        </asp:Panel>
    </div>
    <!-- Termina pie de pagina -->
    </form>
</body>
</html>
