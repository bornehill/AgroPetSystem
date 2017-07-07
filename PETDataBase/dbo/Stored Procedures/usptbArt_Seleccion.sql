
CREATE PROCEDURE usptbArt_Seleccion(
    @NombreArt VARCHAR(100),
    @LineaId INT
    )
AS	
BEGIN
	DECLARE @nom VARCHAR(150);
	DECLARE @script VARCHAR(5000);
	
	IF(@NombreArt = '' AND @LineaId = 0)
	BEGIN
		SET @nom = ' 1 = 1 ';
	END
	ELSE
	BEGIN
		IF (@NombreArt != '')
		BEGIN
			SET @nom = ' (A.Nombre LIKE CONCAT(''%'','''+@NombreArt+''',''%'')) '
			IF (@LineaId > 0)
			BEGIN
				SET @nom = @nom+' AND AMM.MenuId = '+@LineaId+' '
			END
		END
		ELSE
			BEGIN
				IF (@LineaId > 0)
				BEGIN
					SET @nom = ' AMM.MenuId = '+@LineaId+' '
				END
			END
	END		
	SET @script = 'SELECT A.Id_Art, CA.Clave_Articulo AS cve_articulo, A.Nombre AS NombreArt, AMM.MenuId AS LineaId, MWP.Menu AS NombrePadre, NULL AS Existencia '+
	'FROM tbarticulos_menu_marca AS AMM '+
	'INNER JOIN tbArticulos AS A '+
	'ON A.Id_Art = AMM.Id_Art '+
	'INNER JOIN tbClaves_Articulos AS CA '+
	'ON CA.Articulo_Id = A.Articulo_Id '+
	'INNER JOIN tbmenuweb AS MW '+
	'ON MW.MenuId = AMM.MenuId '+
	'INNER JOIN tbmenuweb AS MWP '+
	'ON MW.Padre = MWP.MenuId '+
	'WHERE '+ @nom

	EXEC @script;
END