

CREATE PROCEDURE uspPerfiles_Elimina(@nIdPerfil int)
AS
BEGIN
	DELETE FROM tbperfiles
		WHERE idperfil = @nIdPerfil;
END ;
