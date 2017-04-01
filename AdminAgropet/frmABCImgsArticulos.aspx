<%@ Page Title="" Language="C#" MasterPageFile="~/Agropet.Master" AutoEventWireup="true" CodeBehind="frmABCImgsArticulos.aspx.cs" Inherits="AdminAgropet.ABCImgsArticulos" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/ControlWEB/Comun/wucDropDownEntidadNegocio.ascx" TagName="wucDropDownEntidadNegocio" TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="mvwArticulos" runat="server">
                <asp:View ID="vwConsulta" runat="server">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <label>ABC IMAGENES ARTICULOS</label>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                        <div class="input-group">
                                            <span class="input-group-btn">
                                                <button id="btnBuscar" runat="server" type="button" class="btn btn-success" onserverclick="btnBuscar_Click" tabindex="1">Go!</button>
                                            </span>
                                            <input id="txtArticuloBuscar" runat="server" type="text" class="form-control" placeholder="Buscar por..." tabindex="0" />
                                        </div>
                                    </div>
                                    <center>
                                        <button id="btnNuevo" runat="server" class="btn btn-info" onserverclick="btnNuevo_Click" tabindex="2">Nuevo</button>
                                    </center>
                                </div>
                            </div>

                            <div class="panel-body">
                                <asp:GridView ID="gvwConsulta" runat="server" AutoGenerateColumns="false" DataKeyNames="IdImagenArticulo"
                                    OnRowCommand="gvwConsulta_RowCommand" OnRowDataBound="gvwConsulta_RowDataBound"
                                    CssClass="table table-striped table-bordered table-hover" AllowPaging="true" PageSize="10" OnPageIndexChanging="gvwConsulta_PageIndexChanging"
                                    >
                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                    <EditRowStyle BackColor="#ffffcc" Height="10px" />
                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibnDetalle" runat="server" Width="20px" ImageUrl="~/Imagenes/Iconos/Info2.PNG"
                                                    CommandName="Detalles" CommandArgument='<%# Container.DataItemIndex %>' TabIndex="3" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="IdImagenArticulo" HeaderText="IdImagenArticulo" Visible="false" />
                                        <asp:BoundField DataField="IdArticulo" HeaderText="IdArticulo" Visible="false" />
                                        <asp:BoundField DataField="NombreArticulo" HeaderText="Articulo" />
                                        <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Registro" />
                                        <asp:BoundField DataField="FechaModificacion" HeaderText="Fecha de Modificacion" />
                                        <asp:BoundField DataField="IdUsuarioCreo" HeaderText="IdUsuarioCreo" Visible="false" />
                                        <asp:BoundField DataField="IdUsuarioModifico" HeaderText="IdUsuarioModifico" Visible="false" />
                                    </Columns>
                                    <EmptyDataTemplate>Sin datos</EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </asp:View>

                <asp:View ID="vwNuevo" runat="server">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <label>NUEVA IMAGEN PARA ARTICULOS</label>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <%-- <form role="form">--%>
                                        <div class="form-group col-lg-6">
                                            <asp:DropDownList ID="ddlArticulos" class="form-control" runat="server" ValidationGroup="guarda" Width="300px"></asp:DropDownList>
                                            <asp:ListSearchExtender ID="lseArticulos" runat="server" TargetControlID="ddlArticulos"
                                                PromptText="Busqueda por.." QueryPattern="Contains">
                                            </asp:ListSearchExtender>
                                        </div>
                                        <center>
                                            <button id="btnGuardar" runat="server" class="btn btn-success" onserverclick="btnGuardar_Click" validationgroup="guarda">Guardar</button>
                                            <button id="btnCancelar" runat="server" class="btn btn-info" onserverclick="btnCancelar_Click">Cancelar</button>
                                        </center>
                                        <%-- </form>--%>
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
                                <label>DETALLE DE IMAGENES DE ARTICULOS</label>
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
                                            <asp:GridView ID="gvwConsultaDetalle" runat="server" AutoGenerateColumns="false" DataKeyNames="IdDetImagenArticulo, IdImagenArticulo"
                                                AllowPaging="true" PageSize="10" OnPageIndexChanging="gvwConsultaDetalle_PageIndexChanging" CssClass="table table-striped table-bordered table-hover"
                                                OnRowCommand="gvwConsultaDetalle_RowCommand" OnRowDataBound="gvwConsultaDetalle_RowDataBound">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" Height="10px" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                <Columns>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibnEditarDetalle" runat="server" Width="20px" ImageUrl="~/Imagenes/Iconos/Editar.PNG"
                                                                CommandName="Editar" CommandArgument='<%# Container.DataItemIndex %>'  />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibnEliminarDetalle" runat="server" Width="20px" ImageUrl="~/Imagenes/Iconos/Basurero.PNG"
                                                                 CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>'
                                                                 OnClientClick="return confirm('Are you confirm to delete?')"  />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="IdDetImagenArticulo" HeaderText="IdDetImagenArticulo" Visible="false" />
                                                    <asp:BoundField DataField="IdImagenArticulo" HeaderText="IdImagenArticulo" Visible="false" />
                                                    <asp:BoundField DataField="PathImagen" HeaderText="Fecha de Registro" />
                                                    <asp:BoundField DataField="FechaRegistro" HeaderText="Fecha de Registro" />
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

                <asp:View ID="vwDetalleNuevo" runat="server">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <label>NUEVA DETALLE ARTICULO</label>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-6">
                                            <label style="text-align: left; width: 100%;">Imagen</label>
                                            <div class="input-group">
                                                <label class="input-group-btn">
                                                    <span class="btn btn-primary">Buscar
                                                         <input type="file" name="file" id="inputfile" class="file" runat="server" accept=".jpg,.jpeg,.png,.gif" style="display: none;" />
                                                    </span>
                                                </label>
                                                <input id="txtImagenArticulo" runat="server" type="text" class="form-control" style="width: 300px;" readonly placeholder="Selecciona archivo..." />
                                            </div>
                                        </div>
                                        <div class="form-group col-lg-6">
                                            <br />
                                            <asp:Button ID="btnGuardarDetalle" runat="server" class="btn btn-success" Text="Guardar" OnClick="btnGuardarDetalle_Click" ValidationGroup="guarda"></asp:Button>
                                            <asp:Button ID="btnRegresarDetalleNuevo" runat="server" class="btn btn-info" Text="Regresar" OnClick="btnRegresarDetalleNuevo_Click"></asp:Button>
                                        </div>
                                        <asp:HiddenField ID="hfIdImagenArticulo" runat="server" />
                                        <asp:HiddenField ID="hfIdImagenArticuloDetalle" runat="server" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </asp:View>

            </asp:MultiView>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btnNuevo" />
            <asp:PostBackTrigger ControlID="btnGuardar" />
            <asp:PostBackTrigger ControlID="gvwConsulta" />
            <asp:PostBackTrigger ControlID="btnNuevoDetalle" />
            <asp:PostBackTrigger ControlID="btnGuardarDetalle" />
            <asp:PostBackTrigger ControlID="gvwConsultaDetalle" />
        </Triggers>
    </asp:UpdatePanel>

    <script type="text/javascript">
        $(function () {

            // We can attach the `fileselect` event to all file inputs on the page
            $(document).on('change', ':file', function () {
                var input = $(this),
                    numFiles = input.get(0).files ? input.get(0).files.length : 1,
                    label = input.val().replace(/\\/g, '/').replace(/.*\//, '');
                input.trigger('fileselect', [numFiles, label]);
            });

            // We can watch for our custom `fileselect` event like this
            $(document).ready(function () {
                $(':file').on('fileselect', function (event, numFiles, label) {

                    var input = $(this).parents('.input-group').find(':text'),
                        log = numFiles > 1 ? numFiles + ' files selected' : label;

                    if (input.length) {
                        input.val(log);
                    } else {
                        if (log) alert(log);
                    }
                });
            });
        });

    </script>
</asp:Content>
