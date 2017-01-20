<%@ Page Title="" Language="C#" MasterPageFile="~/Agropet.Master" AutoEventWireup="true" CodeBehind="frmAccesosPerfiles.aspx.cs" Inherits="AdminAgropet.frmAccesosPerfiles" %>
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
                    Diseño de Perfiles
                </div>
                <div class="DivSeccionEspacio"></div>
                <asp:Panel ID="FiltroBusqueda" runat="server">
                    <br />
                    <center>
                        <table>
                            <tr>
                                <td>
                                    <td style="width:75px;"></td>
                                </td>
                                <td>
                                    <asp:Button ID="btnBuscar" runat="server" Text="Buscar" OnClick="btnBuscar_Click" />
                                </td>
                                <td>
                                    <td style="width:75px;"></td>
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
                                        <br />
                                        <asp:Label ID="label2" runat="server" Text="Seleccione Perfil: " CssClass="Etiqueta"></asp:Label>
                                    </td>
                                    <td>
                                        <uc1:wucDropDownEntidadNegocio ID="ddluc_Perfil" runat="server" />
                                    </td>
                                    <td style="width:100px;"></td>
                                    <td>
                                    </td>
                                    <td>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div id="divDetPerfil" runat="server" class="BordesDivBusquedas" visible="false">
                            <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Auto" CssClass="Centrar">
                                <div class="separadorGris">
                                    Resultado de la Busqueda
                                </div>
                                <br />
                                <center>
                                <div class="Centrar">
                                    <asp:Panel ID="pnlGrd2" runat="server" ScrollBars="Vertical" CssClass="Centrar" Height="150px" 
                                        Width="400px" BorderStyle="Solid" BorderWidth="1px" BorderColor="Black" HorizontalAlign="Center">
                                    <asp:GridView ID="grdDetPerfil" runat="server" SkinID="NoPaginacion" AutoGenerateColumns="false"
                                         DataKeyNames="iddetaccesoxperfil, idperfil, idmenu, idmenuusr, idpadre, ordenh"
                                         AllowPaging="false" HorizontalAlign="Center" OnRowDataBound="grdDetPerfil_RowDataBound" >
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelTodoHdr" runat="server"
                                                         ToolTip="Seleccionar / Des-seleccionar todos" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelItem" runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="nombremenu" HeaderText="Opción Menú" />
                                            <asp:BoundField DataField="espadre" HeaderText="Es Menú Principal" />
                                        </Columns>
                                    </asp:GridView>
                                    </asp:Panel>
                                </div>
                                </center>
                            </asp:Panel>
                            <br />
                            <asp:Button ID="btnGuardaDetPerfil" runat="server" Text="Guardar Detalle Perfil" Width="200px" OnClick="btnGuardaDetPerfil_Click" />
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnCancelarDetPerfil" runat="server" Text="Cancelar" Width="150px" OnClick="btnCancelarDetPerfil_Click" />
                            <br />
                            <br />
                        </div>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
