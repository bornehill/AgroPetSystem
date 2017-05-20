<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPet.Master" AutoEventWireup="true" CodeBehind="ShopCar.aspx.cs" Inherits="AgroPetWeb.Mascotas.ShopCar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPage" runat="server">
  <style>
    .row {
      padding: 20px;
    }
    .spinner {
      width:30px;
    }
    .thumbnail img {
      padding-top: 10px;
    }
    /*.addButton {
      color:red;
    }*/
  </style>
  <script>
    $( function() {
      $( ".spinner" ).spinner({
        spin: function( event, ui ) {
          if ( ui.value > 100 ) {
            $( this ).spinner( "value", 100 );
            return false;
          } else if ( ui.value < 0 ) {
            $( this ).spinner( "value", 0 );
            return false;
          }
        }
      });
    });
  </script>
  <asp:Label Text="Shop car" runat="server"></asp:Label>
  <br />
  <br />
  <br />
  <div class="ui-widget-content ui-corner-all">
    <h2 class="ui-widget-header ui-corner-all titleArticulo">Carrito de compras</h2>
    <%GetBuys();%>
  </div>
</asp:Content>