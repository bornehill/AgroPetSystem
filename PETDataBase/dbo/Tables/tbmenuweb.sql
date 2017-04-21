CREATE TABLE [dbo].[tbmenuweb] (
    [MenuId]                INT           IDENTITY (1, 1) NOT NULL,
    [Menu]                  VARCHAR (100) NOT NULL,
    [MenuUrl]               VARCHAR (250) CONSTRAINT [DF__tbmenuweb__MenuU__1DF06171] DEFAULT (NULL) NULL,
    [Padre]                 INT           NOT NULL,
    [Nivel]                 INT           NOT NULL,
    [Orden]                 INT           NOT NULL,
    [Activo]                BIT           CONSTRAINT [DF__tbmenuweb__Activ__1EE485AA] DEFAULT (NULL) NULL,
    [FechaCreacion]         DATETIME      CONSTRAINT [DF__tbmenuweb__Fecha__1FD8A9E3] DEFAULT (NULL) NULL,
    [CreacionUsuarioId]     INT           CONSTRAINT [DF__tbmenuweb__Creac__20CCCE1C] DEFAULT (NULL) NULL,
    [FechaModificacion]     DATETIME      CONSTRAINT [DF__tbmenuweb__Fecha__21C0F255] DEFAULT (NULL) NULL,
    [ModificacionUsuarioId] INT           CONSTRAINT [DF__tbmenuweb__Modif__22B5168E] DEFAULT (NULL) NULL,
    CONSTRAINT [Pk_MenuId] PRIMARY KEY CLUSTERED ([MenuId] ASC)
);





