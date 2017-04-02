<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPet.Master" AutoEventWireup="true" CodeBehind="Aves.aspx.cs" Inherits="AgroPetWeb.Mascotas.Aves" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
  <style>
    .row {
      padding: 20px;
    }

    .thumbnail img {
      padding-top: 10px;
    }
    .spinner {
      width:30px;
    }
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
    } );
    </script>
  <asp:Label Text="Aves" runat="server"></asp:Label>
  <br />
  <br />
  <br />
  <div class="ui-widget-content ui-corner-all">
    <h2 class="ui-widget-header ui-corner-all titleArticulo">Aves</h2>
    <%Articulos();%>
  </div>
</asp:Content>
