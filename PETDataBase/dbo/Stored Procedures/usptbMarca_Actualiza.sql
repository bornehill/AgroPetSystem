CREATE PROCEDURE dbo.usptbMarca_Actualiza(
    @Id_Marca INT,
    @Descripcion VARCHAR(100),
    @UsuarioCreacion VARCHAR(31),
    @FechaCreacion DATETIME)
AS	
BEGIN
	UPDATE tbmarcas
		SET
		Descripcion = @Descripcion, 
		UsuarioCreacion = @UsuarioCreacion, 
		FechaCreacion = @FechaCreacion		
		WHERE
		Id_Marca = @Id_Marca;
END
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS usptbMarca_Alta */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
