
CREATE PROCEDURE [dbo].[uspPerfiles_Actualiza](
	@IdPerfil int, 
	@NombrePerfil varchar(100),
--	@FechaUltModif varchar(20), 
	@IdUsuarioUltModif int,
	@Activo int)
AS
BEGIN

	UPDATE tbperfiles SET nombreperfil = @NombrePerfil, 
		fechaultmodif = GETDATE(),
        idusuarioultmodif = @IdUsuarioUltModif,
        activo = @Activo
	WHERE idperfil = @IdPerfil;
        
    SELECT 'OKActualizacion de datos del perfil terminó correctamente.' AS MsgProceso;    
END ;
