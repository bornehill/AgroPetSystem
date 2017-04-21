CREATE PROCEDURE uspUsuarios_Seleccionar(@nOpcionSeleccion INT, @nIdUsuario INT)
AS
BEGIN
	IF (@nOpcionSeleccion = 1)
	BEGIN
		SELECT tbU.idusuario, tbU.idperfil, tbP.nombreperfil, tbU.nombreusr, tbU.claveusr, tbU.passwordusr, tbU.activo
        FROM tbusuarios As tbU
			INNER JOIN tbperfiles AS tbP
				ON tbU.idperfil = tbP.idPerfil
		WHERE idusuario = @nIdUsuario;    
    END
    
    IF (@nOpcionSeleccion = 2)
	BEGIN
		SELECT tbU.idusuario, tbU.idperfil, tbP.nombreperfil, tbU.nombreusr, tbU.claveusr, tbU.passwordusr, tbU.activo
        FROM tbusuarios As tbU
			INNER JOIN tbperfiles AS tbP
				ON tbU.idperfil = tbP.idPerfil;
    END
END
