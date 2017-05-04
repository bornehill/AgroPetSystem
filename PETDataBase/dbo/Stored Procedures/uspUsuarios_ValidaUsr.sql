CREATE  PROCEDURE uspUsuarios_ValidaUsr
( 
	@vNombreUsuario varchar(20),
	@vPassUsuario varchar(20))
AS
BEGIN
SET NOCOUNT ON;
	IF EXISTS(SELECT idusuario FROM tbusuarios WHERE claveusr = @vNombreUsuario AND passwordusr =  @vPassUsuario) 
	BEGIN
		SELECT tu.idusuario, 
			tu.idperfil, 
			tu.nombreusr as nombre, 
			tu.claveusr as claveusuario,
			tu.passwordusr as passwdusr, 
			tu.activo, tp.nombreperfil
		FROM tbusuarios AS tu 
			INNER JOIN tbperfiles tp ON tu.idperfil = tp.idperfil
        WHERE tu.claveusr = @vNombreUsuario 
			AND	tu.passwordusr = @vPassUsuario
    END 
    ELSE 
	BEGIN
		SELECT -11 as idusuario, 
			-11 as idperfil, 
			'No Existe' as nombre, 
			'' as claveusuario, 
			'' passwdusr, 
			0 as activo,
			 '' nombreperfil
		FROM tbusuarios;
	END 
END
