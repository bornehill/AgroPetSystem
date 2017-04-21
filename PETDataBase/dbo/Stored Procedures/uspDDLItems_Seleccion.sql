CREATE PROCEDURE uspDDLItems_Seleccion(
    @nOpcionSeleccion INT)
AS
BEGIN
	IF(@nOpcionSeleccion = 1)
	BEGIN
		SELECT idPerfil AS idValor, nombrePerfil AS DescripValor
		FROM tbPerfiles ORDER BY DescripValor;
	END
	
	IF(@nOpcionSeleccion = 2)
	BEGIN
		SELECT MenuId AS idValor, Menu AS DescripValor
		FROM tbmenuweb;
		/*where Padre = 1;*/
	END
	
	IF (@nOpcionSeleccion = 10)
	BEGIN
	  SELECT idusuario AS IdValor, claveusr AS DescripValor
	  FROM tbusuarios ORDER BY nombreusr;
	END
END ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS uspDesAsignarArticulosMenuWeb_Delete */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
