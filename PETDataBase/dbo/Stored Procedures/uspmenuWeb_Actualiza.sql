CREATE PROCEDURE dbo.uspmenuWeb_Actualiza(
	@Id INT    
	,@Menu varchar(100)    
	,@MenuUrl varchar(250)    
	,@Padre int    
	,@Activo bit    
	,@ModificacionUsuarioId int )
AS	
BEGIN
	DECLARE @Nivel INT;
	DECLARE @Orden INT;
	
	select @Orden=dbo.ufn_MenuObtenerOrden(@Padre);
	select @Nivel=dbo.ufn_MenuObtenerNivel(@Padre);
	
	UPDATE tbmenuweb   
	SET Menu = @Menu    
	,MenuUrl = @MenuUrl    
	,Padre = @Padre    
	,Nivel = @Nivel    
	,Orden = @Orden    
	,Activo = @Activo    
	,FechaModificacion = GETDATE()
	,ModificacionUsuarioId = @ModificacionUsuarioId    
	WHERE MenuId = @Id;
END ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS uspmenuweb_Alta */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
