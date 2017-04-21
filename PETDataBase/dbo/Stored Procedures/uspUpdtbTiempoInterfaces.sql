
CREATE PROCEDURE uspUpdtbTiempoInterfaces(@OpcionSeleccion INT, @ClaveInterfaz INT, @FechaSiguienteEjecucion DATETIME, 
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
