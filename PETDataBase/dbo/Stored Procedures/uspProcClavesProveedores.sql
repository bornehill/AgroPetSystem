CREATE PROCEDURE dbo.uspProcClavesProveedores(@cveprovID INT, @cveProv VARCHAR(20), @proveedorID INT, @rolCveProvID INT)
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM tbclaves_proveedores
		WHERE CLAVE_PROV_ID = @cveprovID AND PROVEEDOR_ID = @proveedorID)
	BEGIN
		INSERT INTO tbclaves_proveedores
		(
		CLAVE_PROV_ID,
		CLAVE_PROV,
		PROVEEDOR_ID,
		ROL_CLAVE_PROV_ID)
		VALUES
		(
		@cveprovID,
		@cveProv,
		@proveedorID,
		@rolCveProvID);
    END;
END ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS uspSelClientes_Web */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
