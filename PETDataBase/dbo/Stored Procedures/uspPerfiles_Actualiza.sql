CREATE PROCEDURE dbo.uspPerfiles_Actualiza(@IdPerfil int, @NombrePerfil varchar(100),
@FechaUltModif varchar(20), @IdUsuarioUltModif int, @Activo int)
AS
BEGIN

	UPDATE tbperfiles SET nombreperfil = @NombrePerfil, 
		fechaultmodif = @FechaUltModif,
        idusuarioultmodif = @IdUsuarioUltModif,
        activo = @Activo
	WHERE idperfil = @IdPerfil;
        
    SELECT 'OKActualizacion de datos del perfil terminó correctamente.' AS MsgProceso;    
END ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS uspPerfiles_Alta */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
