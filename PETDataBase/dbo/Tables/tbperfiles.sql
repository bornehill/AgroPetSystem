CREATE TABLE [dbo].[tbperfiles] (
    [idperfil]          INT           IDENTITY (1, 1) NOT NULL,
    [nombreperfil]      VARCHAR (100) CONSTRAINT [DF__tbperfile__nombr__2F1AED73] DEFAULT (NULL) NULL,
    [fechacreacion]     DATETIME      CONSTRAINT [DF__tbperfile__fecha__300F11AC] DEFAULT (NULL) NULL,
    [idusuariocreo]     INT           CONSTRAINT [DF__tbperfile__idusu__310335E5] DEFAULT (NULL) NULL,
    [fechaultmodif]     DATETIME      CONSTRAINT [DF__tbperfile__fecha__31F75A1E] DEFAULT (NULL) NULL,
    [idusuarioultmodif] INT           CONSTRAINT [DF__tbperfile__idusu__32EB7E57] DEFAULT (NULL) NULL,
    [activo]            INT           CONSTRAINT [DF__tbperfile__activ__33DFA290] DEFAULT (NULL) NULL,
    CONSTRAINT [Pk_IdPerfil] PRIMARY KEY CLUSTERED ([idperfil] ASC)
);



