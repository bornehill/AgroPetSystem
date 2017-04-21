
CREATE PROCEDURE uspDDLItems_Seleccion(
    @nOpcionSeleccion INT)
AS
BEGIN
	IF(@nOpcionSeleccion = 1)
	BEGIN
		SELECT idPerfil AS idValor, nombrePerfil AS DescripValor
		FROM tbPerfiles ORDER BY DescripValor;
	END
	
	IF(@nOpcionSeleccion = 2)
	BEGIN
		SELECT MenuId AS idValor, Menu AS DescripValor
		FROM tbmenuweb;
		/*where Padre = 1;*/
	END
	
	IF (@nOpcionSeleccion = 10)
	BEGIN
	  SELECT idusuario AS IdValor, claveusr AS DescripValor
	  FROM tbusuarios ORDER BY nombreusr;
	END
END ;
