<%@ Page Title="" Language="C#" MasterPageFile="~/Agropet.Master" AutoEventWireup="true" CodeBehind="frmABCUsuarios.aspx.cs" Inherits="AdminAgropet.ABCUsuarios" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ControlWEB/Comun/wucDropDownEntidadNegocio.ascx" TagName="wucDropDownEntidadNegocio" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .SeparadorPag
        {
        	background-color: #d3d3d3;
            color:darkblue;
            font-family: Verdana;
            font-size: 13px;
            font-weight:bold;
            letter-spacing:4px;
            height:16px;
            text-decoration: none;
            text-align: center;
            text-transform:uppercase;
        }
        .separadorGris
        {
        	background-repeat:repeat-x;
        	background-image:url('../../Imagenes/barunder_gris.JPG');
            color:Black;
            font-family: Verdana;
            font-size:12px;
            font-weight:bold;
            height:14px;
            letter-spacing:2px;
            text-decoration:none;
            text-align: center;
            text-transform:capitalize;
            font-variant:small-caps;
        }
        .separadorNegro
        {
            background-repeat:repeat-x;
            background-image:url('../../Imagenes/barunder.JPG');
            color:White;
            font-family: Verdana;
            font-size: 12px;
            font-weight:bold;
            height:14px;
            letter-spacing:2px;
            text-decoration:none;
            text-align: center;
            text-transform:capitalize;
            font-variant:small-caps;
        }
        .DivSeccion
        {
	        border: 1px solid #C0C0C0;
            color:White;
            font-size: 12px;
            font-family: Verdana;
            font-weight:bold;
            text-decoration: none;
            text-align: center;
            width: 97%;
        }
        .DivSeccionEspacio
        {
	        height:6px;
            width:100%;
        }
        .SeparadorSec
        {
	        background-repeat:repeat-x;
            background-image:url('../../Imagenes/barunder.JPG');
            color:White;
            font-family: Verdana;
            font-size: 12px;
            font-weight:bold;
            height:14px;
            letter-spacing:2px;
            text-decoration: none;
            text-align: center;
            text-transform:capitalize;
            font-variant:small-caps;
        }
        .Etiqueta
        {
            font-weight:bold;
            font-size:10px;
            color:#666666;
        }
        .BordesDivBusquedas
        {
	        width: 97%;
	        margin: 15px auto;
	        border: 1px; 
	        border-color: darkblue;
	        border-style:solid;
        }
        .Centrar
        {
            text-align: center;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <center>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <br />
                <div class="SeparadorPag">
                    Cat&aacute;logo de Usuarios
                </div>
                <div class="DivSeccionEspacio"></div>
                <asp:Panel ID="FiltroBusqueda" runat="server">
                    <div class="DivSeccion" align="center">
                        B&uacute;squeda
                    </div>
                    <br />
                    <center>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" OnClick="btnNuevo_Click" />
                                </td>
                                <td>
                                    <td style="width:75px;"></td>
                                </td>
                                <td>
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div id="Busqueda" runat="server" class="BordesDivBusquedas">
                            <table width="100%">
                                <tr>
                                    <td style="width:150px;">
                                        <asp:Label ID="label2" runat="server" Text="Seleccione Usuario: " CssClass="Etiqueta"></asp:Label>
                                    </td>
                                    <td style="width:350px;">
                                        <uc1:wucDropDownEntidadNegocio ID="ddluc_UserBus" runat="server" />
                                    </td>
                                    <td style="width:30px;"></td>
                                    <td style="width:150px;">
                                        <asp:Label ID="lblPerfilBus" runat="server" Text="Seleccione Perfil: " CssClass="Etiqueta"></asp:Label>
                                    </td>
                                    <td style="width:350px;">
                                        <uc1:wucDropDownEntidadNegocio ID="ddluc_PerfilBus" runat="server" />
                                    </td>
                                    <td style="width:50px;"></td>
                                    <td style="width:100px;">
                                        <asp:Label ID="label3" runat="server" Text="Estatus: " CssClass="Etiqueta"></asp:Label>
                                    </td>
                                    <td style="width:150px;">
                                        <asp:DropDownList ID="SelEstatus" runat="server" AutoPostBack="false">
                                            <asp:ListItem Text="Seleccione..." Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No Activo" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divResulBusqueda" runat="server" class="BordesDivBusquedas" visible="false">
                            <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Auto" CssClass="Centrar" Height="180px">
                                <div class="separadorGris">
                                    Resultado de la Busqueda
                                </div>
                                <br />
                                <div class="Centrar">
                                    <asp:Panel ID="pnlGrd2" runat="server" ScrollBars="Vertical" CssClass="Centrar" Height="150px">
                                    <asp:GridView ID="grdUsuarios" runat="server" SkinID="NoPaginacion" AutoGenerateColumns="false"
                                         DataKeyNames="idusuario, idperfil, activo"
                                         AllowPaging="true" HorizontalAlign="Center" 
                                         OnPageIndexChanging="grdUsuarios_PageIndexChanging" 
                                         OnRowCommand="grdUsuarios_RowCommand" 
                                         OnRowDataBound="grdUsuarios_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgEditItem" runat="server"
                                                         ImageUrl="~/Imagenes/ico_editar_chico.gif"
                                                         ToolTip="Editar información del usuario." />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelTodoHdr" runat="server"
                                                         ToolTip="Seleccionar / Des-seleccionar todos" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelItem" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="idusuario" HeaderText="ID Usuario" />
                                            <asp:BoundField DataField="nombreperfil" HeaderText="Perfil" />
                                            <asp:BoundField DataField="claveusr" HeaderText="Clave Usuario" />
                                            <asp:BoundField DataField="nombreusr" HeaderText="Nombre Usuario" />
                                            <asp:BoundField DataField="fechacreacion" HeaderText="Fecha Creación" />
                                            <asp:BoundField DataField="usuariocreo" HeaderText="Creado por" />
                                            <asp:BoundField DataField="fechaultmodif" HeaderText="Fecha Ult. Modif." />
                                            <asp:BoundField DataField="usuarioultmodif" HeaderText="Modificado por" />
                                            <asp:BoundField DataField="Estatus" HeaderText="Estatus" />
                                        </Columns>
                                    </asp:GridView>
                                    </asp:Panel>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Button ID="btnCambiaEstatus" runat="server" Text="Activar / Des-activar" Width="200px" />
                            <br />
                            <br />
                        </div>
                        <div id="DivUsuariosABC" runat="server" class="BordesDivBusquedas" visible="false">
                            <asp:Panel ID="pnlUsuariosABC" runat="server" ScrollBars="None" CssClass="Centrar" Width="100%"
                                 Height="180px" BorderColor="Black" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center">
                                 <div id="divAlta" runat="server" class="separadorNegro" visible="false">
                                     Alta de Nuevo Usuario
                                 </div>
                                <div id="divCambio" runat="server" class="separadorNegro" visible="false">
                                    Modificación de información del usuario
                                </div>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblIdUsuario" runat="server" Text="Id Usuario: " CssClass="Etiqueta">
                                            </asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtIdUsuario" runat="server" CssClass="" Width="80px" Enabled="false"
                                                 ToolTip="Campo de solo consulta, muestra el ID del usuario."></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:Label ID="lblNomUsusuario" runat="server" Text="Nombre del Usuario" CssClass="Etiqueta"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNombreUsuario" runat="server" CssClass="" Width="250px" MaxLength="100"
                                                     ToolTip="Captura el nombre del usuario que se va a registrar."></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblCveUsuario" runat="server" Text="Usuario: " CssClass="Etiqueta">
                                            </asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtCveUsuario" runat="server" CssClass="" Width="100px" MaxLength="20"
                                                 ToolTip="Captura el nombre que usuara el usuario para accesar al sistema."></asp:TextBox>
                                        </td>

                                        <td align="right">
                                            <asp:Label ID="lblPasswordUsr" runat="server" Text="Password: " CssClass="Etiqueta">
                                            </asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPasswordUsr" runat="server" CssClass="" Width="100px" MaxLength="20"
                                                 ToolTip="Capture el password que el usuario usuara para accesar al sistema." TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblConfirmPass" runat="server" Text="Confirmar Password: " CssClass="Etiqueta"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtConfirmPass" runat="server" CssClass="" Width="100px" MaxLength="20"
                                                 ToolTip="Confirme el password especificado para el usuario." TextMode="Password"></asp:TextBox>
                                            <asp:CompareValidator ID="CV_ComparaPass" runat="server" ControlToValidate="txtPasswordUsr"
                                                 ErrorMessage="Las contraseñas especificadas no son iguales, por ravor corrijalas."
                                                 ControlToCompare="txtConfirmPass" Display="None" EnableClientScript="true">
                                            </asp:CompareValidator>
                                            <asp:ValidatorCalloutExtender ID="VCE_CV_ComparaPass" runat="server" TargetControlID="CV_ComparaPass">
                                            </asp:ValidatorCalloutExtender>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblUserCreo" runat="server" Text="Usuario Creó: " CssClass="Etiqueta"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtUsuarioCreo" runat="server" CssClass="" Width="130px" Enabled="false"
                                                 ToolTip="Usuario que dio de alta a este usuario.">
                                            </asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblFechaC" runat="server" Text="Fecha Creación: " CssClass="Etiqueta">
                                            </asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFechaCreacion" runat="server" CssClass="" Width="130px" Enabled="false"
                                                 ToolTip="Fecha en que fue creado el usuario."></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblUserModif" runat="server" Text="Usuario Modificó." CssClass="Etiqueta">
                                            </asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtUserModifico" runat="server" CssClass="" Width="200px" Enabled="false"
                                                 ToolTip="Usuario que realizo la última modificación."></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblFechaModif" runat="server" Text="Fecha Modificación: " CssClass="Etiqueta">
                                            </asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFechaModif" runat="server" CssClass="" Width="150px" Enabled="false"
                                                 ToolTip="Fecha de Últ. Modificación al usuario."></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblEstatus" runat="server" Text="Estatus" CssClass="Etiqueta"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlEstadoABC" runat="server" AutoPostBack="false">
                                                <asp:ListItem Text="Seleccione..." Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No Activo" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                        <td>
                                            <asp:Label ID="Label1" runat="server" Text="Seleccione Perfil: " CssClass="Etiqueta"></asp:Label>
                                        </td>
                                        <td>
                                            <uc1:wucDropDownEntidadNegocio ID="ddl_PerfilABC" runat="server" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="8">
                                            <br />
                                            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" Text="Guardar" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" Text="Cancelar" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnLimpiar" runat="server" OnClick="btnLimpiar_Click" Text="Limpiar" />
                                            <asp:HiddenField ID="hdnFlag" runat="server" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </center>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
