CREATE PROCEDURE [dbo].[uspPerfiles_Consultar](
	@IdPerfil int = null, 
	@Activo int = null 
)
AS
BEGIN
	SELECT idperfil, nombreperfil, fechacreacion, idusuariocreo, fechaultmodif, idusuarioultmodif, activo
	FROM tbperfiles 
	WHERE idperfil = isnull(@IdPerfil , idperfil)
		AND activo = isnull(@Activo, activo) 
END