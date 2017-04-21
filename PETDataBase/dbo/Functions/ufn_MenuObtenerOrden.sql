
CREATE FUNCTION dbo.ufn_MenuObtenerOrden (@IdPadre INT) 
RETURNS int
BEGIN
	RETURN (SELECT ISNULL(MAX(Orden)+1,0) AS Orden FROM tbmenuweb WHERE Padre = @IdPadre);
END;
