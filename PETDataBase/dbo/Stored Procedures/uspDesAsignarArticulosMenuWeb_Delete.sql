
CREATE PROCEDURE [dbo].[uspDesAsignarArticulosMenuWeb_Delete](
    @MenId INT,
    @idGpo_Lin INT,
    @idLin_Art INT,
    @idArt INT,
	@esLibre bit = 0)
AS	
BEGIN
	
	if @esLibre = 0
	begin
		update tbRelMenuArticulos
		set Activo = 0
		where MenuId = @MenId
	end
	else
	begin
		update tbRelMenuArticulos
		set Activo = 0
		where IdGrupo_Linea = @idGpo_Lin and IdLinea_Articulo = @idLin_Art and IdArticulo = @idArt
	end	
END ;
