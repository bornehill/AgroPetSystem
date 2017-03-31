CREATE TABLE [dbo].[tbclientes_web] (
    [userId]               INT             IDENTITY (1, 1) NOT NULL,
    [NOMBRE]               VARCHAR (100)   NOT NULL,
    [CONTACTO1]            VARCHAR (50)    DEFAULT (NULL) NULL,
    [ESTATUS]              CHAR (1)        DEFAULT ('A') NULL,
    [CAUSA_SUSP]           VARCHAR (100)   DEFAULT (NULL) NULL,
    [LIMITE_CREDITO]       DECIMAL (15, 2) DEFAULT ('0.00') NOT NULL,
    [MONEDA_ID]            INT             NOT NULL,
    [COND_PAGO_ID]         INT             NOT NULL,
    [TIPO_CLIENTE_ID]      INT             DEFAULT (NULL) NULL,
    [USUARIO_CREADOR]      VARCHAR (31)    DEFAULT (NULL) NULL,
    [FECHA_HORA_CREACION]  DATETIME        NOT NULL,
    [USUARIO_ULT_MODIF]    VARCHAR (31)    DEFAULT (NULL) NULL,
    [FECHA_HORA_ULT_MODIF] DATETIME        NOT NULL,
    CONSTRAINT [Pk_UserId] PRIMARY KEY CLUSTERED ([userId] ASC)
);

