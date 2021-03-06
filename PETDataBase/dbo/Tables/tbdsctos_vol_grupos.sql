﻿CREATE TABLE [dbo].[tbdsctos_vol_grupos] (
    [ID_DSCTO_VOL_GPO]          INT             IDENTITY (1, 1) NOT NULL,
    [DSCTO_VOL_GRUPO_ID]        INT             NOT NULL,
    [POLITICA_DSCTO_VOLUMEN_ID] INT             NOT NULL,
    [GRUPO_LINEA_ID]            INT             NOT NULL,
    [UNIDADES]                  DECIMAL (18, 5) DEFAULT ('0.00000') NOT NULL,
    [DESCUENTO]                 DECIMAL (9, 6)  DEFAULT ('0.000000') NOT NULL,
    CONSTRAINT [Pk_IdDsctoVolGpo] PRIMARY KEY CLUSTERED ([ID_DSCTO_VOL_GPO] ASC),
    CONSTRAINT [DSCTOS_VOL_GRUPOS_AK1] UNIQUE NONCLUSTERED ([POLITICA_DSCTO_VOLUMEN_ID] ASC, [GRUPO_LINEA_ID] ASC, [UNIDADES] ASC)
);

