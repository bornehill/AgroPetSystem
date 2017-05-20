
CREATE  PROCEDURE [dbo].[uspImagenesBannerEliminaDetalle](
 @p_idBannerDetalle int,
    @p_idUsuarioModifico int,
    @p_idbanner int
)
AS
BEGIN
	DECLARE @fecha datetime = GETDATE();
 
	delete from tbbannerssitiodetalle
    where iddetbanners = @p_idBannerDetalle;
	 
	UPDATE tbbannerssitio
	set fechamodif = @fecha,
		idusuariomodif = @p_idUsuarioModifico	
	where idbanner = @p_idbanner;
        
END;