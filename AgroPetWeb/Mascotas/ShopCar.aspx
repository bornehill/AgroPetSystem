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

		$(document).ready(function () {
			$('#btnAddress').on('click', function () {
				muestra('dialog', '/Client/ClientDirection.aspx', 'Dirección', 1000, 600);
			});

			$('#btnDispatch').on('click', function () {
				var dirId = $('#hdnDirId').val();
				muestra('dialog', '/Mascotas/DispatchOrder.aspx?DirId=' + dirId, 'Orden', 800, 500);
			});

			$("#btnSave").click(function (event, ui) {
				$(".spinner").each(function () {
					var lot = $(this).val();
					if (lot != '' & lot != '0') {
						var name = $(this).attr('id');
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
					}
				});
			});

		});
  </script>
  <div class="ui-widget-content ui-corner-all">
    <h2 class="ui-widget-header ui-corner-all titleArticulo">Carrito de compras</h2>
    <%GetBuys();%>
  </div>
</asp:Content>