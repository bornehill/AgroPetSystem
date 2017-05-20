
CREATE  PROCEDURE uspImagenesBannersObtenerDetalle(
	 @p_idBanner int
)
AS
BEGIN
SET NOCOUNT ON;

	SELECT iddetbanners, 
			idbanner, 
            pathimagen, 
            orden,
            titulo,
            subtitulo
	FROM tbbannerssitiodetalle
	WHERE idbanner = @p_idBanner
    order by orden;
     
END;