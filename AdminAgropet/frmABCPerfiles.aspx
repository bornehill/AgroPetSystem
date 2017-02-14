<%@ Page Title="Catálogo de Perfiles" Language="C#" MasterPageFile="~/Agropet.Master" AutoEventWireup="true" CodeBehind="frmABCPerfiles.aspx.cs" Inherits="AdminAgropet.frmABCPerfiles" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ControlWEB/Comun/wucDropDownEntidadNegocio.ascx" TagName="wucDropDownEntidadNegocio" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <%--<script type="text/javascript">
        $(function () {
            $(document).tooltip({ track: true });
        });
    </script>--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>

            <asp:MultiView ID="mvwPerfiles" runat="server">
                <asp:View ID="vwConsulta" runat="server">
                    <div class="col-lg-12">
                        <br />
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <label>BUSQUEDA DE PERFIL</label>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                        <div class="form-group col-lg-6">
                                            <label style="text-align: left; width: 100%;">Perfil</label>
                                            <uc1:wucDropDownEntidadNegocio ID="ddluc_Perfil" runat="server" Width="250px" />
                                        </div>
                                    </div>
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                        <label style="text-align: left; width: 100%;">Estatus</label>
                                        <asp:DropDownList ID="SelEstatus" runat="server" AutoPostBack="false" class="form-control">
                                            <asp:ListItem Text="Seleccione..." Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No Activo" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6" style="text-align: center">
                                        <br />
                                        <button id="btnBuscar" runat="server" type="submit" class="btn btn-success" onserverclick="btnBuscar_Click">Buscar</button>
                                        &nbsp;&nbsp;&nbsp;&nbsp;
                                        <button id="btnNuevo" runat="server" type="submit" class="btn btn-info" onserverclick="btnNuevo_Click">Nuevo</button>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <center>
                                            <br />
                                            <asp:GridView ID="grdPerfiles" runat="server" AutoGenerateColumns="false"
                                                CssClass="table table-striped table-bordered table-hover"
                                                OnRowCommand="grdPerfiles_RowCommand" OnRowDataBound="grdPerfiles_RowDataBound"
                                                AllowPaging="true" PageSize="10" OnPageIndexChanging="grdPerfiles_PageIndexChanging">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" Height="10px" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgEditItem" runat="server" Width="20px" ImageUrl="~/Imagenes/Iconos/Editar.PNG"
                                                                CommandName="Editar" CommandArgument='<%# ((GridViewRow)Container).RowIndex %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-HorizontalAlign="Center" Visible="false">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelItem" runat="server" CssClass="Visible" />
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
                                                <EmptyDataTemplate>Sin datos</EmptyDataTemplate>
                                            </asp:GridView>
                                        </center>
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
                                <label id="lblTituloNuevo" runat="server">PERFIL</label>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-4">
                                        <label style="text-align: left; width: 100%;">Id Perfil:</label>
                                        <input type="text" id="txtIdPerfil" runat="server" class="form-control" readonly="readonly" placeholder="Info: ID del perfil" />
                                    </div>
                                    <div class="col-lg-4">
                                        <label style="text-align: left; width: 100%;">Nombre Perfil:</label>
                                        <input type="text" id="txtPerfil" runat="server" class="form-control" placeholder="Ingresa Nombre Perfil...." maxlength="100" />
                                    </div>
                                    <div class="col-lg-4">
                                        <label style="text-align: left; width: 100%;">Fecha Creación:</label>
                                        <input type="text" id="txtFechaCreacion" runat="server" class="form-control" readonly="readonly" placeholder="Info Fecha creacion...." />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4">
                                        <label style="text-align: left; width: 100%;">Creado por: </label>
                                        <input type="text" id="txtUserCreo" runat="server" class="form-control" readonly="readonly" placeholder="Usuario que creo el perfil...." />
                                    </div>
                                    <div class="col-lg-4">
                                        <label style="text-align: left; width: 100%;">Fecha Modificación: </label>
                                        <input type="text" id="txtFechaModif" runat="server" class="form-control" readonly="readonly" placeholder="Fecha de Últ. Modificación al perfil...." />
                                    </div>
                                    <div class="col-lg-4">
                                        <label style="text-align: left; width: 100%;">Usuario Modificó:</label>
                                        <input type="text" id="txtUserModifico" runat="server" class="form-control" readonly="readonly" placeholder="Usuario que realizo la última modificación...." />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-4">
                                        <label style="text-align: left; width: 100%;">Estatus:</label>
                                        <asp:DropDownList ID="ddlEstadoABC" runat="server" AutoPostBack="false" class="form-control">
                                            <asp:ListItem Text="Seleccione..." Value="-1"></asp:ListItem>
                                            <asp:ListItem Text="Activo" Value="1"></asp:ListItem>
                                            <asp:ListItem Text="No Activo" Value="0"></asp:ListItem>
                                        </asp:DropDownList>
                                    </div>
                                    <div class="col-lg-4">
                                    </div>
                                    <div class="col-lg-4">
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">

                                        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" class="btn btn-success" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" OnClick="btnCancelar_Click" class="btn btn-warning" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnLimpiar" runat="server" Text="Limpiar" OnClick="btnLimpiar_Click" class="btn btn-default" />
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
