
CREATE PROCEDURE uspDesAsignarArticulosMenuWeb_Delete(
    @MenId INT,
    @idGpo_Lin INT,
    @idLin_Art INT,
    @idArt INT)
AS	
BEGIN
	DELETE
	FROM WebSites.tbRelMenuArticulos
	WHERE MenuId = @MenId
	    AND IdArticulo = @idArt;
END ;
