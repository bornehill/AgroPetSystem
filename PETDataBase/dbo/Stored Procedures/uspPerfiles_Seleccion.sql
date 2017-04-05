CREATE PROCEDURE dbo.uspPerfiles_Seleccion(@OpcionSeleccion int, @IdPerfil int, @Activo int )
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
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS uspProcArticulosDiscretos */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
