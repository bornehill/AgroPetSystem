<%@ Page Title="" Language="C#" MasterPageFile="~/Agropet.Master" AutoEventWireup="true" CodeBehind="frmABCUsuarios.aspx.cs" Inherits="AdminAgropet.ABCUsuarios" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ControlWEB/Comun/wucDropDownEntidadNegocio.ascx" TagName="wucDropDownEntidadNegocio" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:MultiView ID="mvwUsuario" runat="server">
                <asp:View ID="vwConsulta" runat="server">
                    <div class="col-lg-12">
                        <br />
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <label>USUARIOS</label>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                        <label style="text-align: left; width: 100%;">Usuario:</label>
                                        <uc1:wucDropDownEntidadNegocio ID="ddluc_UserBus" runat="server" Width="250px" />
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                        <label style="text-align: left; width: 100%;">Perfil:</label>
                                        <uc1:wucDropDownEntidadNegocio ID="ddluc_PerfilBus" runat="server" Width="250px" />
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                        <label style="text-align: left; width: 100%;">Estado:</label>
                                        <asp:DropDownList ID="SelEstatus" runat="server" class="form-control" AutoPostBack="false" Width="250px">
                                            <asp:ListItem Text="Seleccione..." Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No Activo" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                        <br />
                                        <asp:Button ID="btnNuevo" runat="server" Text="Nuevo" class="btn btn-success" OnClick="btnNuevo_Click" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Button ID="btnBuscar" runat="server" Text="Buscar" class="btn btn-info" OnClick="btnBuscar_Click" />
                                    </div>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-md-12">
                                        <asp:GridView ID="grdUsuarios" runat="server" AutoGenerateColumns="false" DataKeyNames="idusuario, idperfil, activo"
                                            OnRowCommand="grdUsuarios_RowCommand" OnRowDataBound="grdUsuarios_RowDataBound" OnPageIndexChanging="grdUsuarios_PageIndexChanging"
                                            CssClass="table table-striped table-bordered table-hover" AllowPaging="true" PageSize="10">
                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" Height="10px" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <asp:ImageButton ID="imgEditItem" runat="server" Width="20px" ImageUrl="~/Imagenes/Iconos/Editar.PNG"
                                                            CommandName="CmdEdit" CommandArgument='<%# Container.DataItemIndex %>' />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:TemplateField Visible="false">
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
                                <label id="lblTituloNuevo" runat="server">NUEVO/MODIFICACION USUARIO</label>
                            </div>
                            <div class="panel-body">
                                <asp:HiddenField ID="txtIdUsuario" runat="server" />
                                <div class="row">
                                    <div class="col-xs-7">
                                        <label style="text-align: left; width: 100%;">Nombre:</label>
                                        <input type="text" id="txtNombreUsuario" runat="server" class="form-control" maxlength="100" placeholder="Ingresa Nombre..." />
                                    </div>
                                    <div class="col-xs-2">
                                        <label style="text-align: left; width: 100%;">Clave Usr:</label>
                                        <input type="text" id="txtCveUsuario" runat="server" class="form-control" maxlength="20" placeholder="Ingresa Clave Usuario..." />
                                    </div>
                                    <div class="col-xs-3">
                                        <label style="text-align: left; width: 100%;">Perfil:</label>
                                        <uc1:wucDropDownEntidadNegocio ID="ddl_PerfilABC" runat="server" Width="250px"/>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-4">
                                        <label style="text-align: left; width: 100%;">Contraseña:</label>
                                        <input type="text" id="txtPasswordUsr" runat="server" class="form-control" maxlength="20" placeholder="Ingresa Password..." />
                                    </div>
                                    <div class="col-xs-4">
                                        <label style="text-align: left; width: 100%;">Confirma Contraseña:</label>
                                        <input type="text" id="txtConfirmPass" runat="server" class="form-control" maxlength="20" placeholder="Confirma Password..." />
                                        <asp:CompareValidator ID="CV_ComparaPass" runat="server" ControlToValidate="txtPasswordUsr"
                                            ErrorMessage="Las contraseñas especificadas no son iguales, por ravor corrijalas."
                                            ControlToCompare="txtConfirmPass" Display="None" EnableClientScript="true">
                                        </asp:CompareValidator>
                                        <asp:ValidatorCalloutExtender ID="VCE_CV_ComparaPass" runat="server" TargetControlID="CV_ComparaPass">
                                        </asp:ValidatorCalloutExtender>
                                    </div>
                                    <div class="col-xs-4">
                                        <label style="text-align: left; width: 100%;">Estatus:</label>
                                        <asp:DropDownList ID="ddlEstadoABC" runat="server" AutoPostBack="false" class="form-control">
                                            <asp:ListItem Text="Seleccione..." Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No Activo" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-xs-3">
                                        <label style="text-align: left; width: 100%;">Usuario Creó:</label>
                                        <input type="text" id="txtUsuarioCreo" runat="server" class="form-control" readonly="readonly" placeholder="Usuario que dio de alta a este usuario...." />

                                    </div>
                                    <div class="col-xs-3">
                                        <label style="text-align: left; width: 100%;">Fecha Creacion:</label>
                                        <input type="text" id="txtFechaCreacion" runat="server" class="form-control" readonly="readonly" placeholder="Fecha en que fue creado el usuario..." />

                                    </div>
                                    <div class="col-xs-3">
                                        <label style="text-align: left; width: 100%;">Usuario Modif:</label>
                                        <input type="text" id="txtUserModifico" runat="server" class="form-control" readonly="readonly" placeholder="Usuario que realizo la última modificación..." />

                                    </div>
                                    <div class="col-xs-3">
                                        <label style="text-align: left; width: 100%;">Fecha Modificación:</label>
                                        <input type="text" id="txtFechaModif" runat="server" class="form-control" readonly="readonly" placeholder="Fecha de Últ. Modificación al usuario..." />

                                    </div>
                                </div>
                                 <div class="row" style="text-align:center">
                                    <div class="col-xs-12">
                                         <br />
                                            <asp:Button ID="btnGuardar" runat="server" OnClick="btnGuardar_Click" class="btn btn-success" Text="Guardar" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnCancelar" runat="server" OnClick="btnCancelar_Click" class="btn btn-warning" Text="Cancelar" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnLimpiar" runat="server" OnClick="btnLimpiar_Click" Text="Limpiar"  class="btn btn-default"  />
                                            <asp:HiddenField ID="hdnFlag" runat="server" />
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
