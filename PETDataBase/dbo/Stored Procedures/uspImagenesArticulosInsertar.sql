
CREATE PROCEDURE [dbo].[uspImagenesArticulosInsertar](
    @idArticulo int,
    @idusuariocreo int,
	@pathimagen nvarchar(300)
)
AS
BEGIN

	DECLARE @var_Existe int = 0;
	select @var_Existe = count(*) from tbimagenesarticulos where [idarticulo] = @idArticulo;
 
	if @var_Existe = 0 
    begin

		INSERT INTO tbimagenesarticulos ([idarticulo], [fecharegistro], [idusuariocreo], pathimagen)
		VALUES (@idArticulo, GETDATE(), @idusuariocreo, @pathimagen);
     
    end 
END;