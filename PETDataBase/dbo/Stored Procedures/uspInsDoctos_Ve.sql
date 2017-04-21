
CREATE PROCEDURE uspInsDoctos_Ve(
    @cliID INT,
    @dirCli int,
    @importeTotal decimal(18,6),
    @totalImpuestos DECIMAL(15,2),
    @importeCobro decimal(15,2),
    @folioVe Varchar(9) OUT
    )
AS
BEGIN
    BEGIN TRAN T1;

	SELECT @folioVe='W'+right('00000000'+ISNULL(MAX(SUBSTRING(folio,2,10))+1,1),8) FROM tbdoctos_ve;
	INSERT INTO tbdoctos_ve
		    (
		     TIPO_DOCTO,
		     FOLIO,
		     FECHA,
		     CLIENTE_ID,
		     DIR_CLI_ID,
		     DIR_CONSIG_ID,
		     ALMACEN_ID,
		     MONEDA_ID,
		     TIPO_CAMBIO,
		     DSCTO_PCTJE,
		     DSCTO_IMPORTE,
		     ESTATUS,
		     APLICADO,
		     IMPORTE_NETO,
		     FLETES,
		     OTROS_CARGOS,
		     TOTAL_IMPUESTOS,
		     TOTAL_RETENCIONES,
		     TOTAL_FPGC,
		     PESO_EMBARQUE,
		     SISTEMA_ORIGEN,
		     COND_PAGO_ID,
		     PCTJE_DSCTO_PPAG,
		     VENDEDOR_ID,
		     PCTJE_COMIS,
		     IMPORTE_COBRO,
		     ES_CFD,
		     ENVIADO,
		     CARGAR_SUN,
		     TIPO_DSCTO,
		     FECHA_VIGENCIA_ENTREGA)
	VALUES (
		'P',
		@folioVe,
		GETDATE(),
		@cliID,
		@dirCli,
		@dirCli,
		19,
		1,
		1,
		0,
		0,
		'P',
		'S',
		@importeTotal,
		0,
		0,
		@totalImpuestos,
		0,
		0,
		0,
		'VE',
		656878,
		0,
		NULL,
		0,
		@importeCobro,
		'N',
		'S',
		'N',
		'P',
		((SELECT GETDATE() + 15)));
		
	/*LAST_INSERT_ID()*/
    COMMIT TRAN T1;
END ;
