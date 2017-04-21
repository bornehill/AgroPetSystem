
CREATE PROCEDURE uspSeltbTiempoInterfaces(
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
