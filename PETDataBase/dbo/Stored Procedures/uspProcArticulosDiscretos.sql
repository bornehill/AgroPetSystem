CREATE PROCEDURE dbo.uspProcArticulosDiscretos(@artDiscretoID INT, @clave VARCHAR(20), @articuloID INT, @tipo CHAR(1), @fecha DATETIME)
AS
BEGIN
	IF NOT EXISTS(SELECT 1 FROM tbArticulos_Discretos 
				WHERE ID_ART_DISC = @artDiscretoID)
	BEGIN
		INSERT INTO tbarticulos_discretos
		(
		ART_DISCRETO_ID,
		CLAVE,
		ARTICULO_ID,
		TIPO,
		FECHA)
		VALUES
		(@artDiscretoID,
        @clave,
		@articuloID,
		@tipo,
		@fecha);
    END
    ELSE
    BEGIN
		UPDATE tbarticulos_discretos
		SET
		CLAVE = @clave,
		ARTICULO_ID = @articuloID,
		TIPO = @tipo,
		FECHA = @fecha
		WHERE ART_DISCRETO_ID = @artDiscretoID;
    END;
END ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS uspProcClavesCatSec */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
