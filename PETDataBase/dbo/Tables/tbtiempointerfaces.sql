CREATE TABLE [dbo].[tbtiempointerfaces] (
    [ClaveInterfaz]            INT          NOT NULL,
    [FechaSiguienteEjecucion]  DATETIME     NOT NULL,
    [IntervaloEjecucion]       INT          NOT NULL,
    [TipoIntervalo]            VARCHAR (10) NOT NULL,
    [FechaEjecucionManual]     DATETIME     DEFAULT (NULL) NULL,
    [FechaHoraUltimaEjecucion] DATETIME     DEFAULT (NULL) NULL
);

