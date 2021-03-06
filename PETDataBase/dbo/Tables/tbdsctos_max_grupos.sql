﻿CREATE TABLE [dbo].[tbdsctos_max_grupos] (
    [ID_DSCTO_MAX_GPO_ID]      INT            IDENTITY (1, 1) NOT NULL,
    [DSCTO_MAX_GRUPO_ID]       INT            NOT NULL,
    [POLITICA_DSCTO_MAXIMO_ID] INT            NOT NULL,
    [GRUPO_LINEA_ID]           INT            NOT NULL,
    [DESCUENTO_MAXIMO]         DECIMAL (9, 6) DEFAULT ('0.000000') NOT NULL,
    CONSTRAINT [Pk_IdDsctoMaxGpoId] PRIMARY KEY CLUSTERED ([ID_DSCTO_MAX_GPO_ID] ASC),
    CONSTRAINT [DSCTOS_MAX_GRUPOS_AK1] UNIQUE NONCLUSTERED ([POLITICA_DSCTO_MAXIMO_ID] ASC, [GRUPO_LINEA_ID] ASC)
);

