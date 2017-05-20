
CREATE PROCEDURE [dbo].[uspImagenesBannersActualizar](
	@p_idbanner int,
    @p_descripcion varchar(100),
    @p_idusuariomodif int,
	@p_fechainiaplica datetime,
    @p_fechafinaplica datetime
)
AS
BEGIN
    
	DECLARE @fecha datetime = GETDATE();
	 
	UPDATE tbbannerssitio 
		set descripcion = @p_descripcion,
			idusuariomodif = @p_idusuariomodif,
			fechamodif = @fecha,
			fechainiaplica = @p_fechainiaplica,
			fechafinaplica = @p_fechafinaplica
		where idbanner = @p_idbanner;

END;