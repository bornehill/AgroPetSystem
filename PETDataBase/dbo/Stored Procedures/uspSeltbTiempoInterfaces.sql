CREATE PROCEDURE dbo.uspSeltbTiempoInterfaces(
@OpcionSeleccion INT,
@ClaveInterfaz INT,
@FechaSiguienteEjecucion DATETIME,
@IntervaloEjecucion INT,
@TipoIntervalo NVARCHAR(20),
@FechaEjecucionManual DATETIME,
@FechaHoraUltimaEjecucion DATETIME)
AS
BEGIN
	IF(@OpcionSeleccion = NULL OR @OpcionSeleccion = 0)
    BEGIN
		SET @OpcionSeleccion = 1;
    END;
    
    IF(@OpcionSeleccion = 1)
    BEGIN
		SELECT
			A.ClaveInterfaz,
            A.FechaSiguienteEjecucion,
            A.IntervaloEjecucion,
            A.TipoIntervalo,
            A.FechaEjecucionManual,
            A.FechaHoraUltimaEjecucion
		FROM tbtiempointerfaces AS A
        WHERE A.ClaveInterfaz = ISNULL(@ClaveInterfaz,A.ClaveInterfaz)
        AND A.FechaSiguienteEjecucion = ISNULL(@FechaSiguienteEjecucion, A.FechaSiguienteEjecucion)
        AND A.IntervaloEjecucion = ISNULL(@IntervaloEjecucion, A.IntervaloEjecucion)
		AND A.TipoIntervalo = ISNULL(@TipoIntervalo, A.TipoIntervalo)
		AND A.FechaEjecucionManual = ISNULL(@FechaEjecucionManual, A.FechaEjecucionManual)
		AND A.FechaHoraUltimaEjecucion = ISNULL(@FechaHoraUltimaEjecucion, A.FechaHoraUltimaEjecucion);
	END;
    ELSE 
	BEGIN
		IF (@OpcionSeleccion = 2)
		BEGIN
			SELECT
				A.ClaveInterfaz,
				A.FechaSiguienteEjecucion,
				A.IntervaloEjecucion,
				A.TipoIntervalo,
				A.FechaEjecucionManual,
				A.FechaHoraUltimaEjecucion
			FROM tbtiempointerfaces AS A
			WHERE A.FechaSiguienteEjecucion <= ISNULL(@FechaSiguienteEjecucion, A.FechaSiguienteEjecucion);
		END;
    END
END ;
/*!50003 SET sql_mode              = @saved_sql_mode */ ;
/*!50003 SET character_set_client  = @saved_cs_client */ ;
/*!50003 SET character_set_results = @saved_cs_results */ ;
/*!50003 SET collation_connection  = @saved_col_connection */ ;
/*!50003 DROP PROCEDURE IF EXISTS usptbArt_Seleccion */;
/*!50003 SET @saved_cs_client      = @@character_set_client */ ;
/*!50003 SET @saved_cs_results     = @@character_set_results */ ;
/*!50003 SET @saved_col_connection = @@collation_connection */ ;
/*!50003 SET character_set_client  = utf8 */ ;
/*!50003 SET character_set_results = utf8 */ ;
/*!50003 SET collation_connection  = utf8_general_ci */ ;
/*!50003 SET @saved_sql_mode       = @@sql_mode */ ;
/*!50003 SET sql_mode              = 'NO_AUTO_VALUE_ON_ZERO' */ ;
