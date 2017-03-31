CREATE TABLE [dbo].[tbperfiles] (
    [idperfil]          INT           IDENTITY (1, 1) NOT NULL,
    [nombreperfil]      VARCHAR (100) DEFAULT (NULL) NULL,
    [fechacreacion]     VARCHAR (15)  DEFAULT (NULL) NULL,
    [idusuariocreo]     INT           DEFAULT (NULL) NULL,
    [fechaultmodif]     VARCHAR (15)  DEFAULT (NULL) NULL,
    [idusuarioultmodif] INT           DEFAULT (NULL) NULL,
    [activo]            INT           DEFAULT (NULL) NULL,
    CONSTRAINT [Pk_IdPerfil] PRIMARY KEY CLUSTERED ([idperfil] ASC)
);

