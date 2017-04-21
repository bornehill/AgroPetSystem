

CREATE PROCEDURE uspPerfiles_Alta(@NombrePerfil varchar(100),
@FechaCreacion varchar(20), @IdUsuarioCreo int, @FechaUltModif varchar(20),
@Activo int)
AS
BEGIN
    
	IF  EXISTS (SELECT tp.idperfil FROM tbperfiles as tp WHERE tp.nombreperfil = @NombrePerfil)
		BEGIN
			SELECT 'EREl perfil especificado ya existe en la Base de Datos.' as MsgProceso;
		END	
    ELSE
		BEGIN
			INSERT INTO tbperfiles VALUES (@NombrePerfil, @FechaCreacion, @IdUsuarioCreo,
				@FechaUltModif, NULL, @Activo);    
            
		SELECT 'OKPerfil Registrado Correctamente.' as MsgProceso;
    END
END ;
