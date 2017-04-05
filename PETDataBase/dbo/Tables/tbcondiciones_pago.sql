﻿CREATE TABLE [dbo].[tbcondiciones_pago] (
    [ID_COND_PAGO]         INT            IDENTITY (1, 1) NOT NULL,
    [COND_PAGO_ID]         INT            NOT NULL,
    [NOMBRE]               VARCHAR (50)   NOT NULL,
    [PCTJE_DSCTO_PPAG]     DECIMAL (9, 6) DEFAULT ('0.000000') NOT NULL,
    [DIAS_PPAG]            SMALLINT       DEFAULT ('0') NULL,
    [ES_PREDET]            CHAR (1)       DEFAULT ('N') NULL,
    [USUARIO_CREADOR]      VARCHAR (31)   DEFAULT (NULL) NULL,
    [FECHA_HORA_CREACION]  DATETIME       NOT NULL,
    [USUARIO_AUT_CREACION] VARCHAR (31)   DEFAULT (NULL) NULL,
    [USUARIO_ULT_MODIF]    VARCHAR (31)   DEFAULT (NULL) NULL,
    [FECHA_HORA_ULT_MODIF] DATETIME       NOT NULL,
    [USUARIO_AUT_MODIF]    VARCHAR (31)   DEFAULT (NULL) NULL,
    CONSTRAINT [Pk_IdCondPago] PRIMARY KEY CLUSTERED ([ID_COND_PAGO] ASC),
    CONSTRAINT [CONDICIONES_PAGO_AK1] UNIQUE NONCLUSTERED ([NOMBRE] ASC)
);

