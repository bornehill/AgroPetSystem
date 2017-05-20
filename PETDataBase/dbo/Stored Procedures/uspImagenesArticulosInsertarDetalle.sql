
CREATE PROCEDURE [dbo].[uspImagenesArticulosInsertarDetalle](
 @p_idImagenArticulo int,
    @p_idusuarioModifico int,
    @p_pathimagen varchar(300)
)
AS
BEGIN
    
	DECLARE @fecha datetime = GETDATE();
    DECLARE @var_Existe int = 0;
    
    select @var_Existe = count(*) from tbimagenesarticulosdet where [idimagenarticulo] = @p_idImagenArticulo and [pathimagen] = @p_pathimagen;
 
	if @var_Existe = 0 
    begin
		
        INSERT INTO tbimagenesarticulosdet ([idimagenarticulo], [pathimagen], [fecharegistro])
		VALUES (@p_idImagenArticulo, @p_pathimagen, @fecha);
		
		UPDATE  tbimagenesarticulos
		set [fechamodificacion] = @fecha,
			[idusuariomodifico] = @p_idusuarioModifico	
		where [idimagenarticulo] = @p_idImagenArticulo;
     
    end 
        
END;