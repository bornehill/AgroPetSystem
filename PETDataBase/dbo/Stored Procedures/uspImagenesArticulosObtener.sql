

CREATE PROCEDURE [dbo].[uspImagenesArticulosObtener](
	 @p_NombreArticulo varchar(100)
)
AS
BEGIN
SET NOCOUNT ON;
 
	SELECT 
		idimagenarticulo,
		idarticulo,
        nombre as NombreArticulo,
		fecharegistro,
		fechamodificacion,
		idusuariocreo,
		idusuariomodifico,
		pathimagen
	FROM tbarticulos AS a
		INNER JOIN tbimagenesarticulos AS ima ON ima.idarticulo = a.id_art
	WHERE a.nombre like '%' + @p_NombreArticulo + '%';
END;