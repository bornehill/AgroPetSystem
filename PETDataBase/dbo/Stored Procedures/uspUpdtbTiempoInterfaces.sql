CREATE PROCEDURE dbo.uspUpdtbTiempoInterfaces(@OpcionSeleccion INT, @ClaveInterfaz INT, @FechaSiguienteEjecucion DATETIME, 
	@IntervaloEjecucion INT, @TipoIntervalo NVARCHAR(20), @FechaEjecucionManual DATETIME, @FechaHoraUltimaEjecucion DATETIME)
AS
BEGIN
    IF(@OpcionSeleccion = NULL OR @OpcionSeleccion = 0)
    BEGIN
		SET @OpcionSeleccion = 1;
    END

	IF (@OpcionSeleccion = 1)
	Begin
		/* Actualización "normal" del tiempo de ejecución */
        UPDATE tbtiempointerfaces
			SET
			FechaSiguienteEjecucion = @FechaSiguienteEjecucion,
			IntervaloEjecucion = @IntervaloEjecucion,
			TipoIntervalo = @TipoIntervalo,
			FechaEjecucionManual = @FechaEjecucionManual,
			FechaHoraUltimaEjecucion = @FechaHoraUltimaEjecucion
			WHERE tbtiempointerfaces.ClaveInterfaz = @ClaveInterfaz;
	End
    
    IF (@OpcionSeleccion = 2)
	Begin
		/*Actualizar únicamente la fecha de ejecución manual*/
		Update tbtiempointerfaces
        Set
			FechaEjecucionManual = FechaEjecucionManual
		Where tbtiempointerfaces.ClaveInterfaz = @ClaveInterfaz;
	End
END
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS uspUsuarios_Seleccionar */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
