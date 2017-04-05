<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPet.Master" AutoEventWireup="true" CodeBehind="Acuario.aspx.cs" Inherits="AgroPetWeb.Mascotas.Acuario" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <asp:Label Text="Acuarios" runat="server"></asp:Label>
  <br />
  <br />
  <br />
  <div class="ui-widget-content ui-corner-all">
    <h2 class="ui-widget-header ui-corner-all titleArticulo">Pecuarios</h2>
    <%Articulos();%>
  </div>
</asp:Content>