<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPet.Master" AutoEventWireup="true" CodeBehind="Articulos.aspx.cs" Inherits="AgroPetWeb.Mascotas.Articulos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPage" runat="server">
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
    .addButton {
      color:red;
    }
    .btn {
      padding:3px 3px;
    }
    .glyphicon {
      top:0px;
      left:0px;
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
    });

    $(document).ready(function() {
      $(".addButton").click(function (event, ui) {
        var name = $(this).attr('name');
        var idArt = name.substr(name.indexOf("_") + 1);
        var lot = $("#spinner" + name.substr(name.indexOf("_"))).val();
        var price = $("#price" + name.substr(name.indexOf("_"))).val();
        var userId = $("#UserId").val();

        $.ajax({
          type: "post",
          url: "Articulos.aspx/AddCar",
          contentType: "application/json; charset=utf-8",
          data: JSON.stringify({ item: { UserId: userId, ItemId: idArt, lot: lot, price: price } }),
          dataType: "json",
          success: function (result) {
            $(".badge").html("").append(result.d);
            $("#spinner" + name.substr(name.indexOf("_"))).val("");
          },
          error: function (result) {
            alert('error occured');
            alert(result.responseText);
          },
          async: true
        });
      });
    });
    </script>
  <asp:Label Text="Aves" runat="server"></asp:Label>
  <br />
  <br />
  <br />
  <div class="ui-widget-content ui-corner-all">
    <h2 class="ui-widget-header ui-corner-all titleArticulo"><%=Titulo %></h2>
    <div class="row">
      <asp:Label runat="server" Text="Registros"/>
      <asp:DropDownList ID="cboRecords" runat="server">
        <asp:ListItem Value="10">10</asp:ListItem>
        <asp:ListItem Value="30">30</asp:ListItem>
        <asp:ListItem Value="50">50</asp:ListItem>
        <asp:ListItem Value="100">100</asp:ListItem>
      </asp:DropDownList>
      <asp:LinkButton ID="btnFastBack" runat="server" Cssclass="btn btn-default" Width="25px" Height="25px">
        <span class="glyphicon glyphicon-fast-backward" aria-hidden="true"></span>
      </asp:LinkButton>
      <asp:LinkButton ID="btnBack" runat="server" Cssclass="btn btn-default" Width="25px" Height="25px">
        <span class="glyphicon glyphicon-backward" aria-hidden="true"></span>
      </asp:LinkButton>
      <asp:LinkButton ID="btnForward" runat="server" Cssclass="btn btn-default" Width="25px" Height="25px">
        <span class="glyphicon glyphicon-forward" aria-hidden="true"></span>
      </asp:LinkButton>
      <asp:LinkButton ID="btnFastForward" runat="server" Cssclass="btn btn-default" Width="25px" Height="25px">
        <span class="glyphicon glyphicon-fast-forward" aria-hidden="true"></span>
      </asp:LinkButton>
    </div>
    <%PrintArticulos();%>
  </div>
</asp:Content>