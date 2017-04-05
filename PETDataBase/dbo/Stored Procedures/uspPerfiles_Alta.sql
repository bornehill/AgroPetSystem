CREATE PROCEDURE dbo.uspPerfiles_Alta(@NombrePerfil varchar(100),
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
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS uspPerfiles_Elimina */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
