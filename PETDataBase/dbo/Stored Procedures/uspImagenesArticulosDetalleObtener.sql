
CREATE PROCEDURE uspImagenesArticulosDetalleObtener(
	 @p_IdImagenArticulo int
)
AS
BEGIN
SET NOCOUNT ON;

	SELECT [iddetimagenarticulo], 
			[idimagenarticulo],
            [pathimagen], 
            [fecharegistro]
	FROM tbimagenesarticulosdet
	WHERE [idimagenarticulo] = @p_IdImagenArticulo;
     
END;