CREATE PROCEDURE dbo.uspProcClavesCatSec(@nombreTabla VARCHAR(31), @elemID INT, @cve VARCHAR(20))
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM tbClaves_cat_sec
		WHERE Nombre_Tabla = @nombreTabla AND Elem_ID = @elemID)
	BEGIN
		INSERT INTO tbclaves_cat_sec
		(NOMBRE_TABLA,
		ELEM_ID,
		CLAVE)
		VALUES
		(@nombreTabla,
		@elemID,
		@cve);
    END
END ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS uspProcClavesProveedores */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
