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
        <div class="thumbnail col-sm-6 col-md-4">
        </div>
        <div class="thumbnail col-sm-6 col-md-4">
          <div class="form-group">
            <p id="msg" style="color:red"></p>
            <asp:Label CssClass="glyphicon glyphicon-user" runat="server" />
            <asp:TextBox runat="server" ID="txtUser" CssClass="form-control" placeholder="Usuario" Width="180"/>
          </div>
<%--          <div class="form-group">
            <asp:Label CssClass="glyphicon glyphicon-envelope" runat="server" />
            <asp:TextBox runat="server" ID="txtMail" TextMode="Email" CssClass="form-control" placeholder="Email" Width="180"/>
          </div>--%>
          <div class="form-group">
              <asp:Label CssClass="glyphicon glyphicon-lock" runat="server" />
              <asp:TextBox runat="server" ID="txtPass" TextMode="Password" CssClass="form-control" placeholder="Contrase&ntilde;a" Width="180"/>
          </div>
          <div class="form-group">
              <asp:Label CssClass="glyphicon glyphicon-lock" runat="server" />
              <asp:TextBox runat="server" ID="txtPassConfirm" TextMode="Password" CssClass="form-control" placeholder="Confirmar contrase&ntilde;a" Width="180"/>
          </div>
          <div class="form-group">
            <a class="btn btn-primary" role ="button" name="btnRegistrar" onclick="registrarse(); return false;" >Registrar</a>
          </div>
        </div>
      </div>
      <footer class="text-center">
          <a class="up-arrow" href="#frmIndex" data-toggle="tooltip" title="IR ARRIBA">
          <span class="glyphicon glyphicon-chevron-up"></span>
          </a>
          <br/><br/>
          <p>Grupo Agropet <a href="http://www.petcetera.com" data-toggle="tooltip" title="Visita Petcetera">www.petcetera.com</a></p> 
      </footer>
    </form>
</body>
</html>
