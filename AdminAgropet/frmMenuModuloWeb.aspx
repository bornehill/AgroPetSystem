﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Agropet.Master" AutoEventWireup="true" CodeBehind="frmMenuModuloWeb.aspx.cs" Inherits="AdminAgropet.frmMenuModuloWeb" %>

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
                                <asp:TemplateField ItemStyle-Width="50px">
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
                                <asp:TemplateField Visible="false">
                                    <HeaderTemplate>
                                        <asp:CheckBox ID="chkSelTodoHdr" CssClass="Visible" runat="server" ToolTip="Seleccionar / Des-seleccionar todos" />
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <%--<div class="squaredOne">--%>
                                        <asp:CheckBox ID="chkSelItem" runat="server" CssClass="Visible" />
                                        <%-- <label id="lblConsulta" for="ContentPlaceHolder1_grd_Consultas_chkSelItem_<%# Container.DataItemIndex %>"></label>
                                        </div>--%>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField HeaderText="ID" DataField="MenuId" Visible="False" />
                                <asp:BoundField HeaderText="Menu" DataField="Menu" />
                                <asp:BoundField HeaderText="Url" DataField="MenuUrl" />
                                <asp:BoundField HeaderText="Padre" DataField="PadreNom" />
                            </Columns>
                            <EmptyDataTemplate>Sin datos</EmptyDataTemplate>
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
                            <div class="col-xs-2 col-sm-2 col-md-2 col-lg-2" style="text-align: left">
                                <label style="text-align: left; width: 100%; font-size: small">Activo:</label>
                                <div class="btn btn-default" style="height: 34px">
                                    <div class="material-switch pull-right" style="padding-top: 10px">
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
                                    NodeStyle-CssClass="nodoTreeView" SelectedNodeStyle-BackColor="LightSkyBlue"
                                    EnableViewState="true" ExpandImageUrl="~/Images/arrow_right.png" CollapseImageUrl="~/Images/bullet.png" ImageSet="Custom">
                                </asp:TreeView>
                            </div>
                        </div>

                        <div class="panel panel-default">
                            <div class="panel-heading">Asignar Articulos</div>
                            <div class="panel-body">
                              <%--  <div class="alert alert-info alert-dismissible" role="alert">
                                    <button type="button" class="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">&times;</span></button>
                                    <strong class="glyphicon glyphicon-info-sign" style="float: left; font-size: 15px; margin-right: 9px;"></strong>
                                    <span>Puede Filtrar Articulos Utilizando los siguientes controles:</span>
                                </div>--%>
                                <div class="row">
                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                        <div class="funkyradio funkyradio-warning" style="width: 220px;">
                                            <asp:CheckBox id="rdGrupoLineaArticulo" runat="server" AutoPostBack="true" OnCheckedChanged="rblFiltro_SelectedIndexChanged"></asp:CheckBox>
                                           <%-- <input type="radio" name="radio" id="rdGrupoLineaArticulo" runat="server" onserverchange="rblFiltro_SelectedIndexChanged" />--%>
                                            <label for="ContentPlaceHolder1_rdGrupoLineaArticulo" style="font-size: 13px">Grupos, Líneas y Artículos</label>
                                        </div>
                                    </div>

                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                        <div class="funkyradio funkyradio-warning" style="width: 200px">
                                            <asp:CheckBox id="rdLibreArticulo" runat="server" AutoPostBack="true" OnCheckedChanged="rblFiltro_SelectedIndexChanged"></asp:CheckBox>
                                           <%-- <input type="radio" name="radio" id="rdLibreArticulo" runat="server" onserverchange="rblFiltro_SelectedIndexChanged" />--%>
                                            <label for="ContentPlaceHolder1_rdLibreArticulo" style="font-size: 13px">Libres Artículos</label>
                                        </div>
                                    </div>

                                    <div class="col-xs-3 col-sm-3 col-md-3 col-lg-3">
                                        <button id="btnAsignar" type="button" runat="server" class="btn btn-info" onserverclick="btnAsignar_Click">Asignar / Desasignar</button> 
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="divGpoLinArt" runat="server" visible="false" class="panel-body">
                            <%--<div class="panel-heading">Asignar Articulos</div>--%>
                            <div class="panel-body">
                                <div class="row">
                                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4" style="overflow-y:scroll; overflow-x:hidden; max-height:500px;">
                                       <div></div>
                                        <asp:GridView ID="grdGposLin" runat="server" AutoGenerateColumns="False"
                                            DataKeyNames="grupo_linea_id" OnPageIndexChanging="grdGposLin_PageIndexChanging"
                                            CssClass="table table-striped table-bordered table-hover" AllowPaging="false" PageSize="10">
                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" Height="10px" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <div class="squaredOne">
                                                            <asp:CheckBox ID="chkGposLin" CssClass="Visible" runat="server" AutoPostBack="True" OnCheckedChanged="chkGposLin_CheckedChanged" />
                                                        </div>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelItem" runat="server" class="Visible" AutoPostBack="True" OnCheckedChanged="chkSelItem_CheckedChanged" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Nombre" HeaderText="Grupos Lineas" />
                                            </Columns>
                                            <EmptyDataTemplate>Sin datos</EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4" style="overflow-y:scroll; overflow-x:hidden; max-height:500px;">
                                        <asp:GridView ID="grdLinArt" runat="server" AutoGenerateColumns="False" DataKeyNames="linea_articulo_id" OnPageIndexChanging="grdLinArt_PageIndexChanging"
                                            CssClass="table table-striped table-bordered table-hover" AllowPaging="false" PageSize="10">
                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" Height="10px" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                    <HeaderTemplate>
                                                        <asp:CheckBox ID="chkLinArt" CssClass="Visible" runat="server" AutoPostBack="true" ToolTip="Seleccionar / Des-seleccionar todos" OnCheckedChanged="chkLinArt_CheckedChanged" />
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <%--<div class="squaredOne">--%>
                                                        <asp:CheckBox ID="chkSelItem" runat="server" class="Visible" AutoPostBack="True" OnCheckedChanged="chkSelItem_CheckedChanged1" />
                                                        <%-- <label for="ContentPlaceHolder1_grdLinArt_chkSelItem_<%# Container.DataItemIndex %>"></label>
                                                        </div>--%>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Nombre" HeaderText="Lineas Articulos" />
                                            </Columns>
                                            <EmptyDataTemplate>Sin datos</EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                    <div class="col-xs-4 col-sm-4 col-md-4 col-lg-4" style="overflow-y:scroll; overflow-x:hidden; max-height:500px;">
                                        <asp:GridView ID="grdArt" runat="server" AutoGenerateColumns="False" DataKeyNames="articulo_id" OnDataBound="grdArt_DataBound" OnPageIndexChanging="grdArt_PageIndexChanging"
                                            CssClass="table table-striped table-bordered table-hover" AllowPaging="false" PageSize="10">
                                            <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White" />
                                            <EditRowStyle BackColor="#ffffcc" Height="10px" />
                                            <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                            <AlternatingRowStyle BackColor="White" />
                                            <Columns>
                                                <asp:TemplateField>
                                                     <HeaderTemplate>
                                                        <asp:CheckBox ID="chkArt" runat="server" OnCheckedChanged="chkArt_CheckedChanged" AutoPostBack="true"
                                                          CssClass="Visible" ToolTip="Seleccionar / Des-seleccionar todos"  Checked="true"/>
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <asp:CheckBox ID="chkSelItem" runat="server" class="Visible" />
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                                <asp:BoundField DataField="Nombre" HeaderText="Articulos" />
                                            </Columns>
                                            <EmptyDataTemplate>Sin datos</EmptyDataTemplate>
                                        </asp:GridView>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div id="divLibresArt" runat="server" visible="false" class="panel">
                            <div class="panel-body">
                                <asp:TreeView CssClass="treeview" ID="trvLibArt" runat="server" ShowLines="True" TabIndex="7" OnSelectedNodeChanged="trvLibArt_SelectedNodeChanged"
                                    NodeStyle-CssClass="nodoTreeView"  SelectedNodeStyle-BackColor="LightSkyBlue"
                                    EnableViewState="true" ExpandImageUrl="~/Images/arrow_right.png" CollapseImageUrl="~/Images/bullet.png" ImageSet="Custom">
                                </asp:TreeView>
                            </div>
                            <div class="panel-body" style="overflow-y:scroll; overflow-x:hidden; max-height:300px; ">
                                <asp:GridView ID="grdLibArt" runat="server" AutoGenerateColumns="False" DataKeyNames="articulo_id"
                                    CssClass="table table-striped table-bordered table-hover" AllowPaging="false" PageSize="10">
                                    <HeaderStyle BackColor="#337ab7" Font-Bold="True" ForeColor="White"/>
                                    <EditRowStyle BackColor="#ffffcc" Height="10px" />
                                    <EmptyDataRowStyle ForeColor="Red" CssClass="table table-bordered" />
                                    <AlternatingRowStyle BackColor="White" />
                                    <Columns>
                                        <asp:TemplateField ItemStyle-Width="50px" >
                                            <HeaderTemplate>
                                                <asp:CheckBox ID="chkSelTodoHdr" runat="server" class="Visible" OnCheckedChanged="chkSelTodoHdr_CheckedChanged"
                                                    AutoPostBack="true" ToolTip="Seleccionar / Des-seleccionar todos" />
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <%--<div class="squaredOne">--%>
                                                <asp:CheckBox ID="chkSelItem" runat="server" class="Visible" />
                                                <%-- <label for="ContentPlaceHolder1_grdLibArt_chkSelItem_<%# Container.DataItemIndex %>"></label>
                                                </div>--%>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:BoundField DataField="Nombre" HeaderText="Articulos" />
                                    </Columns>
                                    <EmptyDataTemplate>Sin datos</EmptyDataTemplate>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>


            <asp:HiddenField ID="hdnFlag" runat="server" />
            <asp:HiddenField ID="hdnIdSeleccionado" runat="server" />
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="rdLibreArticulo"  />
            <asp:AsyncPostBackTrigger ControlID="rdGrupoLineaArticulo"  />
            <asp:AsyncPostBackTrigger ControlID="rdLibreArticulo"  />
            <asp:AsyncPostBackTrigger ControlID="btn_Buscar"  />
            <asp:AsyncPostBackTrigger ControlID="btn_Nuevo"   />
            <asp:AsyncPostBackTrigger ControlID="btn_Guardar" />
            <asp:AsyncPostBackTrigger ControlID="btn_Cancelar"  />
            <asp:AsyncPostBackTrigger ControlID="grd_Consultas"  />
            <asp:AsyncPostBackTrigger ControlID="tvw_Editar" />
            <asp:AsyncPostBackTrigger ControlID="trvLibArt" />
            <asp:AsyncPostBackTrigger ControlID="grdGposLin" />
            <asp:AsyncPostBackTrigger ControlID="grdLinArt" />
            <asp:AsyncPostBackTrigger ControlID="grdArt"/>
            <asp:AsyncPostBackTrigger ControlID="grdLibArt" />
            <asp:AsyncPostBackTrigger ControlID="btnAsignar"/>
        </Triggers>
    </asp:UpdatePanel>
</asp:Content>
