CREATE PROCEDURE [dbo].[uspImagenesArticulosActualizar](
	@p_idImagenArticulo int,
    @p_idusuarioModifico int,
    @p_pathimagen varchar(300)
)
AS
BEGIN
  
	DECLARE @fecha datetime = GETDATE();

	UPDATE  tbimagenesarticulos
	set fechamodificacion = @fecha,
		idusuariomodifico = @p_idusuarioModifico,
		pathimagen = @p_pathimagen	
	where idimagenarticulo = @p_idImagenArticulo;
        
END;