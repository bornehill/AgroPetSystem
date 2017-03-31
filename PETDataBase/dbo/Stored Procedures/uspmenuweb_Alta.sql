CREATE PROCEDURE dbo.uspmenuweb_Alta(
    @Menu varchar(100),
    @MenuUrl Varchar(250),
    @Padre INT,
    @Activo BIT,
    @CreacionUsuarioId Int)
AS	
BEGIN
	DECLARE @Nivel INT;
	DECLARE @Orden INT;
	
	select @Orden = dbo.ufn_MenuObtenerOrden(@Padre);
	select @Nivel = dbo.ufn_MenuObtenerNivel(@Padre);
	
	INSERT INTO tbmenuweb
	  (Menu    
	  ,MenuUrl    
	  ,Padre      
	  ,Nivel    
	  ,Orden      
	  ,Activo    
	  ,FechaCreacion    
	  ,CreacionUsuarioId    
	  ,FechaModificacion    
	  ,ModificacionUsuarioId)    
	 VALUES    
	  (@Menu    
	  ,@MenuUrl    
	  ,@Padre      
	  ,@Nivel    
	  ,@Orden      
	  ,@Activo  
	  ,GETDATE()    
	  ,@CreacionUsuarioId    
	  ,NULL    
	  ,NULL);
END ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS uspPerfiles_Actualiza */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
