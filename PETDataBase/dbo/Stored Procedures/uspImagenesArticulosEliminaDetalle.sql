

CREATE PROCEDURE [dbo].[uspImagenesArticulosEliminaDetalle](
 @p_IdDetImagenArticulo int,
    @p_idImagenArticulo int,
    @p_idusuarioModifico int
)
AS
BEGIN
   
	DECLARE @fecha datetime = GETDATE();
 
	delete from [tbimagenesarticulosdet] 
    where [iddetimagenarticulo] = @p_IdDetImagenArticulo;
	 
	UPDATE  tbimagenesarticulos
	set [fechamodificacion] = @fecha,
		[idusuariomodifico] = @p_idusuarioModifico	
	where [idimagenarticulo] = @p_idImagenArticulo;
        
END;