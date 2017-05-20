CREATE  PROCEDURE [dbo].[uspImagenesBannersObtener](
	 @p_Descripcion varchar(100)
)
AS
BEGIN
SET NOCOUNT ON;

	SELECT idbanner, 
			descripcion, 
            idusuariocreo, 
            fechacreacion, 
            idusuariomodif, 
            fechamodif, 
            fechainiaplica, 
            fechafinaplica
	FROM tbbannerssitio
	WHERE descripcion like '%'+ @p_Descripcion+ '%'
     
END;