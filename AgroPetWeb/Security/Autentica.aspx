<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Autentica.aspx.cs" Inherits="AgroPetWeb.website.Autentica" %>

<!DOCTYPE html>

<html>
<head runat="server">
    <title></title>
    <script lang="javascript" type="text/javascript">
      function autenticame() {
        var userName = $("#txtUser").val();
        var passWord = $("#txtPass").val();
        if (userName.length == 0 || passWord == 0)
          $("#msg").text("Ingrese usuario y contraseña");
        else
          $.ajax({
            type: "post",
            url: "/Security/Autentica.aspx/Autenticate",
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ userName: userName, passWord: passWord }),
            dataType: "json",
            success: function (result) {
              if (isNaN(result.d))
                $("#msg").text(result.d);
              else {
                var form = document.getElementById("frmAutentica");
                form.action = "/Security/Autentica.aspx?idUser=" + result.d;
                form.submit();
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
  <form id="frmAutentica" runat="server" method="post">
    <div class="navbar-form navbar-left" role="login">
      <div><p id="msg" style="color:red"></p></div>
      <div class="form-group">
        <asp:Label CssClass="glyphicon glyphicon-user" runat="server" />
        <asp:TextBox runat="server" ID="txtUser" CssClass="form-control" placeholder="Usuario"/>
      </div>
      <div class="form-group">
          <asp:Label CssClass="glyphicon glyphicon-lock" runat="server" />
          <asp:TextBox runat="server" ID="txtPass" TextMode="Password" CssClass="form-control" placeholder="Contrase&ntilde;a"/>
      </div>
      <br /><br />
      <div class="form-group">
        <a class="btn btn-primary" role ="button" name="btnIngresa" onclick="autenticame(); return false;" >Ingresar</a>
        <%--<asp:Button runat="server" ID="btnIngresa" CssClass="btn btn-default" Text="Ingresar" OnClientClick="autenticame();" />--%>
      </div>
    </div>
  </form>
</body>
</html>