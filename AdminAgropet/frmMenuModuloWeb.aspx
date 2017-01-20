<%@ Page Title="" Language="C#" MasterPageFile="~/Agropet.Master" AutoEventWireup="true" CodeBehind="frmMenuModuloWeb.aspx.cs" Inherits="AdminAgropet.frmMenuModuloWeb" %>
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
                .Titulos
        {
            font-family: Verdana;
            font-size: 9pt;
            font-weight: bold;
            display: block;
        }
        
        .Datos_Personal
        {
            height: 250px;
            width: 750px;
            border: solid 1px #000000;
            overflow-x: auto;
            overflow-y: scroll;
        }
        
        .Datos_Arbol
        {
            display: inline-block;
            text-align: left;
            height: 250px;
            width: 300px;
            border: solid 1px #000000;
            overflow-x: auto;
            overflow-y: scroll;
        }

        .Datos_Microsip
        {
            display: inline-block;
            text-align: left;
            height: 100%;
            width: 900px;
            border: solid 1px #000000;
            overflow-x: auto;
            overflow-y: scroll;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <center>
     
        <asp:UpdatePanel ID="udpMain" runat="server">
            <ContentTemplate>
                <div id="div_Principal" runat="server">
                    <br />
                    <div id="div_Titulo" class="Titulos">Menú Web</div>
                    <br />
                    <div id="div_FiltrosB" runat="server" style="display: inline-block;" class="Centrado">
                        <table border="0">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lbl_Menu" runat="server" Text="Menu:" EnableViewState="False"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txt_Menu_Buscar" runat="server" Columns="50" MaxLength="95" 
                                        TabIndex="1"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <br />
                    <br />
                    <div id="div_Botones_Buscar" runat="server" style="display: inline-block;" class="Centrado">
                        <asp:Button ID="btn_Buscar" runat="server" Text="Buscar" EnableViewState="false"
                            OnClick="btn_Buscar_Click" CausesValidation="False" TabIndex="2" />
                        <asp:Button ID="btn_Nuevo" runat="server" Text="Nuevo" EnableViewState="false" OnClick="btn_Nuevo_Click"
                            CausesValidation="False" TabIndex="3" />
                    </div>
                    <br />
                    <br />
                    <div class="Datos_Personal Centrado">
                        <asp:GridView ID="grd_Consultas" runat="server" CssClass="mGrid" Width="100%" OnPageIndexChanging="grd_Consultas_PageIndexChanging"
                            OnRowCommand="grd_Consultas_RowCommand" AutoGenerateColumns="False" 
                            DataKeyNames="MenuId,Padre,Activo" EnableModelValidation="True" TabIndex="5">
                            <columns>
                                <asp:TemplateField>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="imgEditItem" runat="server"
                                                ImageUrl="~/Imagenes/ico_editar_chico.gif"
                                                ToolTip="Editar información del usuario." CommandName="Editar"
                                                CommandArgument="<%# ((GridViewRow)Container).RowIndex %>" />

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
                                <asp:BoundField HeaderText="ID" DataField="MenuId" Visible="False" />
                                <asp:BoundField HeaderText="Menu" DataField="Menu" />
                                <asp:BoundField HeaderText="Url" DataField="MenuUrl" />
                                <asp:BoundField HeaderText="Padre" DataField="PadreNom" />
                            </columns>
                        </asp:GridView>
                    </div>
                </div>
                <div id="div_Editar" visible="false" runat="server" >
                    <div id="div_CamposEP" runat="server" 
                        style="display: inline-block; width: 410px; height: 250px;" class="Izquierda" >
                        <br />
                        <div id="div_TitoloEP" class="Titulos">Editar menu</div>
                        <br />
                        <table border="0">
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lbl_Menu_Editar" runat="server" Text="Menu:" 
                                        EnableViewState="False"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txt_Menu_Editar" runat="server" Columns="50" MaxLength="95" 
                                        ValidationGroup="Editar" TabIndex="1"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_txt_MenuEditar" runat="server" ControlToValidate="txt_Menu_Editar"
                                        ErrorMessage="Captura de Menu" ValidationGroup="Editar" Display = "None"></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="vce_Menu" runat="server" 
                                        TargetControlID="rfv_txt_MenuEditar" Width="200px"
                                        HighlightCssClass="MyMsgHighlight" 
                                        WarningIconImageUrl="~\Imagenes\Alertas\warning_msg.png"
                                        CssClass="CustomValidatorCalloutStyle">
                                    </asp:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lbl_MenuUrl_Editar" runat="server" EnableViewState="False" 
                                        Text="Menu Url:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="txt_MenuUrl_Editar" runat="server" Columns="50" MaxLength="245" 
                                        ValidationGroup="Editar" Height="22px" Width="308px" TabIndex="2"></asp:TextBox>
                                    <asp:RequiredFieldValidator ID="rfv_txt_MenuUrlEditar" runat="server" 
                                        ControlToValidate="txt_MenuUrl_Editar" ErrorMessage="Capture el Url" 
                                        ValidationGroup="Editar" Display = "None" ></asp:RequiredFieldValidator>
                                    <asp:ValidatorCalloutExtender ID="vce_Url" runat="server" 
                                        TargetControlID="rfv_txt_MenuUrlEditar" Width="200px"
                                        HighlightCssClass="MyMsgHighlight" 
                                        WarningIconImageUrl="~\Imagenes\Alertas\warning_msg.png"
                                        CssClass="CustomValidatorCalloutStyle">
                                    </asp:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lbl_Padre_Editar" runat="server" EnableViewState="False" 
                                        Text="Padre:"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddl_ListaMenus_Editar" runat="server" 
                                        ValidationGroup="Editar" TabIndex="3">
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator ID="rfv_ddl_ListaMenus" runat="server" 
                                        ControlToValidate="ddl_ListaMenus_Editar" ErrorMessage="Seleccione un elemento" InitialValue="0" 
                                        ValidationGroup="Editar" Display = "None" ></asp:RequiredFieldValidator>
                                
                                    <asp:ValidatorCalloutExtender ID="vce_Padre" runat="server" 
                                        TargetControlID="rfv_ddl_ListaMenus" Width="200px"
                                        HighlightCssClass="MyMsgHighlight" 
                                        WarningIconImageUrl="~\Imagenes\Alertas\warning_msg.png"
                                        CssClass="CustomValidatorCalloutStyle">
                                    </asp:ValidatorCalloutExtender>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Label ID="lbl_Estado_Editar" runat="server" Text="Activo:" 
                                        EnableViewState="False"></asp:Label>
                                </td>
                                <td align="left">
                                    <asp:CheckBox ID="chk_Estado_Editar" runat="server" 
                                        ValidationGroup="Editar" Checked="True" TabIndex="4" />
                                </td>
                            </tr>
                        </table>
                        <br />
                        <div id="div_Botones_Editar" runat="server" style="display: inline-block;" class="Centrado">
                            <asp:Button ID="btn_Guardar" runat="server" Text="Guardar" EnableViewState="false"
                                OnClick="btn_Guardar_Click" ValidationGroup="Editar" TabIndex="5" />
                            <asp:Button ID="btn_Cancelar" runat="server" Text="Cancelar" EnableViewState="false"
                                OnClick="btn_Cancelar_Click" ValidationGroup="Editar" 
                                CausesValidation="False" TabIndex="6" />
                            <input type="hidden" id="txt_Comando" runat="server" />
                        </div>
                    </div>
                    <div id="div_Estrucrura" runat="server" class="Centrado">
                        <br />
                        <div id="div_Titulo_Arbol" class="Titulos">Arbol menus</div>
                        <br />
                        <div id="div_Arboles" runat="server" class="Datos_Arbol">
                            <asp:TreeView ID="tvw_Editar" runat="server" ShowLines="True" TabIndex="7" OnSelectedNodeChanged="tvw_Editar_SelectedNodeChanged" >
                            </asp:TreeView>
                        </div>
                    </div>
                    <div id="div1" runat="server" class="Centrado">
                        <br />
                        <div id="div2" class="Titulos">Asignar Articulos</div>
                        <br />
                        <div id="div3" runat="server" class="Datos_Microsip">
                            <table width="100%">
                                <tr>
                                    <td colspan="3" align="center">
                                        <asp:Button ID="btnMostrarArt" runat="server" Text="Asignar Articulos Microsip" OnClick="btnMostrarArt_Click" /> 
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <br />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" colspan="3">
                                        <asp:Label ID="lblMensaje" runat="server" Text="Puede Filtrar Articulos Utilizando los siguientes controles:" CssClass="Etiqueta"></asp:Label>
                                        &nbsp;&nbsp;
                                        <asp:RadioButtonList ID="rblFiltro" runat="server" CssClass="Etiqueta" AutoPostBack="true" OnSelectedIndexChanged="rblFiltro_SelectedIndexChanged" RepeatDirection="Horizontal" RepeatLayout="Flow">
                                            <asp:ListItem Text="Grupos, Líneas y Artículos" Value="0"></asp:ListItem>
                                            <asp:ListItem Text="Libres Artículos" Value ="1"></asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr>
                                <div id="divGpoLinArt" runat="server" visible="false">
                                <tr>
                                    <td>
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
                                                <asp:CheckBox ID="chkSelItem" runat="server" AutoPostBack="True" OnCheckedChanged="chkSelItem_CheckedChanged"/>
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
                                    </td>
                                    <td>
                                        <asp:GridView ID="grdLinArt" runat="server" SkinID="NoPaginacion" AutoGenerateColumns="False"
                                            AllowPaging="True" HorizontalAlign="Center" DataKeyNames="Id_Lin_Art" OnPageIndexChanging="grdLinArt_PageIndexChanging" CellPadding="4" ForeColor="#333333" GridLines="None"  >
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
                                    </td>
                                    <td>
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
                                    </td>
                                </tr>
                                </div>
                                <div id="divLibresArt" runat="server" visible="false">
                                    <tr>
                                        <td colspan="2" align="center">
                                            <div id="div4" runat="server" class="Datos_Arbol">
                                                <asp:TreeView ID="trvLibArt" runat="server" ShowLines="True" TabIndex="7" OnSelectedNodeChanged="trvLibArt_SelectedNodeChanged"  >
                                                </asp:TreeView>
                                            </div>
                                        </td>
                                        <td >
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
                                                    <asp:CheckBox ID="chkSelItem" runat="server" AutoPostBack="True"  />
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
                                        </td>
                                    </tr>
                                </div>
                                <caption>
                                    <br />
                                    <tr>
                                        <td align="center" colspan="6">
                                            <asp:Button ID="btnAsignar" runat="server" Text="Asignar / Desasignar" OnClick="btnAsignar_Click" />
                                        </td>
                                    </tr>
                                </caption>
                            </table>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hdnFlag" runat="server" />
                <asp:HiddenField ID="hdnIdSeleccionado" runat="server" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </center>
</asp:Content>
