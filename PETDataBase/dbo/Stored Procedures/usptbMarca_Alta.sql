CREATE PROCEDURE dbo.usptbMarca_Alta(
    @Id_Marca INT,
    @Descripcion VARCHAR(100),
    @UsuarioCreacion VARCHAR(31),
    @FechaCreacion Datetime,
    @Ret int OUT,
    @MsgProceso VARCHAR(100) OUT)
AS	
BEGIN
	INSERT INTO tbmarcas 
		(Id_Marca, 
		Descripcion, 
		UsuarioCreacion, 
		FechaCreacion
		)
		VALUES
		(@Id_Marca, 
		@Descripcion, 
		@UsuarioCreacion, 
		@FechaCreacion
		);
	
	SET @Ret = @@ROWCOUNT;
	SET @MsgProceso = '';
END
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS uspUpdtbTiempoInterfaces */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
