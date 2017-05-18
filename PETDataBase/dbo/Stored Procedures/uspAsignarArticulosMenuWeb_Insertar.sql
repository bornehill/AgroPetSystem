CREATE PROCEDURE [dbo].[uspAsignarArticulosMenuWeb_Insertar](
    @MenId INT,
    @idGpo_Lin INT,
    @idLin_Art INT,
    @idArt INT)
AS	
BEGIN
	
	if not exists(select * from tbRelMenuArticulos 
				  where MenuId = @MenId and IdGrupo_Linea = @idGpo_Lin and IdLinea_Articulo = @idLin_Art and IdArticulo = @idArt)
	BEGIN
		INSERT INTO tbRelMenuArticulos
				(MenuId,
				 IdGrupo_Linea,
				 IdLinea_Articulo,
				 IdArticulo)
		VALUES (@MenId,
			@IdGpo_Lin,
			@IdLin_Art,
			@IdArt);
	END
	else 
	BEGIN
		update tbRelMenuArticulos
		set Activo = 1
		where IdGrupo_Linea = @idGpo_Lin and IdLinea_Articulo = @idLin_Art and IdArticulo = @idArt
	END
END;
