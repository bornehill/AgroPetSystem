CREATE TABLE [dbo].[tbcajas_cajeros] (
    [TIPO_ACCESO] CHAR (1) DEFAULT ('O') NOT NULL,
    [CAJERO_ID]   INT      NOT NULL,
    [CAJA_ID]     INT      NOT NULL,
    CONSTRAINT [Pk_cajas_cajeros] PRIMARY KEY CLUSTERED ([TIPO_ACCESO] ASC, [CAJERO_ID] ASC, [CAJA_ID] ASC)
);

