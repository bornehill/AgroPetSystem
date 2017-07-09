<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DispatchOrder.aspx.cs" Inherits="AgroPetWeb.Mascotas.DispatchOrder" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
  <style>
    .panel {
      color: rgb(255, 255, 255);
	    background-color: rgba(227, 117, 42, 0.6);
    }
  </style>
</head>
<body>
    <form id="frmDispatchOrder" runat="server" method="post">
			<%if (folio == null)
				{ %>
					<div class="panel">
						<div	class="panel-body">
							<img src="/images/ErrorDispatch.png" class="col-md-4 col-md-offset-4"/>
							<h1 class="col-md-6 col-md-offset-4">Ocurrio un error al solicitar el pedido.</h1>
						</div>
					</div>
					<%}
			else { %>
					<div class="panel">
						<div	class="panel-body">
							<img src="/images/pedido-realizado.png" class="col-md-4 col-md-offset-4"/>
							<h1 class="col-md-8 col-md-offset-4">¡Enhorabuena!</h1>
							<p class="col-md-6 col-md-offset-4">Tu pedido se ha realizazo correctamente con folio : <b><%=folio %></b>.
								¡Agradecemos tu preferencia!
								Tú vendedor se pondra en contacto contigo en breve.
							</p>
						</div>
					</div>		
			<%} %>
    </form>
</body>
</html>
