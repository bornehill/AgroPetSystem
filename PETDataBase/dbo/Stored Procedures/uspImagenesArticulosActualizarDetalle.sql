CREATE PROCEDURE [dbo].[uspImagenesArticulosActualizarDetalle](
	@p_idImagenArticulo int,
    @p_idusuarioModifico int,
    @p_pathimagen varchar(300),
    @p_iddetimagenarticulo int
)
AS
BEGIN
  
	DECLARE @fecha datetime = GETDATE();
 
	UPDATE tbimagenesarticulosdet 
    SET pathimagen = @p_pathimagen
    where iddetimagenarticulo = @p_iddetimagenarticulo;
	 
	UPDATE  tbimagenesarticulos
	set fechamodificacion = @fecha,
		idusuariomodifico = @p_idusuarioModifico	
	where idimagenarticulo = @p_idImagenArticulo;
        
END;