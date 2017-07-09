
CREATE PROCEDURE usp_GetMenuArticulos(@MenuId INT)
AS
BEGIN
	SELECT '' NombreGpoMicrosip, '' NombreLinMicrosip, '' NombreArticulo, cast(0 as decimal(18,6)) PRECIO, IdArticulo, '' Image
	FROM tbRelMenuArticulos
	WHERE (@MenuId=0 or MenuId = @MenuId) and Activo=1
	ORDER BY IdArticulo
END
