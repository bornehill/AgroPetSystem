<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Registro.aspx.cs" Inherits="AgroPetWeb.website.Registro" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Registro</title>
    <meta http-equiv="X-UA-Compatible" content="IE=EmulateIE8, IE=Edge, IE=EmulateIE9, IE=EmulateIE10" />
    <link rel="stylesheet" href="/Content/bootstrap.min.css"/>
    <link rel="stylesheet" href="/Content/jquery-ui.css"/>
    <link rel="stylesheet" href="/Content/style.css"/>
    
    <style>
    .thumbnail{
      padding-top: 20px;
      padding-left: 30px;
    }
    </style>
    <script src="/Scripts/jquery.js"></script>
    <script src="/Scripts/bootstrap.min.js"></script>
    <script src="/Script/jquery-ui.js"></script>

    <script lang="javascript" type="text/javascript">
      function registrarse() {
        var userName = $("#txtUser").val();
        var passWord = $("#txtPass").val();
        var passConfirm = $("#txtPassConfirm").val();

        if (passWord != passConfirm)
          $("#msg").text("Contraseña no coincide");
        else
          $.ajax({
            type: "post",
            url: "/Security/Registro.aspx/ValidaUser",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ userName: userName, pass: passWord }),
            dataType: "json",
            success: function (result) {
                $("#msg").text(result.d);
            },
            error: function (result) {
              alert('error occured');
              alert(result.responseText);
            },
            async: true
          });
			}

			function searchClient(){
				var userName = $("#txtUser").val();

				if (false)
					$("#msg").text("Cuenta de correo invalida");
				else
					$.ajax({
						type: "post",
						url: "/Security/Registro.aspx/SearchUser",
						contentType: "application/json; charset=utf-8",
						data: JSON.stringify({ userName: userName}),
						dataType: "json",
						success: function (result) {
							if (result.d.Client_Id == -1)
								$("#msg").text(result.d.Nombre);
							else {
								$("#DataClient").css("visibility", "visible");;
								$("#txtNombre").val(result.d.Nombre);
								$("#txtRFC").val(result.d.RFC);
								$("#txtCalle").val(result.d.Calle);
								$("#txtNoExt").val(result.d.Num_Exterior);
								$("#txtNoInt").val(result.d.Num_Interior);
								$("#txtColonia").val(result.d.Colonia);
								$("#txtNomEstado").val(result.d.NomEstado);
								$("#txtCodigoPostal").val(result.d.Codigo_Postal);
								$("#txtTelefono1").val(result.d.Telefono1);
								$("#txtTelefono2").val(result.d.Telefono2);
								$("#msg").text('');
							}
						},
						error: function (result) {
							alert('error occured');
							alert(result.responseText);
						},
						async: true
					});
			}
    </script>
</head>
<body>
    <form id="frmRegistro" runat="server">
      <br />
      <br />
      <br />
      <br />
      <div class="ui-widget-content ui-corner-all">
        <h2 class="ui-widget-header ui-corner-all titleArticulo">Alta de usuario</h2>
      </div>
      <div class="contaier-fluid">
        <nav class="navbar navbar-default navbar-fixed-top">
            <div class="container">
              <div class="navbar-header">
                  <button type="button" class="navbar-toggle" data-toggle="collapse" data-target="#myNavbar">
                  <span class="icon-bar"></span>
                  <span class="icon-bar"></span>
                  <span class="icon-bar"></span> 
                  </button>
                  <a class="navbar-brand logo" href="/index.aspx"></a>
              </div>
            </div>
        </nav>
      </div>
      <div class="ui-widget-content row" >
        <div class="thumbnail col-sm-6 col-md-4 col-sm-offset-4">
          <div class="form-group">
            <p id="msg" style="color:red"></p>
            <asp:Label CssClass="glyphicon glyphicon-envelope" runat="server" ToolTip="Cuenta de correo electrónico" />
            <asp:TextBox runat="server" ID="txtUser" CssClass="form-control" placeholder="Correo electrónico" Width="180"/>
          </div>
          <div class="form-group">
            <a class="btn btn-primary" role ="button" name="btnSearch" onclick="searchClient(); return false;">Buscar</a>
          </div>
<%--          <div class="form-group">
            <asp:Label CssClass="glyphicon glyphicon-envelope" runat="server" />
            <asp:TextBox runat="server" ID="txtMail" TextMode="Email" CssClass="form-control" placeholder="Email" Width="180"/>
          </div>--%>
					<div id="DataClient" style="visibility:hidden">
						<div class="form-group">
								<asp:Label CssClass="glyphicon glyphicon-lock" runat="server" />
								<asp:TextBox runat="server" ID="txtPass" TextMode="Password" CssClass="form-control" placeholder="Contrase&ntilde;a" Width="180"/>
						</div>
						<div class="form-group">
								<asp:Label CssClass="glyphicon glyphicon-lock" runat="server" />
								<asp:TextBox runat="server" ID="txtPassConfirm" TextMode="Password" CssClass="form-control" placeholder="Confirmar contrase&ntilde;a" Width="180"/>
						</div>

						<div class="form-group">
								<asp:Label runat="server" Text="RFC:" />
								<asp:TextBox runat="server" ID="txtRFC" CssClass="form-control" placeholder="RFC" Width="150"/>
						</div>
						<div class="form-group">
								<asp:Label runat="server" Text="Razón social:" />
								<asp:TextBox runat="server" ID="txtNombre" CssClass="form-control" placeholder="Razón social" Width="350"/>
						</div>
						<div class="form-group">
								<asp:Label runat="server" Text="Calle:" />
								<asp:TextBox runat="server" ID="txtCalle" CssClass="form-control" placeholder="Calle" Width="300"/>
						</div>
						<div class="form-group">
								<asp:Label runat="server" Text="No ext:" />
								<asp:TextBox runat="server" ID="txtNoExt" CssClass="form-control" placeholder="Número exterior" Width="100"/>
								<asp:Label runat="server" Text="No int.:" />
								<asp:TextBox runat="server" ID="txtNoInt" CssClass="form-control" placeholder="Número interior" Width="100"/>
						</div>
						<div class="form-group">
								<asp:Label runat="server" Text="Colonia:" />
								<asp:TextBox runat="server" ID="txtColonia" CssClass="form-control" placeholder="Colonia" Width="200"/>
						</div>
						<div class="form-group">
								<asp:Label runat="server" Text="Estado:" />
								<asp:TextBox runat="server" ID="txtNomEstado" CssClass="form-control" placeholder="Estado" Width="200"/>
								<asp:Label runat="server" Text="Codigo postal:" />
								<asp:TextBox runat="server" ID="txtCodigoPostal" CssClass="form-control" placeholder="CP." Width="100"/>
						</div>
						<div class="form-group">
								<asp:Label runat="server" Text="Teléfono:" />
								<asp:TextBox runat="server" ID="txtTelefono1" CssClass="form-control" placeholder="Teléfono" Width="200"/>
								<asp:Label runat="server" Text="Móvil:" />
								<asp:TextBox runat="server" ID="txtTelefono2" CssClass="form-control" placeholder="Móvil" Width="200"/>
						</div>
						<div class="form-group">
							<a class="btn btn-primary" role ="button" name="btnRegistrar" onclick="registrarse(); return false;">Registrar</a>
						</div>
					</div>
        </div>
      </div>
      <footer class="text-center">
          <a class="up-arrow" href="#frmIndex" data-toggle="tooltip" title="IR ARRIBA">
          <span class="glyphicon glyphicon-chevron-up"></span>
          </a>
          <br/><br/>
          <p>Grupo Agropet</p> 
      </footer>
    </form>
</body>
</html>
