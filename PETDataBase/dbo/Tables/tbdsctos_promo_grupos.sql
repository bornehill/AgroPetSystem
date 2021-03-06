﻿CREATE TABLE [dbo].[tbdsctos_promo_grupos] (
    [ID_DSCTO_PROMO_GPO]      INT            IDENTITY (1, 1) NOT NULL,
    [DSCTO_PROMO_GRUPO_ID]    INT            NOT NULL,
    [POLITICA_DSCTO_PROMO_ID] INT            NOT NULL,
    [GRUPO_LINEA_ID]          INT            NOT NULL,
    [DESCUENTO]               DECIMAL (9, 6) DEFAULT ('0.000000') NOT NULL,
    CONSTRAINT [Pk_IdDsctoPromoGpo] PRIMARY KEY CLUSTERED ([ID_DSCTO_PROMO_GPO] ASC),
    CONSTRAINT [DSCTOS_PROMO_GRUPOS_AK1] UNIQUE NONCLUSTERED ([POLITICA_DSCTO_PROMO_ID] ASC, [GRUPO_LINEA_ID] ASC)
);

