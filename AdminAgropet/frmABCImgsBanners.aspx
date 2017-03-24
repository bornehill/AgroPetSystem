<%@ Page Title="" Language="C#" MasterPageFile="~/Agropet.Master" AutoEventWireup="true" CodeBehind="frmABCImgsBanners.aspx.cs" Inherits="AdminAgropet.ABCImgsBanners" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <asp:MultiView ID="mvwBanners" runat="server">
                <asp:View ID="vwConsulta" runat="server">
                    <div class="col-lg-12">
                        <div class="panel panel-default">
                            <div class="panel-heading">
                                <label>ABC IMAGENES BANNERS</label>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                        <div class="input-group">
                                            <span class="input-group-btn">
                                                <button id="btnBuscar" runat="server" type="button" class="btn btn-success" onserverclick="btnBuscar_ServerClick" tabindex="1">Go!</button>
                                            </span>
                                            <input id="txtBannerBuscar" runat="server" type="text" class="form-control" placeholder="Buscar por..." tabindex="0" />
                                        </div>
                                    </div>
                                    <center>
                                        <button id="btnNuevo" runat="server" class="btn btn-info" onserverclick="btnNuevo_ServerClick" tabindex="2">Nuevo</button>
                                    </center>
                                </div>
                            </div>

                            <div class="panel-body">
                                <asp:GridView ID="gvwConsulta" runat="server" AutoGenerateColumns="false" DataKeyNames="idbanner"
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
                                        <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="ibnEditar" runat="server" Width="20px" ImageUrl="~/Imagenes/Iconos/Editar.PNG"
                                                    CommandName="Editar" CommandArgument='<%# Container.DataItemIndex %>' TabIndex="4" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="idbanner" HeaderText="IdImagenArticulo" Visible="false" />
                                        <asp:BoundField DataField="idusuariocreo" HeaderText="IdArticulo" Visible="false" />
                                        <asp:BoundField DataField="idusuariomodif" HeaderText="IdArticulo" Visible="false" />
                                        <asp:BoundField DataField="descripcion" HeaderText="Descripcion" />
                                        <asp:BoundField DataField="fechacreacion" HeaderText="Fecha Registro" />
                                        <asp:BoundField DataField="fechamodif" HeaderText="Fecha Modificacion" />
                                        <asp:BoundField DataField="fechainiaplica" HeaderText="Fecha Inicio" DataFormatString="{0:dd/MM/yyyy}" />
                                        <asp:BoundField DataField="fechafinaplica" HeaderText="Fecha Fin" DataFormatString="{0:dd/MM/yyyy}" />
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
                                <label>NUEVO BANNER</label>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-6">
                                            <input type="text" id="dtpFechaInicioApp" runat="server" value="" class="form-control" style="width: 170px" readonly="readonly" placeholder="Fecha Inicio Aplicacion" />
                                        </div>
                                        <div class="form-group col-lg-6">
                                            <input type="text" id="dtpFechaFinApp" runat="server" value="" class="form-control" style="width: 170px" readonly="readonly" placeholder="Fecha Fin Aplicacion" />
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-6">
                                            <input id="txtDescripcionBanner" runat="server" class="form-control" type="text" placeholder="Ingresa Descripción Banner" />
                                            <br />
                                        </div>
                                        <center>
                                            <button id="btnGuardar" runat="server" class="btn btn-success" onserverclick="btnGuardar_ServerClick">Guardar</button>
                                            <button id="btnCancelar" runat="server" class="btn btn-info" onserverclick="btnCancelar_ServerClick">Cancelar</button>
                                        </center>
                                        <asp:HiddenField ID="hfIdBannerEditar" runat="server" />
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
                                <label>DETALLE DE IMAGENES DE BANNERS</label>
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
                                            <asp:GridView ID="gvwConsultaDetalle" runat="server" AutoGenerateColumns="false" DataKeyNames="IdDetBanners, IdBanner"
                                                CssClass="table table-striped table-bordered table-hover" OnRowCommand="gvwConsultaDetalle_RowCommand" OnRowDataBound="gvwConsultaDetalle_RowDataBound"
                                                AllowPaging="true" PageSize="10" OnPageIndexChanging="gvwConsultaDetalle_PageIndexChanging">
                                                <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                                <EditRowStyle BackColor="#ffffcc" Height="10px" />
                                                <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                                <Columns>
                                                    <asp:TemplateField ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibnEditarDetalle" runat="server" Width="20px" ImageUrl="~/Imagenes/Iconos/Editar.PNG"
                                                                CommandName="Editar" CommandArgument='<%# Container.DataItemIndex %>' />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:TemplateField ItemStyle-Width="50px">
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="ibnEliminarDetalle" runat="server" Width="20px" ImageUrl="~/Imagenes/Iconos/Basurero.PNG"
                                                                CommandName="Eliminar" CommandArgument='<%# Container.DataItemIndex %>'
                                                                OnClientClick="return confirm('Are you confirm to delete?')" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                    <asp:BoundField DataField="IdDetBanners" HeaderText="IdDetBanners" Visible="false" />
                                                    <asp:BoundField DataField="IdBanner" HeaderText="IdBanner" Visible="false" />
                                                    <asp:BoundField DataField="Orden" HeaderText="Orden" ItemStyle-Width="50px" />
                                                    <asp:BoundField DataField="PathImagen" HeaderText="Imagen" />
                                                    <asp:BoundField DataField="Titulo" HeaderText="Titulo" />
                                                    <asp:BoundField DataField="Subtitulo" HeaderText="Subtitulo" />
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
                                <label>NUEVO DETALLE IMAGEN BANNER</label>
                            </div>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-lg-12">
                                        <div class="form-group col-lg-2">
                                            <label style="text-align: left; width: 100%;">Orden</label>
                                            <asp:TextBox ID="txtOrdenDetalle" runat="server" class="form-control" placeholder="Ingrese Orden..." Width="130px"></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-4">
                                            <%-- <div class="btnera col-lg-12 col-md-12 col-xs-12">
                                                <div class="col-xs-6 col-md-2">--%>
                                            <label style="text-align: left; width: 100%;">Imagen</label>
                                            <div class="input-group">
                                                <label class="input-group-btn">
                                                    <span class="btn btn-primary">Buscar
                                                                <input type="file" name="file" id="inputfile" class="file" runat="server" accept=".jpg,.jpeg,.png,.gif" style="display: none;" />
                                                    </span>
                                                </label>
                                                <input id="txtRutaBannerDetalle" runat="server" type="text" class="form-control" style="width: 300px;" readonly placeholder="Selecciona archivo..." />
                                            </div>
                                            <%--    </div>
                                           </div>--%>
                                        </div>
                                        <div class="form-group col-lg-3">
                                            <label style="text-align: left; width: 100%;">Titulo</label>
                                            <asp:TextBox ID="txtTiltulo" runat="server" class="form-control" placeholder="Ingrese Titulo..."></asp:TextBox>
                                        </div>
                                        <div class="form-group col-lg-3">
                                            <label style="text-align: left; width: 100%;">Subtitulo</label>
                                            <asp:TextBox ID="txtSubtitulo" runat="server" class="form-control" placeholder="Ingrese Subtitulo.."></asp:TextBox>
                                        </div>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-lg-12">
                                        <center>
                                            <asp:Button ID="btnGuardarDetalle" runat="server" class="btn btn-success" Text="Guardar" OnClick="btnGuardarDetalle_Click"></asp:Button>
                                            <asp:Button ID="btnRegresarDetalleNuevo" runat="server" class="btn btn-info" Text="Regresar" OnClick="btnRegresarDetalleNuevo_Click"></asp:Button>
                                        </center>
                                        <asp:HiddenField ID="hfIdBanner" runat="server" />
                                        <asp:HiddenField ID="hfIdBannerDetalle" runat="server" />
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
        $('#ContentPlaceHolder1_dtpFechaInicioApp').datepicker();
        $('#ContentPlaceHolder1_dtpFechaFinApp').datepicker();
    </script>

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
