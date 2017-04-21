
CREATE FUNCTION dbo.ufn_MenuObtenerNivel (@IdPadre INT) 
RETURNS int
BEGIN
	RETURN (SELECT ISNULL(MAX(Nivel)+1,0) AS Nivel FROM tbmenuweb WHERE Padre = @IdPadre);
END ;
