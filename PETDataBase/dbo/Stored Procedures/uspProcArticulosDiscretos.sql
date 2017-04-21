


CREATE PROCEDURE uspProcArticulosDiscretos(@artDiscretoID INT, @clave VARCHAR(20), @articuloID INT, @tipo CHAR(1), @fecha DATETIME)
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM tbArticulos_Discretos 
				WHERE ID_ART_DISC = @artDiscretoID)
	BEGIN
		INSERT INTO tbarticulos_discretos
		(
		ART_DISCRETO_ID,
		CLAVE,
		ARTICULO_ID,
		TIPO,
		FECHA)
		VALUES
		(@artDiscretoID,
        @clave,
		@articuloID,
		@tipo,
		@fecha);
    END
    ELSE
    BEGIN
		UPDATE tbarticulos_discretos
		SET
		CLAVE = @clave,
		ARTICULO_ID = @articuloID,
		TIPO = @tipo,
		FECHA = @fecha
		WHERE ART_DISCRETO_ID = @artDiscretoID;
    END;
END ;
