<%@ Page Title="" Language="C#" MasterPageFile="~/Agropet.Master" AutoEventWireup="true" CodeBehind="frmMenuModuloWeb.aspx.cs" Inherits="AdminAgropet.frmMenuModuloWeb" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <script type="text/javascript">
        $(document).ready(function () {
            $("input[type='radio']").click(function () {
                __doPostBack();
            });
        });
    </script>
    <br />
    <asp:UpdatePanel ID="udpMain" runat="server">
        <ContentTemplate>

            <div id="div_Principal" runat="server" class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <label>Menú Web</label>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                <div class="input-group">
                                    <span class="input-group-btn">
                                        <button id="btn_Buscar" runat="server" type="button" class="btn btn-success" onserverclick="btn_Buscar_Click" tabindex="2">Go!</button>
                                    </span>
                                    <input id="txt_Menu_Buscar" runat="server" type="text" class="form-control" placeholder="Buscar por..." tabindex="1" />
                                </div>
                            </div>
                            <div style="text-align: left" class="col-xs-6 col-sm-6 col-md-6 col-lg-6">
                                <button id="btn_Nuevo" runat="server" type="button" class="btn btn-info" onserverclick="btn_Nuevo_Click" tabindex="3">Nuevo</button>
                            </div>
                        </div>
                    </div>

                    <div class="panel-body">
                        <asp:GridView ID="grd_Consultas" runat="server" AutoGenerateColumns="false" DataKeyNames="MenuId,Padre,Activo" EnableModelValidation="True" TabIndex="5"
                            OnRowCommand="grd_Consultas_RowCommand" OnPageIndexChanging="grd_Consultas_PageIndexChanging"
                            CssClass="table table-striped table-bordered table-hover" AllowPaging="true" PageSize="10">
                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                            <EditRowStyle BackColor="#ffffcc" Height="10px" />
                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                            <Columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditItem" runat="server" Width="20px" ImageUrl="~/Imagenes/Iconos/Editar.PNG"
                                            CommandName="Editar" CommandArgument='<%# Container.DataItemIndex %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <%--  <asp:TemplateField>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="imgEditItem" runat="server"
                                                        ImageUrl="~/Imagenes/ico_editar_chico.gif"
                                                        ToolTip="Editar información del usuario." CommandName="Editar"
                                                        CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />
                                            </ItemTemplate>
                                        </asp:TemplateField>--%>
                                <asp:TemplateField>
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelTodoHdr" runat="server"
                                            ToolTip="Seleccionar / Des-seleccionar todos" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <asp:CheckBox ID="chkSelItem" runat="server" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="ID" DataField="MenuId" Visible="False" />
                                <asp:BoundField HeaderText="Menu" DataField="Menu" />
                                <asp:BoundField HeaderText="Url" DataField="MenuUrl" />
                                <asp:BoundField HeaderText="Padre" DataField="PadreNom" />
                            </Columns>
                        </asp:GridView>
                    </div>
                </div>
            </div>

            <div id="div_Editar" runat="server" visible="false" class="col-lg-12">
                <div class="panel panel-default">
                    <div class="panel-heading">
                        <label>Editar menu</label>
                    </div>
                    <div class="panel-body">
                        <div class="row">
                            <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                                <label style="text-align: left; width: 100%; font-size: small">Menu</label>
                                <input id="txt_Menu_Editar" runat="server" class="form-control" type="text" placeholder="Ingresa Menú ..." />
                            </div>
                            <div class="col-xs-8 col-sm-8 col-md-8 col-lg-8">
                                <label style="text-align: left; width: 100%; font-size: small">URL Menu</label>
                                <input id="txt_MenuUrl_Editar" runat="server" class="form-control" type="text" placeholder="Ingresa URL del Menu" />
                            </div>
                        </div>
                        <div style="padding-bottom: 5px"></div>
                        <div class="row">
                            <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                                <label style="text-align: left; width: 100%; font-size: small">Padre:</label>
                                <asp:DropDownList ID="ddl_ListaMenus_Editar" class="form-control" runat="server" ValidationGroup="guarda" Width="100%"></asp:DropDownList>
                                <asp:ListSearchExtender ID="lseMenus" runat="server" TargetControlID="ddl_ListaMenus_Editar"
                                            PromptText="Busqueda por.." QueryPattern="Contains">
                                </asp:ListSearchExtender>
                            </div>
                            <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2">
                                <label style="text-align: left; width: 100%; font-size: small">Activo:</label>
                                <div class="btn btn-default">
                                    <div class="material-switch pull-right">
                                        <input id="chk_Estado_Editar" runat="server" type="checkbox" />
                                        <label for="ContentPlaceHolder1_chk_Estado_Editar" style="text-align: left" class="label-success"></label>
                                    </div>
                                </div>

                            </div>
                            <div class="col-xs-6 col-sm-6 col-md-6 col-lg-6" style="align-content: center">
                                <br />
                                <div style="padding-bottom: 5px"></div>
                                <button id="btn_Guardar" type="button" runat="server" class="btn btn-success" onserverclick="btn_Guardar_Click">Guardar</button>
                                <button id="btn_Cancelar" type="button" runat="server" class="btn btn-default" onserverclick="btn_Cancelar_Click">Cancelar</button>
                                <input type="hidden" id="txt_Comando" runat="server" />
                            </div>
                        </div>

                        <div style="padding-bottom: 15px"></div>

                        <div class="panel panel-default">
                            <div class="panel-heading">Arbol Menus</div>
                            <div class="panel-body">

                                <asp:TreeView CssClass="treeview" ID="tvw_Editar" runat="server" ShowLines="True" TabIndex="7" OnSelectedNodeChanged="tvw_Editar_SelectedNodeChanged"
                                    NodeStyle-CssClass="nodoTreeView"
                                    EnableViewState="true" ExpandImageUrl="~/Images/arrow_right.png" CollapseImageUrl="~/Images/bullet.png" ImageSet="Custom">
                                </asp:TreeView>
                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-heading">Asignar Articulos</div>
                            <div class="panel-body">
                                <div class="alert alert-info alert-dismissible" role="alert">
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <strong class="glyphicon glyphicon-info-sign" style="float: left; font-size: 15px; margin-right: 9px;"></strong>
                                    <span>Puede Filtrar Articulos Utilizando los siguientes controles:</span>
                                </div>
                                <div class="row">
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                        <button id="btnMostrarArt" type="button" runat="server" class="btn btn-info" onserverclick="btnMostrarArt_Click">Asignar Articulos Microsip</button>
                                    </div>
                                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4">
                                        <div class="funkyradio funkyradio-warning" style="width: 220px;">
                                            <input type="radio" name="radio" id="rdGrupoLineaArticulo" runat="server" onserverchange="rblFiltro_SelectedIndexChanged" />
                                            <label for="ContentPlaceHolder1_rdGrupoLineaArticulo" style="font-size: 13px">Grupos, Líneas y Artículos</label>
                                        </div>
                                    </div>

                                    <div class="col-xs-5 col-sm-5 col-md-5 col-lg-5">
                                        <div class="funkyradio funkyradio-warning" style="width: 200px">
                                            <input type="radio" name="radio" id="rdLibreArticulo" runat="server" onserverchange="rblFiltro_SelectedIndexChanged" />
                                            <label for="ContentPlaceHolder1_rdLibreArticulo" style="font-size: 13px">Libres Artículos</label>
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>

                        <div id="divGpoLinArt" runat="server" visible="false" class="panel-body">
                            <%--<div class="panel-heading">Asignar Articulos</div>--%>
                            <div class="panel-body">
                                <div class="row">
                                    <asp:GridView ID="grdGposLin" runat="server" SkinID="NoPaginacion" AutoGenerateColumns="False"
                                        AllowPaging="True" HorizontalAlign="Center"
                                        DataKeyNames="Id_Gpo_Lin" OnPageIndexChanging="grdGposLin_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelTodoHdr" runat="server"
                                                        ToolTip="Seleccionar / Des-seleccionar todos" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelItem" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelItem_CheckedChanged" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Nombre" HeaderText="Grupos Lineas" />
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                                    </asp:GridView>

                                </div>
                                <div class="row">
                                    <asp:GridView ID="grdLinArt" runat="server" SkinID="NoPaginacion" AutoGenerateColumns="False"
                                        AllowPaging="True" HorizontalAlign="Center" DataKeyNames="Id_Lin_Art" OnPageIndexChanging="grdLinArt_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelTodoHdr" runat="server"
                                                        ToolTip="Seleccionar / Des-seleccionar todos" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelItem" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelItem_CheckedChanged1" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Nombre" HeaderText="Lineas Articulos" />
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                                    </asp:GridView>

                                </div>

                                <div class="row">
                                    <asp:GridView ID="grdArt" runat="server" SkinID="NoPaginacion" AutoGenerateColumns="False"
                                        AllowPaging="True" HorizontalAlign="Center" DataKeyNames="Id_Art" OnDataBound="grdArt_DataBound" OnPageIndexChanging="grdArt_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None">
                                        <AlternatingRowStyle BackColor="White" />
                                        <Columns>
                                            <asp:TemplateField>
                                                <HeaderTemplate>
                                                    <asp:CheckBox ID="chkSelTodoHdr" runat="server"
                                                        ToolTip="Seleccionar / Des-seleccionar todos" />
                                                </HeaderTemplate>
                                                <ItemTemplate>
                                                    <asp:CheckBox ID="chkSelItem" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelItem_CheckedChanged2" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="Nombre" HeaderText="Articulos" />
                                        </Columns>
                                        <EditRowStyle BackColor="#7C6F57" />
                                        <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="#E3EAEB" />
                                        <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                        <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                        <SortedAscendingHeaderStyle BackColor="#246B61" />
                                        <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                        <SortedDescendingHeaderStyle BackColor="#15524A" />
                                    </asp:GridView>
                                </div>

                            </div>
                        </div>

                        <div id="divLibresArt" runat="server" visible="false" class="panel panel-default">
                            <%--<div class="panel-heading">Asignar / Desasignar</div>--%>
                            <div class="panel-body">
                                <asp:TreeView CssClass="treeview" ID="trvLibArt" runat="server" ShowLines="True" TabIndex="7" OnSelectedNodeChanged="trvLibArt_SelectedNodeChanged"
                                    NodeStyle-CssClass="nodoTreeView"
                                    EnableViewState="true" ExpandImageUrl="~/Images/arrow_right.png" CollapseImageUrl="~/Images/bullet.png" ImageSet="Custom">
                                </asp:TreeView>
                            </div>
                            <div class="panel-body">
                                <asp:GridView ID="grdLibArt" runat="server" SkinID="NoPaginacion" AutoGenerateColumns="False"
                                    AllowPaging="True" HorizontalAlign="Center" DataKeyNames="Id_Art" CellPadding="4" ForeColor="#333333" GridLines="None">
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField>
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelTodoHdr" runat="server"
                                                    ToolTip="Seleccionar / Des-seleccionar todos" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkSelItem" runat="server" AutoPostBack="True" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Nombre" HeaderText="Articulos" />
                                    </Columns>
                                    <EditRowStyle BackColor="#7C6F57" />
                                    <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
                                    <RowStyle BackColor="#E3EAEB" />
                                    <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
                                    <SortedAscendingCellStyle BackColor="#F8FAFA" />
                                    <SortedAscendingHeaderStyle BackColor="#246B61" />
                                    <SortedDescendingCellStyle BackColor="#D4DFE1" />
                                    <SortedDescendingHeaderStyle BackColor="#15524A" />
                                </asp:GridView>
                            </div>
                            <div class="panel-body">
                                <button id="btnAsignar" type="button" runat="server" class="btn btn-info" onserverclick="btnAsignar_Click">Asignar / Desasignar</button>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <asp:HiddenField ID="hdnFlag" runat="server" />
            <asp:HiddenField ID="hdnIdSeleccionado" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="btn_Buscar" />
            <asp:PostBackTrigger ControlID="btn_Nuevo" />
            <asp:PostBackTrigger ControlID="btn_Guardar" />
            <asp:PostBackTrigger ControlID="grd_Consultas" />
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
