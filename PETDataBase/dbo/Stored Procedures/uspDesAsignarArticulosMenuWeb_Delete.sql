
CREATE PROCEDURE [dbo].[uspDesAsignarArticulosMenuWeb_Delete](
    @MenId INT,
    @idGpo_Lin INT,
    @idLin_Art INT,
    @idArt INT)
AS	
BEGIN
		update tbRelMenuArticulos
		set Activo = 0
		where IdGrupo_Linea = @idGpo_Lin and IdLinea_Articulo = @idLin_Art and IdArticulo = @idArt
END ;
