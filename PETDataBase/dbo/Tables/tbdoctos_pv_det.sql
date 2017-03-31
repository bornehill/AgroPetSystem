CREATE TABLE [dbo].[tbdoctos_pv_det] (
    [DOCTO_PV_DET_ID]       INT             IDENTITY (1, 1) NOT NULL,
    [DOCTO_PV_ID]           INT             DEFAULT (NULL) NULL,
    [CLAVE_ARTICULO]        VARCHAR (20)    DEFAULT (NULL) NULL,
    [ARTICULO_ID]           INT             DEFAULT (NULL) NULL,
    [UNIDADES]              DECIMAL (18, 5) DEFAULT (NULL) NULL,
    [UNIDADES_DEV]          DECIMAL (18, 5) DEFAULT (NULL) NULL,
    [PRECIO_UNITARIO]       DECIMAL (18, 6) DEFAULT (NULL) NULL,
    [PRECIO_UNITARIO_IMPTO] DECIMAL (18, 6) DEFAULT (NULL) NULL,
    [FPGC_UNITARIO]         DECIMAL (18, 6) DEFAULT (NULL) NULL,
    [PCTJE_DSCTO]           DECIMAL (9, 6)  DEFAULT (NULL) NULL,
    [PCTJE_COMIS]           DECIMAL (9, 6)  DEFAULT (NULL) NULL,
    [ROL]                   CHAR (1)        DEFAULT (NULL) NULL,
    [PRECIO_TOTAL_NETO]     DECIMAL (15, 2) DEFAULT (NULL) NULL,
    [PRECIO_MODIFICADO]     CHAR (1)        DEFAULT (NULL) NULL,
    [VENDEDOR_ID]           INT             DEFAULT (NULL) NULL,
    [POSICION]              INT             DEFAULT (NULL) NULL,
    [NOTAS]                 TEXT            NULL,
    CONSTRAINT [Pk_DoctoPvDetId] PRIMARY KEY CLUSTERED ([DOCTO_PV_DET_ID] ASC)
);

