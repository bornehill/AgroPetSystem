

Create PROCEDURE [dbo].[uspImagenesArticulosElimina](
    @idImagenArticulo int
)
AS
BEGIN
   
	DECLARE @fecha datetime = GETDATE();
 
	delete from tbimagenesarticulos 
    where idimagenarticulo = @idImagenArticulo;
	 
END;