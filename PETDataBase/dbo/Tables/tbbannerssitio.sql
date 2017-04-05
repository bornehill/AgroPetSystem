CREATE TABLE [dbo].[tbbannerssitio] (
    [idbanner]       INT           IDENTITY (1, 1) NOT NULL,
    [descripcion]    VARCHAR (100) DEFAULT (NULL) NULL,
    [idusuariocreo]  INT           DEFAULT (NULL) NULL,
    [fechacreacion]  DATETIME      DEFAULT (NULL) NULL,
    [idusuariomodif] INT           DEFAULT (NULL) NULL,
    [fechamodif]     DATETIME      DEFAULT (NULL) NULL,
    [fechainiaplica] DATETIME      DEFAULT (NULL) NULL,
    [fechafinaplica] DATETIME      DEFAULT (NULL) NULL,
    CONSTRAINT [Pk_idbanner] PRIMARY KEY CLUSTERED ([idbanner] ASC),
    CONSTRAINT [idbanner_UNIQUE] UNIQUE NONCLUSTERED ([idbanner] ASC)
);

