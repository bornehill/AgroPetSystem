CREATE TABLE [dbo].[tbmenuweb] (
    [MenuId]                INT           IDENTITY (1, 1) NOT NULL,
    [Menu]                  VARCHAR (100) NOT NULL,
    [MenuUrl]               VARCHAR (250) DEFAULT (NULL) NULL,
    [Padre]                 INT           NOT NULL,
    [Nivel]                 INT           NOT NULL,
    [Orden]                 INT           NOT NULL,
    [Activo]                TINYINT       DEFAULT (NULL) NULL,
    [FechaCreacion]         DATETIME      DEFAULT (NULL) NULL,
    [CreacionUsuarioId]     INT           DEFAULT (NULL) NULL,
    [FechaModificacion]     DATETIME      DEFAULT (NULL) NULL,
    [ModificacionUsuarioId] INT           DEFAULT (NULL) NULL,
    CONSTRAINT [Pk_MenuId] PRIMARY KEY CLUSTERED ([MenuId] ASC)
);

