CREATE  PROCEDURE [dbo].[uspUsuariosConsultar](
	@param_IdPerfil INT,
    @param_IdUsuario INT,
    @param_Activo INT
)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT tbU.idusuario, 
		tbU.idperfil, 
        tbP.nombreperfil,
        tbU.nombreusr, 
        tbU.claveusr, 
        tbU.passwordusr,
        tbU.activo,
		CASE tbP.activo WHEN 1 THEN 'Activo' WHEN 2 THEN 'No Activo' END as estatus,
        tbU.idusuariocreo, 
        tbU.fechacreacion, 
        (select nombreusr from tbusuarios where idusuario = tbU.idusuariocreo) as usuariocreo,
        tbU.idusuariomodif, 
        tbU.fechamodif as fechaultmodif,
        (select nombreusr from tbusuarios where idusuario = tbU.idusuariomodif) as usuarioultmodif
    FROM tbusuarios as tbU
		INNER JOIN tbperfiles AS tbP ON tbU.idperfil = tbP.idPerfil
	where tbU.idperfil = ISNULL(@param_IdPerfil, tbU.idperfil)
		and tbU.idusuario = ISNULL(@param_IdUsuario, tbU.idusuario)
		and tbU.activo = ISNULL(@param_Activo, tbU.activo) 
END;