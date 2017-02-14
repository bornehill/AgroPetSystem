<%@ Page Title="" Language="C#" MasterPageFile="~/Agropet.Master" AutoEventWireup="true" CodeBehind="frmABCClientes.aspx.cs" Inherits="AdminAgropet.frmABCClientes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ControlWEB/Comun/wucDropDownEntidadNegocio.ascx" TagName="wucDropDownEntidadNegocio" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="mvwArticulos" runat="server">
                <asp:View ID="vwConsulta" runat="server">
                    <div class="col-lg-12">
                        <br />
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <label>BUSQUEDA DE CLIENTES</label>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                         <div class="form-group col-lg-6">
                                            <label style="text-align: left; width: 100%;">Estado</label>
                                            <asp:DropDownList ID="ddlArticulos" class="form-control" runat="server" ValidationGroup="guarda" Width="100%"></asp:DropDownList>
                                            <asp:ListSearchExtender ID="lseArticulos" runat="server" TargetControlID="ddlArticulos" 
                                                PromptText="Busqueda por.." QueryPattern="Contains">
                                            </asp:ListSearchExtender>
                                        </div>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                        <label style="text-align: left; width: 100%;">Razon Social</label>
                                        <input id="txtRazonSocialBuscar" runat="server" class="form-control"  placeholder="Buscar por..." />
                                    </div>
                                    <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">

                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                </asp:View>
            </asp:MultiView>


            <br />
            <div class="SeparadorPag">
                Cat&aacute;logo de Clientes
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
                                <td style="width: 75px;"></td>
                            </td>
                            <td>
                                <asp:Button ID="btnBuscar" runat="server" Text="Buscar" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <div id="Busqueda" runat="server" class="BordesDivBusquedas">
                        <div class="SeparadorPag">
                            Criterios de b&uacute;squeda
                        </div>
                        <br />
                        <table>
                            <tr>
                                <td>
                                    <asp:Label ID="label2" runat="server" Text="Correo electronico: " CssClass="Etiqueta"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtCorreoBus" runat="server" CssClass="" Width="250px" Enabled="false"
                                        ToolTip="Capture el correo electrónico del cliente a buscar."></asp:TextBox>
                                </td>
                                <td style="width: 100px;"></td>
                                <td>
                                    <asp:Label ID="label3" runat="server" Text="Seleccione Estado: " CssClass="Etiqueta"></asp:Label>
                                </td>
                                <td>
                                    <uc1:wucDropDownEntidadNegocio ID="ddlUC_Estados" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divResulBusqueda" runat="server" class="BordesDivBusquedas" visible="false">
                        <asp:Panel ID="pnlGrid" runat="server" ScrollBars="Auto" CssClass="Centrar">
                            <div class="separadorGris">
                                Resultado de la b&uacute;squeda
                            </div>
                            <br />
                            <div class="Centrar">
                                <asp:GridView ID="grdClientes" runat="server" SkinID="NoPaginacion" AutoGenerateColumns="false"
                                    AllowPaging="true" HorizontalAlign="Center" PageSize="20"
                                    DataKeyNames="idcliente, idestado, idgirocliente passwdcliente">
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
                                        <asp:BoundField DataField="correoelectronico" HeaderText="" />
                                        <asp:BoundField DataField="razonsocial" HeaderText="" />
                                        <asp:BoundField DataField="nombrecontacto" HeaderText="" />
                                        <asp:BoundField DataField="girocliente" HeaderText="" />
                                        <asp:BoundField DataField="domcalle" HeaderText="" />
                                        <asp:BoundField DataField="domnumext" HeaderText="" />
                                        <asp:BoundField DataField="domnumint" HeaderText="" />
                                        <asp:BoundField DataField="domcp" HeaderText="" />
                                        <asp:BoundField DataField="domcolonia" HeaderText="" />
                                        <asp:BoundField DataField="ladatel" HeaderText="" />
                                        <asp:BoundField DataField="telefono" HeaderText="" />
                                        <asp:BoundField DataField="telmovil" HeaderText="" />
                                        <asp:BoundField DataField="ciudad" HeaderText="" />
                                        <asp:BoundField DataField="estado" HeaderText="" />
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </asp:Panel>
                        <br />
                        <asp:Button ID="btnCambiaEstatus" runat="server" Text="Activar / Des-activar" Width="200px" />
                        <br />
                    </div>
                    <div id="DivClientesABC" runat="server" class="BordesDivBusquedas" visible="false">
                        <asp:Panel ID="pnlClientesABC" runat="server" ScrollBars="None" CssClass="Centrar" Width="100%"
                            Height="350px" BorderColor="Black" BorderWidth="1px" BorderStyle="Solid" HorizontalAlign="Center">
                            <div id="divAlta" runat="server" class="separadorNegro" visible="false">
                                Alta de un cliente Nuevo
                            </div>
                            <div id="divCambio" runat="server" class="separadorNegro" visible="false">
                                Modificación de información de un cliente.
                            </div>
                            <br />
                            <table width="100%">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblcorreocliente" runat="server" Text="Correo Electrónico:" CssClass="Etiqueta">
                                        </asp:Label>
                                    </td>
                                    <td align="left" colspan="5">
                                        <asp:TextBox ID="txtCorreoCliente" runat="server" Width="350px" MaxLength="150"
                                            ToolTip="Capturar el correo electronico del cliente."></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblrazonsocial" runat="server" Text="Razón Social: " CssClass="Etiqueta">
                                        </asp:Label>
                                    </td>
                                    <td align="left" colspan="5">
                                        <asp:TextBox ID="txtRazonSocial" runat="server" Width="400px" MaxLength="200"
                                            ToolTip="Capturar la Razón Social del cliente.."></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblNombreContacto" runat="server" Text="Nombre Contacto: " CssClass="Etiqueta">
                                        </asp:Label>
                                    </td>
                                    <td align="left" colspan="5">
                                        <asp:TextBox ID="txtNombreContacto" runat="server" Width="400px" MaxLength="200"
                                            ToolTip="Capturar el nombre del contacto de este clientes."></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblDomCalle" runat="server" Text="Calle: " CssClass="Etiqueta">
                                        </asp:Label>
                                    </td>
                                    <td align="left" colspan="5">
                                        <asp:TextBox ID="txtDomicilioCalle" runat="server" Width="400px" MaxLength="200"
                                            ToolTip="Capturar el correo electronico del cliente."></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label5" runat="server" Text="Núm. Exterior: " CssClass="Etiqueta">
                                        </asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtNumExterior" runat="server" Width="80px" MaxLength="6"
                                            ToolTip="Capturar el número exterior del domicilio del cliente."></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label6" runat="server" Text="Núm. Interior: " CssClass="Etiqueta">
                                        </asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtNumInterior" runat="server" Width="80px" MaxLength="6"
                                            ToolTip="Capturar el número de interior del domicilio del cliente."></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label1" runat="server" Text="Cod. Postal: " CssClass="Etiqueta">
                                        </asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtCodigoPostal" runat="server" Width="80px" MaxLength="6"
                                            ToolTip="Capturar el código postal del domicilio del cliente."></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblColonia" runat="server" Text="Colonia: " CssClass="Etiqueta">
                                        </asp:Label>
                                    </td>
                                    <td align="left" colspan="3">
                                        <asp:TextBox ID="txtColonia" runat="server" Width="300px" MaxLength="100"
                                            ToolTip="Capturar la colonia del cliente."></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label4" runat="server" Text="Lada: " CssClass="Etiqueta">
                                        </asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtLadaTel" runat="server" Width="100px" MaxLength="6"
                                            ToolTip="Capturar la lada del teléfono del cliente."></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label7" runat="server" Text="Teléfono: " CssClass="Etiqueta">
                                        </asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtTelefono" runat="server" Width="100px" MaxLength="15"
                                            ToolTip="Capturar el número de telefono del cliente."></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label8" runat="server" Text="Tel. Móvil: " CssClass="Etiqueta">
                                        </asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtTelMovil" runat="server" Width="100px" MaxLength="15"
                                            ToolTip="Capturar el número de de telefono móvil del cliente."></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label9" runat="server" Text="Ciudad: " CssClass="Etiqueta">
                                        </asp:Label>
                                    </td>
                                    <td align="left" colspan="4">
                                        <asp:TextBox ID="txtCiudad" runat="server" Width="300px" MaxLength="100"
                                            ToolTip="Capturar la ciudad del cliente.">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblEstado" runat="server" Text="Estado: " CssClass="Etiqueta">
                                        </asp:Label>
                                    </td>
                                    <td align="left" colspan="3">
                                        <uc1:wucDropDownEntidadNegocio ID="ddlUC_EstadosABC" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblPassWd" runat="server" Text="Contraseña: " CssClass="Etiqueta">
                                        </asp:Label>
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:TextBox ID="txtPassWd" runat="server" Width="200px" MaxLength="30"
                                            ToolTip="Capture la contraseña para el cliente.">
                                        </asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblConfirmPass" runat="server" Text="Confirmar Contraseña: " CssClass="Etiqueta">
                                        </asp:Label>
                                    </td>
                                    <td align="left" colspan="2">
                                        <asp:TextBox ID="txtConfirmPassWd" runat="server" Width="200px" MaxLength="30"
                                            ToolTip="Confirme la contraseña para el cliente.">
                                        </asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="6">
                                        <br />
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" />
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
</asp:Content>
