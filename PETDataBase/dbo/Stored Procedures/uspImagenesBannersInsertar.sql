
CREATE PROCEDURE [dbo].[uspImagenesBannersInsertar](
	@p_descripcion varchar(100),
    @p_idusuariocreo int,
    @p_fehcaInicioApp datetime,
    @p_fechaFinApp datetime
)
AS
BEGIN
	DECLARE @var_Existe int = 0;
	select @var_Existe = count(*) from tbbannerssitio where [descripcion] = @p_descripcion;
 
	select @var_Existe

	if @var_Existe = 0 
    begin
	 
		INSERT INTO tbbannerssitio ([descripcion], [idusuariocreo], [fechacreacion], [fechainiaplica], [fechafinaplica])
		VALUES (@p_descripcion, @p_idusuariocreo, GETDATE(), @p_fehcaInicioApp, @p_fechaFinApp);
    
    end 
END;