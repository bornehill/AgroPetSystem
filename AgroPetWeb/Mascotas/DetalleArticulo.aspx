<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPet.Master" AutoEventWireup="true" CodeBehind="DetalleArticulo.aspx.cs" 
		EnableEventValidation="false"	Inherits="AgroPetWeb.Mascotas.DetalleArticulo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPage" runat="server">
  <style>
    .row {
      padding: 20px;
    }

    .thumbnail img {
      padding-top: 10px;
    }
    .spinner {
      width:40px;
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

    .offItem {
      font-size:24px;
      color:red;
			text-decoration:line-through;
    }
    .well {
      color: rgba(227, 117, 42, 0.6);
	    background-color: rgba(254, 238, 189, 0.3);
    }
  </style>

  <script>
    $( function() {
      $( ".spinner" ).spinner({
        spin: function( event, ui ) {
          if ( ui.value > 10000 ) {
            $( this ).spinner( "value", 10000 );
            return false;
          } else if ( ui.value < 0 ) {
            $( this ).spinner( "value", 0 );
            return false;
          }
        }
      });
    });

		function DelArt() {
			var form = document.getElementById("formMaster");
			form.action = form.action.substr(0, form.action.indexOf("?")) + "?DelArtId=" + $("#DelArtId").val();
		}

    $(document).ready(function() {
      $(".addButton").click(function (event, ui) {
        var name = $(this).attr('name');
        var idArt = name.substr(name.indexOf("_") + 1);
        var lot = $("#spinner" + name.substr(name.indexOf("_"))).val();
				var price = $("#price" + name.substr(name.indexOf("_"))).val();
				var off = $("#off" + name.substr(name.indexOf("_"))).val();
				var tax = $("#tax" + name.substr(name.indexOf("_"))).val();
        var userId = $("#UserId").val();

        $.ajax({
          type: "post",
          url: "ArticulosPedido.aspx/AddCar",
          contentType: "application/json; charset=utf-8",
					data: JSON.stringify({ item: { UserId: userId, ItemId: idArt, lot: lot, price: price, off: off, tax: tax } }),
          dataType: "json",
          success: function (result) {
            $(".badge").html("").append(result.d);
            $("#spinner" + name.substr(name.indexOf("_"))).val("0");
          },
          error: function (result) {
            alert('error occured');
            alert(result.responseText);
          },
          async: true
        });
			});

			$(".updButton").click(function (event, ui) {
				var name = $(this).attr('name');
				var idArt = name.substr(name.indexOf("_") + 1);
				var lot = $("#spinner" + name.substr(name.indexOf("_"))).val();
				var notes = $("#txtNotes" + name.substr(name.indexOf("_"))).val();
				var userId = $("#UserId").val();

				$.ajax({
					type: "post",
					url: "DetalleArticulo.aspx/UpdateCar",
					contentType: "application/json; charset=utf-8",
					data: JSON.stringify({ item: { UserId: userId, ItemId: idArt, lot: lot, notes: notes } }),
					dataType: "json",
					success: function (result) {
						$(".badge").html("").append(result.d);
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
  <div class="ui-widget-content ui-corner-all">
    <h2 class="ui-widget-header ui-corner-all titleArticulo"><%=Titulo %></h2>
	<%PrintDetalle();%>
	</div>
</asp:Content>
