<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="wucDropDownEntidadNegocio.ascx.cs" Inherits="AdminAgropet.ControlWEB.Comun.wucDropDownEntidadNegocio" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<asp:Panel ID="pnlDescripcion" runat="server" CssClass="bloqueIzq">
    <asp:Label ID="lblDescripcion" runat="server" CssClass="etiqueta">
    </asp:Label>
</asp:Panel>
<asp:Panel ID="pnlDropDown" runat="server" CssClass="bloqueIzq" Width="100">    
    <input type="text" id="txtComboSearch" runat="server" value="" style="display:none;position:absolute"/>
    <asp:DropDownList ID="ddlEntidadNegocio" runat="server" AutoPostBack="false" CssClass="ddlGeneral" 
        onchange="f_throwTextSearcher(this);">
    </asp:DropDownList>
</asp:Panel>
<asp:ListSearchExtender ID="ddlEnNgListSearchExtender" runat="server" Enabled="true"
    TargetControlID="ddlEntidadNegocio" PromptCssClass="cuadroTextoBuscar"
    PromptText="Escriba para buscar" >
</asp:ListSearchExtender>
<asp:Button ID="btnEntidadNegocio" runat="server" style="display:none"  />
<asp:RequiredFieldValidator ID="rfvEntidadNegocio" runat="server" Display="None"
    ErrorMessage="Se requiere que seleccione un dato" ControlToValidate="ddlEntidadNegocio"
    Enabled="false" InitialValue="-1" ValidationGroup="G">
</asp:RequiredFieldValidator>
<asp:ValidatorCalloutExtender ID="vcerfvEntidadNegocio" runat="server" Enabled="true" 
    TargetControlID="rfvEntidadNegocio" CssClass="CustomValidatorCalloutStyle"
    HighlightCssClass="MyMsgHighlight" WarningIconImageUrl="..\..\Imagenes\Alertas\warning_msg.png">
</asp:ValidatorCalloutExtender>
<asp:HiddenField ID="hidDropDownSoloLectura" runat="server" Value="NO" />
<asp:HiddenField ID="hdnDropDownPostBack" runat="server" Value="0" />
<asp:HiddenField ID="hdnDropDownValue" runat="server" Value="0" />