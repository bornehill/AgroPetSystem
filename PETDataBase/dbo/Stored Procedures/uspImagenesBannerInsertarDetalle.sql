

CREATE PROCEDURE [dbo].[uspImagenesBannerInsertarDetalle](
 @p_idBanner int,
    @p_idUsuarioModifico int,
    @p_RutaBannerDetalle varchar(300),
    @p_OrdenDetalle int,
 @p_titulo varchar(150),
    @p_subtitulo varchar(150)
)
AS
BEGIN
    
	DECLARE @fecha datetime = GETDATE();
    DECLARE @var_Existe int = 0;
     
    select @var_Existe = count(*) from tbbannerssitiodetalle where pathimagen = @p_RutaBannerDetalle and idbanner = @p_idBanner;
 
	if @var_Existe = 0 
    begin
		
        INSERT INTO tbbannerssitiodetalle (idbanner, pathimagen, orden, titulo, subtitulo)
		VALUES (@p_idBanner, @p_RutaBannerDetalle, @p_OrdenDetalle, @p_titulo, @p_subtitulo);
		
		UPDATE tbbannerssitio
		set fechamodif = @fecha,
			idusuariomodif = @p_idUsuarioModifico	
		where idbanner = @p_idBanner;
    
    end 
        
END;