
CREATE PROCEDURE uspPerfiles_Seleccion(@OpcionSeleccion int, @IdPerfil int, @Activo int )
AS
BEGIN

	IF (@OpcionSeleccion = 1)
	BEGIN
		IF (((@Activo = 0) or (@Activo = 1)))
		BEGIN
			SELECT tPf.idperfil, tPf.nombreperfil, tPf.fechacreacion, tPf.idusuariocreo, tUsC.claveusr as UsuarioCreo,
					tPf.fechaultmodif, isnull(tPf.idusuarioultmodif, 0) as idusuarioultmodif, isnull(tUsM.claveusr, '') as UsuarioModif, 
                    CASE tPf.activo WHEN 1 THEN 'Activo' WHEN 2 THEN 'No Activo' END as Estatus
			FROM agropet_web.tbperfiles as tPf
				INNER JOIN tbusuarios tUsC
					ON tPf.idusuariocreo = tUsC.idusuario
				LEFT JOIN tbusuarios tUsM
					ON tPf.idusuarioultmodif = tUsM.idusuario
			WHERE tPf.idperfil = @IdPerfil AND tPf.activo = @Activo;
		END
		ELSE
		BEGIN
			SELECT tPf.idperfil, tPf.nombreperfil, tPf.fechacreacion, tPf.idusuariocreo, tUsC.claveusr as UsuarioCreo,
					tPf.fechaultmodif, isnull(tPf.idusuarioultmodif, 0) as idusuarioultmodif, isnull(tUsM.claveusro, '') as UsuarioModif, 
                    CASE tPf.activo WHEN 1 THEN 'Activo' WHEN 2 THEN 'No Activo' END as Estatus
			FROM agropet_web.tbperfiles as tPf
				INNER JOIN tbusuarios tUsC
					ON tPf.idusuariocreo = tUsC.idusuario
				LEFT JOIN tbusuarios tUsM
					ON tPf.idusuarioultmodif = tUsM.idusuario
			WHERE tPf.idperfil = @IdPerfil
        END
    END
    
    IF (@OpcionSeleccion = 2)
	BEGIN
		IF (((@Activo = 0) OR (@Activo = 1)))
		BEGIN
			SELECT tPf.idperfil, tPf.nombreperfil, tPf.fechacreacion, tPf.idusuariocreo, tUsC.claveusr as UsuarioCreo,
					tPf.fechaultmodif, isnull(tPf.idusuarioultmodif, 0) as idusuarioultmodif, isnull(tUsM.claveusr, '') as UsuarioModif,
                    CASE tPf.activo WHEN 1 THEN 'Activo' WHEN 2 THEN 'No Activo' END as Estatus
			FROM agropet_web.tbperfiles as tPf
				INNER JOIN tbusuarios tUsC
					ON tPf.idusuariocreo = tUsC.idusuario
				LEFT JOIN tbusuarios tUsM
					ON tPf.idusuarioultmodif = tUsM.idusuario
			WHERE tPf.activo = @Activo
			ORDER BY tPf.nombreperfil;
		END	
		ELSE
		BEGIN
			SELECT tPf.idperfil, tPf.nombreperfil, tPf.fechacreacion, tPf.idusuariocreo, tUsC.claveusr as UsuarioCreo,
					tPf.fechaultmodif, isnull(tPf.idusuarioultmodif, 0) as idusuarioultmodif, isnull(tUsM.claveusr, '') as UsuarioModif,
                    CASE tPf.activo WHEN 1 THEN 'Activo' WHEN 2 THEN 'No Activo' END as Estatus
			FROM agropet_web.tbperfiles as tPf
				INNER JOIN tbusuarios tUsC
					ON tPf.idusuariocreo = tUsC.idusuario
				LEFT JOIN tbusuarios tUsM
					ON tPf.idusuarioultmodif = tUsM.idusuario
			ORDER BY tPf.nombreperfil;        
        END
    END
END
