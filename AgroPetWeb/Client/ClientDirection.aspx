<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ClientDirection.aspx.cs" Inherits="AgroPetWeb.website.ClientDirection" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>

  <script>
		$(document).ready(function () {
			$('#btnCaller').on('click', function () {
				alert($('#txtCalleSend').val());
				return false;
			});

			$('#cboPais').change(function () {
				var paisId = $('#cboPais option:selected').val();

				$.ajax({
					type: "post",
					url: "/Client/ClientDirection.aspx/GetEstados",
					contentType: "application/json; charset=utf-8",
					data: JSON.stringify({ pais: { PaisId: paisId} }),
					dataType: "json",
					success: function (result) {
						$('#cboEstado').children('option:not(:first)').remove();

						$.each(result.d, function (i, item) {
							$('#cboEstado')
								.append($("<option></option>")
									.attr("value", item.EstadoId)
									.text(item.Nombre));							
						});
					},
					error: function (result) {
						alert('error occured');
						alert(result.responseText);
					},
					async: true
				});
				//return false;
			});

			$('#cboEstado').change(function () {
				var estadoId = $('#cboEstado option:selected').val();

				$.ajax({
					type: "post",
					url: "/Client/ClientDirection.aspx/GetCiudades",
					contentType: "application/json; charset=utf-8",
					data: JSON.stringify({ estado: { EstadoId: estadoId } }),
					dataType: "json",
					success: function (result) {
						$('#cboCiudad').children('option:not(:first)').remove();

						$.each(result.d, function (i, item) {
							$('#cboCiudad')
								.append($("<option></option>")
									.attr("value", item.CiudadId)
									.text(item.Nombre));
						});
					},
					error: function (result) {
						alert('error occured');
						alert(result.responseText);
					},
					async: true
				});
				//return false;
			});

			$('#btnAddAddress').on('click',function () {
				var clientId = $('#ClientId').val();
				var calle = $('#txtCalle').val();
				var numExt = $('#txtNoExt').val();
				var numInt = $('#txtNoInt').val();
				var colonia = $('#txtColonia').val();
				var ref = $('#txtReferencia').val();
				var ciudadId = $('#cboCiudad option:selected').val();
				var estadoId = $('#cboEstado option:selected').val();
				var paisId = $('#cboPais option:selected').val();
				var cp = $('#txtCodigoPostal').val();
				var tel1 = $('#txtTelefono1').val();
				var tel2 = $('#txtTelefono2').val();
				var email = $('#txtEmail').val();
				var contacto= $('#txtContacto').val();
				var nomCiudad = $('#cboCiudad option:selected').text();
				var nomEstado = $('#cboEstado option:selected').text();
				var nomPais = $('#cboPais option:selected').text();

				$.ajax({
					type: "post",
					url: "/Client/ClientDirection.aspx/AddAddress",
					contentType: "application/json; charset=utf-8",
					data: JSON.stringify({
						address: {
							ClienteId: clientId, Calle: calle, NumExt: numExt, NumInt: numInt, Colonia: colonia, Referencia: ref, CiudadId: ciudadId,
							EstadoId: estadoId, PaisId: paisId, CP: cp, Telefono1: tel1, Telefono2: tel2, Email: email, Contacto: contacto,
							NomCiudad: nomCiudad, NomEstado: nomEstado, NomPais: nomPais
						}
					}),
					dataType: "json",
					success: function (result) {
						var rowDir = "<tr><td><input type=\"radio\" name =\"rdbAddrClt\" value =\"" + result.d.DirCliId + "\"></td><td>" +
						result.d.Calle + "</td><td>" + result.d.Colonia + "</td><td>" + result.d.NomCiudad + "</td><td>" + result.d.NomEstado + "</td><td>" +
						result.d.NomPais + "</td><td>" + result.d.Contacto + "</td><tr>";
						$("#tblDirCliente").append(rowDir);
					},
					error: function (result) {
						alert('error occured');
						alert(result.responseText);
					},
					async: true
				});
				return false;
			});

			$('#btnSelect').on('click', function () {
				var selId = $('input[name=rdbAddrClt]:checked').val();

				$.ajax({
					type: "post",
					url: "/Client/ClientDirection.aspx/GetAddress",
					contentType: "application/json; charset=utf-8",
					data: JSON.stringify({
						address: { DirCliId: selId }
					}),
					dataType: "json",
					success: function (result) {
						$('#txtCalleSend').val(result.d.Calle);
						$('#txtColoniaSend').val(result.d.Colonia);
						$('#txtCiudadSend').val(result.d.NomCiudad);
						$('#txtEstadoSend').val(result.d.NomEstado);
						$('#txtPaisSend').val(result.d.NomPais);
						$('#txtContactoSend').val(result.d.Contacto);
						$('#hdnDirId').val(result.d.DirCliId);

						$('#dialog').dialog('close');
					},
					error: function (result) {
						alert('error occured');
						alert(result.responseText);
					},
					async: true
				});
				return false;
			});
		});
  </script>
</head>
<body>
    <form id="frmClientAddress" runat="server" method="post">
			<%PrintAddress(); %>

			  <button type='button' class='btn btn-default btn-lg' data-toggle='collapse' data-target='#datAddress'>
					<span class='glyphicon glyphicon-plus' aria-hidden='true'></span> Nueva
			  </button>

			  <div class='collapse' id='datAddress'>
					<div class='panel panel-warning'>
			  		<div class='panel-heading'>
			  			<h3 class='panel-title'>Nueva dirección</h3>
			  		</div>
			  		<div class='panel-body'>
							<table class='table' style='width:70%'>
								<tr>
									<th style="text-align:right">Calle:</th>
									<th>
										<asp:TextBox runat="server" ID="txtCalle" CssClass="form-control" placeholder="Calle" Width="300"/>
									</th>
								</tr>
								<tr>
									<th style="text-align:right">No. Ext:</th>
									<th>
										<asp:TextBox runat="server" ID="txtNoExt" CssClass="form-control" placeholder="Número exterior" Width="100"/>
									</th>
								</tr>
								<tr>
									<th style="text-align:right">No. Int:</th>
									<th>
										<asp:TextBox runat="server" ID="txtNoInt" CssClass="form-control" placeholder="Número interior" Width="100"/>
									</th>
								</tr>
								<tr>
									<th style="text-align:right">Colonia:</th>
									<th>
										<asp:TextBox runat="server" ID="txtColonia" CssClass="form-control" placeholder="Colonia" Width="200"/>
									</th>
								</tr>
								<tr>
									<th style="text-align:right">Referencia:</th>
									<th>
										<asp:TextBox runat="server" ID="txtReferencia" CssClass="form-control" placeholder="Referencia" Width="300"/>
									</th>
								</tr>
								<tr>
									<th style="text-align:right">Pa&iacute;s:</th>
									<th>
										<%DropPais(); %>
									</th>
								</tr>
								<tr>
									<th style="text-align:right">Estado:</th>
									<th>
										<%DropEstado(); %>
									</th>
								</tr>
								<tr>
									<th style="text-align:right">Ciudad:</th>
									<th>
										<%DropCiudad(); %>
									</th>
								</tr>
								<tr>
									<th style="text-align:right">CP:</th>
									<th>
										<asp:TextBox runat="server" ID="txtCodigoPostal" CssClass="form-control" placeholder="CP." Width="100"/>
									</th>
								</tr>
								<tr>
									<th style="text-align:right">Tel.:</th>
									<th>
										<asp:TextBox runat="server" ID="txtTelefono1" CssClass="form-control" placeholder="Teléfono" Width="200"/>
									</th>
								</tr>
								<tr>
									<th style="text-align:right">Movil:</th>
									<th>
										<asp:TextBox runat="server" ID="txtTelefono2" CssClass="form-control" placeholder="Móvil" Width="200"/>
									</th>
								</tr>
								<tr>
									<th style="text-align:right">Email:</th>
									<th>
										<asp:TextBox runat="server" ID="txtEmail" CssClass="form-control" placeholder="Correo electrónico" Width="200"/>
									</th>
								</tr>
								<tr>
									<th style="text-align:right">Contacto:</th>
									<th>
										<asp:TextBox runat="server" ID="txtContacto" CssClass="form-control" placeholder="Contacto" Width="300"/>
									</th>
								</tr>
							</table>
							<button type='button' id='btnAddAddress' class='btn btn-info btn-lg'>
								<span class='glyphicon glyphicon-floppy-save' aria-hidden='true'></span> Guardar
							</button>
							<input type="hidden" id="ClientId" value="<%=ClienteId %>"/>
			  		</div>
					</div>
				</div>
    </form>
</body>
</html>
