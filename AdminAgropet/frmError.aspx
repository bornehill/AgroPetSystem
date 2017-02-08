<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="frmError.aspx.cs" Inherits="AdminAgropet.frmError" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv='X-UA-Compatible' content="IE=9; IE=8; IE=7; IE=EDGE, chrome=1" />
    <title>Error, Agropet</title>
    <link href="Content/bootstrap.min.css" rel="stylesheet" />
    <script src="~/Scripts/jquery-1.9.1.js" type="text/javascript"></script>
    <script src="~/Scripts/bootstrap.js" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="panel panel-default" style="padding:50px">
            <div class="panel-heading" style="text-align:center">
                <label>AVISO AGROPET</label>
            </div>
            <div class="panel-body">
                <div class="row">
                    <div class="col-sm-3 col-md-3" style="text-align: center;">
                        <img src="Imagenes/PaginaMantenimiento.png" class="img-responsive" alt="Responsive image" />
                    </div>
                    <div class="col-sm-9 col-md-9">
                        <div class="row">
                            <div class="col-sm-12 col-md-12">
                                <div class="alert alert-danger" role="alert">
                                    <strong>Error:</strong> Ocurrio un Error en el Sistema, es posible que la pagina se encuentre en Mantenimiento
                                </div>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-sm-12 col-md-12">
                                <div class="alert alert-warning" role="alert">
                                    <div id="lblError" runat="server">
                                        <strong>Detalle:</strong>
                                        <asp:Label ID="lbltextoError" runat="server"></asp:Label>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>


