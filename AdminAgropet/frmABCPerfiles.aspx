<%@ Page Title="Catálogo de Perfiles" Language="C#" MasterPageFile="~/Agropet.Master" AutoEventWireup="true" CodeBehind="frmABCPerfiles.aspx.cs" Inherits="AdminAgropet.frmABCPerfiles" %>
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
        .EtiquetaNegra
        {
            font-family:"Verdana";
            font-weight:bold;
            font-size:12px;
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
    <script type="text/javascript">
        $(function () {
            $(document).tooltip({ track: true });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
   
    <center>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <br />
                <div class="SeparadorPag">
                    Cat&aacute;logo de Perfiles
                </div>
                <div class="DivSeccionEspacio"></div>
                <asp:Panel ID="FiltroBusqueda" runat="server">
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
                            <asp:Label ID="lbletiq" runat="server" Text="Criterios para búsqueda" CssClass="EtiquetaNegra"></asp:Label>
                        <br />
                            <table>
                                <tr>
                                    <td>
                                        <asp:Label ID="label2" runat="server" Text="Seleccione Perfil: " CssClass="Etiqueta"></asp:Label>
                                    </td>
                                    <td>
                                        <uc1:wucDropDownEntidadNegocio ID="ddluc_Perfil" runat="server" />
                                    </td>
                                    <td style="width:100px;"></td>
                                    <td>
                                        <asp:Label ID="label3" runat="server" Text="Estatus: " CssClass="Etiqueta"></asp:Label>
                                    </td>
                                    <td>
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
                            <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Auto" CssClass="Centrar">
                                <div class="separadorGris">
                                    Resultado de la Busqueda
                                </div>
                                <br />
                                <div class="Centrar">
                                    <asp:GridView ID="grdPerfiles" runat="server" SkinID="NoPaginacion" AutoGenerateColumns="false"
                                         AllowPaging="true" HorizontalAlign="Center" 
                                        OnPageIndexChanging="grdPerfiles_PageIndexChanging" 
                                        OnRowCommand="grdPerfiles_RowCommand" 
                                        OnRowDataBound="grdPerfiles_RowDataBound">
                                        <Columns>
                                            <asp:TemplateField>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="imgEditItem" runat="server"
                                                         ImageUrl="~/Imagenes/ico_editar_chico.gif"
                                                         CommandName="UpdateRow"
                                                         CommandArgument="<%# ((GridViewRow)Container).RowIndex %>"
                                                         ToolTip="Editar perfil" />
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
                                            <asp:BoundField DataField="idperfil" HeaderText="ID Perfil" />
                                            <asp:BoundField DataField="nombreperfil" HeaderText="Perfil" />
                                            <asp:BoundField DataField="fechacreacion" HeaderText="Fecha Creación" />
                                            <asp:BoundField DataField="UsuarioCreo" HeaderText="Usuario Creó" />
                                            <asp:BoundField DataField="fechaultmodif" HeaderText="Fecha Ult. Modif." />
                                            <asp:BoundField DataField="UsuarioModif" HeaderText="Usuario Modificó" />
                                            <asp:BoundField DataField="Estatus" HeaderText="Estatus" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </asp:Panel>
                            <br />
                            <asp:Button ID="btnCambiaEstatus" runat="server" Text="Activar / Des-activar" Width="200px" />
                            <br />
                            <br />
                        </div>
                        <div id="DivPerfilesABC" runat="server" class="BordesDivBusquedas" Visible="false">
                            <asp:Panel ID="pnlPerfilesABC" runat="server" ScrollBars="None" CssClass="Centrar" Width="100%"
                                 Height="145px" BorderColor="Black" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center">
                                 <div id="divAlta" runat="server" class="separadorNegro" visible="false">
                                     Alta de Nuevo Perfil
                                 </div>
                                <div id="divCambio" runat="server" class="separadorNegro" visible="false">
                                    Modificación de información del perfil
                                </div>
                                <br />
                                <table width="100%">
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblIdPerfil" runat="server" Text="Id Perfil: " CssClass="Etiqueta">
                                            </asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtIdPerfil" runat="server" CssClass="" Width="80px" Enabled="false"
                                                 ToolTip="Campo de solo consulta, muestra el ID del perfil."></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblPerfil" runat="server" Text="Nombre Perfil: " CssClass="Etiqueta">
                                            </asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtPerfil" runat="server" CssClass="" Width="300px" MaxLength="100"
                                                 ToolTip="Captura el nombre del perfil."></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblFechaC" runat="server" Text="Fecha Creación: " CssClass="Etiqueta">
                                            </asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFechaCreacion" runat="server" CssClass="" Width="150px" Enabled="false"
                                                 ToolTip="Fecha en que fue creado el perfil."></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right">
                                            <asp:Label ID="lblusrcreo" runat="server" Text="Creado por: " CssClass="Etiqueta">
                                            </asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtUserCreo" runat="server" CssClass="" Width="200px" Enabled="false"
                                                 ToolTip="Usuario que creo el perfil."></asp:TextBox>
                                        </td>
                                        <td align="right">
                                            <asp:Label ID="lblFechaModif" runat="server" Text="Fecha Modificación: " CssClass="Etiqueta">
                                            </asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:TextBox ID="txtFechaModif" runat="server" CssClass="" Width="150px" Enabled="false"
                                                 ToolTip="Fecha de Últ. Modificación al perfil."></asp:TextBox>
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
                                            <asp:Label ID="lblEstatus" runat="server" Text="Estatus" CssClass="Etiqueta"></asp:Label>
                                        </td>
                                        <td align="left">
                                            <asp:DropDownList ID="ddlEstadoABC" runat="server" AutoPostBack="false" >
                                                <asp:ListItem Text="Seleccione..." Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No Activo" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="6" align="center">
                                            <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" />
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
