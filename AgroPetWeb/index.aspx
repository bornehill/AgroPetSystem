<%@ Page Title="" Language="C#" MasterPageFile="~/MasterPet.Master" AutoEventWireup="true" CodeBehind="index.aspx.cs" Inherits="AgroPetWeb.website.Index" %>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPage" runat="server">
    <div id="myCarousel" class="carousel slide" data-ride="carousel">
        <!-- Indicators -->
        <ol class="carousel-indicators">
            <li data-target="#myCarousel" data-slide-to="0" class="active"></li>
            <li data-target="#myCarousel" data-slide-to="1"></li>
            <li data-target="#myCarousel" data-slide-to="3"></li>
        </ol>

        <!-- Wrapper for slides -->
        <div class="carousel-inner" role="listbox">
          <%GetBannersWeb(); %>
        </div>

        <!-- Left and right controls -->
        <a class="left carousel-control" href="#myCarousel" role="button" data-slide="prev">
        <span class="glyphicon glyphicon-chevron-left" aria-hidden="true"></span>
        <span class="sr-only">Previous</span>
        </a>
        <a class="right carousel-control" href="#myCarousel" role="button" data-slide="next">
        <span class="glyphicon glyphicon-chevron-right" aria-hidden="true"></span>
        <span class="sr-only">Next</span>
        </a>
    </div>

    <div class="container-fluid bgwhite">
        <div class="container">
            <h3 class="text-center">Contacto</h3>
            <p class="text-center"><em>Nos gusta estar en contacto con nuestros clientes!</em></p>
            <div class="row test">
                <div class="col-md-4">
                    <p>Contactanos.</p>
                    <p><span class="glyphicon glyphicon-map-marker"></span> Asunción 202,Barrio de San Marcos,20070 Aguascalientes, Ags.</p>
                    <p><span class="glyphicon glyphicon-phone"></span> Telefono: 01 (449) 1944741</p>
                    <p><span class="glyphicon glyphicon-envelope"></span> Email: mail@mail.com</p> 
                </div>
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-sm-6 form-group">
                            <asp:TextBox ID="txtName" CssClass="form-control" runat="server" placeholder="Nombre"/>
                        </div>
                        <div class="col-sm-6 form-group">
                            <asp:TextBox ID="txtEmail" CssClass="form-control" runat="server"  placeholder="Correo electr&oacute;nio"/>
                        </div>
                    </div>

                    <textarea class="form-control" id="comments" name="comments" placeholder="Comentario" rows="5"></textarea>
                    <div class="row">
                        <br>
                        <div class="col-md-12 form-group">
                            <asp:Button runat="server" ID="btnSend" CssClass="btn pull-right" Text="Enviar" OnClientClick="muestra('dialog','Prueba.aspx', 'Prueba');return false;"/>
                        </div>
                    </div> 
                </div>
            </div>
        </div>
      <div id="googleMap"></div>
  </div>
</asp:Content>