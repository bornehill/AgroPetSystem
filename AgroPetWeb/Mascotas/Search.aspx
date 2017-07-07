<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="AgroPetWeb.Mascotas.Search" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="frmSearch" runat="server" action="/Mascotas/Search.aspx">
			<div class="navbar-form navbar-left" role="login">
				<div class="form-group">
					<asp:Label CssClass="glyphicon glyphicon-search" runat="server" />
					<asp:TextBox runat="server" ID="txtSearch" CssClass="form-control" placeholder="Buscar"/>
				</div>
				<br /><br />
				<asp:Button runat="server" ID="btnSearch" OnClick="search" Text="Buscar" ToolTip="Buscar" CssClass="btn btn-primary" />
			</div>
    </form>
</body>
</html>
