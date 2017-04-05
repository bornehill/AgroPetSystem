CREATE PROCEDURE dbo.uspUsuarios_ValidaUsr(@vNombreUsuario varchar(20),
@vPassUsuario varchar(20))
AS
BEGIN
	IF EXISTS(SELECT idusuario FROM tbusuarios
				WHERE claveusr = @vNombreUsuario AND
					passwordusr =  @vPassUsuario)
	BEGIN				
		SELECT tu.idusuario, tu.idperfil, tu.nombreusr as nombre, tu.claveusr as claveusuario,
				tu.passwordusr as passwdusr, tu.activo, tp.nombreperfil
		FROM tbusuarios AS tu 
			INNER JOIN tbperfiles tp 
				ON tu.idperfil = tp.idperfil
        WHERE tu.claveusr = @vNombreUsuario AND
				tu.passwordusr = @vPassUsuario;
	END			
    ELSE
	BEGIN
		SELECT -11 as idusuario, -11 as idperfil, 'No Existe' as nombre, 
			'' as claveusuario, '' passwdusr, 0 as activo, '' nombreperfil
		FROM tbusuarios;
 	END
END
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS usp_GetBannersWeb */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_ENGINE_SUBSTITUTION' */ ;
