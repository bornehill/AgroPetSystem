﻿CREATE TABLE [dbo].[tbpoliticas_dsctos_volumen] (
    [ID_POL_DSCTO_VOL]          INT          IDENTITY (1, 1) NOT NULL,
    [POLITICA_DSCTO_VOLUMEN_ID] INT          NOT NULL,
    [NOMBRE]                    VARCHAR (50) NOT NULL,
    [ES_DSCTO_EXCLUSIVO]        CHAR (1)     DEFAULT ('N') NULL,
    [HABILITADA]                CHAR (1)     DEFAULT ('S') NULL,
    [ES_PERMANENTE]             CHAR (1)     DEFAULT ('N') NULL,
    [FECHA_INI_VIGENCIA]        DATE         DEFAULT (NULL) NULL,
    [FECHA_FIN_VIGENCIA]        DATE         DEFAULT (NULL) NULL,
    [USUARIO_CREADOR]           VARCHAR (31) DEFAULT (NULL) NULL,
    [FECHA_HORA_CREACION]       DATETIME     NOT NULL,
    [USUARIO_AUT_CREACION]      VARCHAR (31) DEFAULT (NULL) NULL,
    [USUARIO_ULT_MODIF]         VARCHAR (31) DEFAULT (NULL) NULL,
    [FECHA_HORA_ULT_MODIF]      DATETIME     NOT NULL,
    [USUARIO_AUT_MODIF]         VARCHAR (31) DEFAULT (NULL) NULL,
    CONSTRAINT [Pk_IdPolDsctoVol] PRIMARY KEY CLUSTERED ([ID_POL_DSCTO_VOL] ASC),
    CONSTRAINT [POLITICAS_DSCTOS_VOLUMEN_AK1] UNIQUE NONCLUSTERED ([NOMBRE] ASC)
);

