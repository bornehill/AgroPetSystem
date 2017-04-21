
CREATE PROCEDURE uspAsignarArticulosMenuWeb_Insertar(
    @MenId INT,
    @idGpo_Lin INT,
    @idLin_Art INT,
    @idArt INT)
AS	
BEGIN
	SELECT @idLin_Art=ID_LIN_ART FROM tbarticulos WHERE ID_ART = @idArt;
	SELECT @idGpo_Lin=ID_GRUPO_LIN FROM tblineas_articulos WHERE ID_LIN_ART = @idLin_Art;
	
	INSERT INTO tbRelMenuArticulos
		    (MenuId,
		     IdGrupo_Linea,
		     IdLinea_Articulo,
		     IdArticulo)
	VALUES (@MenId,
		@IdGpo_Lin,
		@IdLin_Art,
		@IdArt);
    END ;
