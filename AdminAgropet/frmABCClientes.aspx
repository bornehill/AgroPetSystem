<%@ Page Title="" Language="C#" MasterPageFile="~/Agropet.Master" AutoEventWireup="true" CodeBehind="frmABCClientes.aspx.cs" Inherits="AdminAgropet.frmABCClientes" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ControlWEB/Comun/wucDropDownEntidadNegocio.ascx" TagName="wucDropDownEntidadNegocio" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="mvwClientes" runat="server">
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
                                            <label style="text-align: left; width: 100%;">Estado:</label>
                                            <asp:DropDownList ID="ddlEstado" runat="server" Width="200px" AutoPostBack="false" class="form-control">
                                                <asp:ListItem Text="Seleccione..." Value="-1"></asp:ListItem>
                                                <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                                                <asp:ListItem Text="No Activo" Value="0"></asp:ListItem>
                                            </asp:DropDownList>
                                        </div>
                                    </div>
                                    <div class=" col-md-5">
                                        <label style="text-align: left; width: 100%;">Razon Social:</label>
                                        <input id="txtRazonSocialBusca" runat="server" style="width: 100%" class="form-control" placeholder="Buscar por..." />
                                    </div>
                                    <div class="col-md-4">
                                        <br />
                                        <asp:Button ID="btnBuscar" runat="server" class="btn btn-success" Text="Buscar" OnClick="btnBuscar_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnNuevo" runat="server" class="btn btn-info" Text="Nuevo" OnClick="btnNuevo_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="gvwConsulta" runat="server" AutoGenerateColumns="false" DataKeyNames="idcliente, idestado, idgirocliente, passwdcliente"
                                            OnRowCommand="gvwConsulta_RowCommand" OnRowDataBound="gvwConsulta_RowDataBound" OnPageIndexChanging="gvwConsulta_PageIndexChanging"
                                            CssClass="table table-striped table-bordered table-hover" AllowPaging="true" PageSize="10">
                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" Height="10px" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
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
                                                <asp:BoundField DataField="razonsocial" HeaderText="" />
                                                <asp:BoundField DataField="nombrecontacto" HeaderText="" />
                                            </Columns>
                                            <EmptyDataTemplate>Sin datos</EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>

                <asp:View ID="vwNuevo" runat="server">
                    <br />
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <label id="lblTituloNuevo" runat="server">CLIENTES</label>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-5">
                                        <label style="text-align: left; width: 100%;">Razon Social:</label>
                                        <input type="text" id="txtRazonSocial" runat="server" class="form-control" placeholder="Razon Social..." />
                                    </div>
                                    <div class="col-lg-5">
                                        <label style="text-align: left; width: 100%;">Nombre Contacto:</label>
                                        <input type="text" id="txtNombreContacto" runat="server" class="form-control" placeholder="Nombre de Contacto...." />
                                    </div>
                                    <div class="col-lg-2" style="text-align: left;">
                                        <label style="text-align: left; width: 100%;">Estatus:</label>
                                        <div class="btn btn-default" style="height: 34px">
                                            <div class="material-switch pull-right" style="padding-top: 10px">
                                                <input id="chkEstado" runat="server" type="checkbox" />
                                                <label for="ContentPlaceHolder1_chkEstado" style="text-align: left" class="label-success"></label>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-5">
                                        <label style="text-align: left; width: 100%;">Causa Suspension:</label>
                                        <input type="text" id="txtCausaSusp" runat="server" class="form-control" placeholder="Causa de suspension...." />
                                    </div>
                                    <div class="col-lg-5">
                                        <label style="text-align: left; width: 100%;">Limite Credito:</label>
                                        <input type="text" id="txtLimiteCredito" runat="server" class="form-control" placeholder="Limite Credito...." />
                                    </div>
                                    <div class="col-lg-2">
                                        <label style="text-align: left; width: 100%;">Moneda:</label>
                                        <asp:DropDownList ID="ddlMoneda" runat="server" Style="width: 100%;" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-5">
                                        <label style="text-align: left; width: 100%;">Pago:</label>
                                        <asp:DropDownList ID="ddlTipoPago" runat="server" Style="width: 30%;" AutoPostBack="false" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-5">
                                        <label style="text-align: left; width: 100%;">Tipo de Cliente:</label>
                                        <asp:DropDownList ID="ddlTipoCliente" runat="server" Style="width: 30%;" AutoPostBack="false" class="form-control">
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-2">
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-12">
                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" class="btn btn-success" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" class="btn btn-warning" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" class="btn btn-default" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>


                <asp:View ID="vwDetalle" runat="server">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <label>DETALLE DE CLIENTES</label>
                            </div>

                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <center>
                                            <button id="btnNuevoDetalle" runat="server" class="btn btn-success" onserverclick="btnNuevoDetalle_ServerClick" validationgroup="guarda">Nuevo</button>
                                            <button id="btnCancelarDetalle" runat="server" class="btn btn-info" onserverclick="btnCancelarDetalle_ServerClick">Cancelar</button>
                                        </center>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <center>
                                            <br />
                                            <asp:GridView ID="gvwConsultaDetalle" runat="server" AutoGenerateColumns="false" DataKeyNames="dirsClientesId, clienteId"
                                                CssClass="table table-striped table-bordered table-hover" OnRowCommand="gvwConsultaDetalle_RowCommand" OnRowDataBound="gvwConsultaDetalle_RowDataBound"
                                                AllowPaging="true" PageSize="10" OnPageIndexChanging="gvwConsultaDetalle_PageIndexChanging">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" Height="10px" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibnEditarCliente" runat="server" Width="20px" ImageUrl="~/Imagenes/Iconos/Editar.PNG"
                                                                CommandName="Editar" CommandArgument='<%# Container.DataItemIndex %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibnEliminarCliente" runat="server" Width="20px" ImageUrl="~/Imagenes/Iconos/Basurero.PNG"
                                                                CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>'
                                                                OnClientClick="return confirm('Are you confirm to delete?')" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="correoelectronico" HeaderText="" />
                                                    
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
                                                <EmptyDataTemplate>Sin datos</EmptyDataTemplate>
                                            </asp:GridView>
                                        </center>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>

                <asp:View ID="vwNuevoDetalle" runat="server">
                    <br />
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <label id="lblTituloNuevoDetalle" runat="server">CLIENTES</label>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-8">
                                        <label style="text-align: left; width: 100%;">Calle:</label>
                                        <input type="text" id="txtCalle" runat="server" class="form-control" placeholder="Calle...." />
                                    </div>
                                    <div class="col-lg-2">
                                        <label style="text-align: left; width: 100%;">Num Int: </label>
                                        <input type="text" id="txtNumInterior" runat="server" class="form-control" placeholder="#...." />
                                    </div>
                                    <div class="col-lg-2">
                                        <label style="text-align: left; width: 100%;">Num Ext:</label>
                                        <input type="text" id="txtNumExterior" runat="server" class="form-control" placeholder="#...." />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-7">
                                        <label style="text-align: left; width: 100%;">Colonia</label>
                                        <input type="text" id="txtColonia" runat="server" class="form-control" placeholder="Colonia...." />
                                    </div>
                                    <div class="col-lg-1">
                                        <label style="text-align: left; width: 100%;">CP: </label>
                                        <input type="text" id="txtCodigoPostal" runat="server" class="form-control" placeholder="CP...." />
                                    </div>
                                    <div class="col-lg-4">
                                        <label style="text-align: left; width: 100%;">Ciudad: </label>
                                        <input type="text" id="txtCiudad" runat="server" class="form-control" placeholder="Ciudad...." />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3">
                                        <label style="text-align: left; width: 100%;">Estado:</label>
                                        <asp:DropDownList ID="ddlEstadoAlta" class="form-control" runat="server" Width="250px"></asp:DropDownList>
                                        <asp:ListSearchExtender ID="lexddlEstadoAlta" runat="server" TargetControlID="ddlEstadoAlta"
                                            PromptText="Busqueda por.." QueryPattern="Contains">
                                        </asp:ListSearchExtender>
                                    </div>
                                    <div class="col-lg-4">
                                        <label style="text-align: left; width: 100%;">Correo Electronico:</label>
                                        <input type="text" id="txtCorreoElectronico" runat="server" class="form-control" placeholder="Ingresa Correo...." />
                                    </div>
                                    <div class="col-lg-2">
                                        <label style="text-align: left; width: 100%;">Lada:</label>
                                        <input type="text" id="txtLada" runat="server" class="form-control" placeholder="Lada...." />
                                    </div>
                                    <div class="col-lg-2">
                                        <label style="text-align: left; width: 100%;">Telefono:</label>
                                        <input type="text" id="txtTelefono" runat="server" class="form-control" placeholder="Telefono...." />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-3">
                                        <label style="text-align: left; width: 100%;">Tel. Movil:</label>
                                        <input type="text" id="Text1" runat="server" class="form-control" placeholder="Telefono Movil...." />
                                    </div>
                                    <div class="col-lg-4">
                                        <label style="text-align: left; width: 100%;">Contraseña:</label>
                                        <input type="text" id="txtContraseña" runat="server" class="form-control" placeholder="Contraseña...." />
                                    </div>
                                    <div class="col-lg-4">
                                        <label style="text-align: left; width: 100%;">Confirma Contraseña:</label>
                                        <input type="text" id="txtConfirmaContraseña" runat="server" class="form-control" placeholder="Confirma Contraseña...." />
                                    </div>
                                </div>
                                <br />
                                <div class="row">
                                    <div class="col-lg-12">
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>
            </asp:MultiView>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
