CREATE PROCEDURE dbo.uspAsignarArticulosMenuWeb_Insertar(
    @MenId INT,
    @idGpo_Lin INT,
    @idLin_Art INT,
    @idArt INT)
AS	
BEGIN
	SELECT @idLin_Art=ID_LIN_ART FROM tbarticulos WHERE ID_ART = @idArt;
	SELECT @idGpo_Lin=ID_GRUPO_LIN FROM tblineas_articulos WHERE ID_LIN_ART = @idLin_Art;
	
	INSERT INTO tbRelMenuArticulos
		    (MenuId,
		     IdGrupo_Linea,
		     IdLinea_Articulo,
		     IdArticulo)
	VALUES (@MenId,
		@IdGpo_Lin,
		@IdLin_Art,
		@IdArt);
    END ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS uspDDLItems_Seleccion */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
