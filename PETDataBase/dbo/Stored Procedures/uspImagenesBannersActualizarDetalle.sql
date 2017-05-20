

CREATE  PROCEDURE [dbo].[uspImagenesBannersActualizarDetalle](
	@p_idbanner int,
    @p_idusuarioModifico int,
    @p_RutaBannerDetalle varchar(300),
    @p_iddetbanners int,
    @p_OrdenDetalle int,
    @p_titulo varchar(150),
    @p_subtitulo varchar(150)
)
AS
BEGIN
    
	DECLARE @fecha datetime = GETDATE();
  
	UPDATE tbbannerssitiodetalle
    SET pathimagen = @p_RutaBannerDetalle,
		orden = @p_OrdenDetalle,
        titulo = @p_titulo,
        subtitulo = @p_subtitulo
    where iddetbanners = @p_iddetbanners;
	
	UPDATE tbbannerssitio 
	set fechamodif = @fecha,
		idusuariomodif = @p_idusuarioModifico	
	where idbanner = @p_idbanner;
        
END;