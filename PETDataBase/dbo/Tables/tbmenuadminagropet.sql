CREATE TABLE [dbo].[tbmenuadminagropet] (
    [idmenu]            INT           NOT NULL,
    [nombremenu]        VARCHAR (80)  DEFAULT (NULL) NULL,
    [menuurl]           VARCHAR (300) DEFAULT (NULL) NULL,
    [idpadre]           INT           DEFAULT (NULL) NULL,
    [nivel]             INT           DEFAULT (NULL) NULL,
    [ordenh]            INT           DEFAULT (NULL) NULL,
    [activo]            INT           DEFAULT (NULL) NULL,
    [idpantallasistema] INT           DEFAULT (NULL) NULL,
    [espadre]           VARCHAR (2)   DEFAULT (NULL) NULL,
    CONSTRAINT [Pk_IdMenu] PRIMARY KEY CLUSTERED ([idmenu] ASC)
);

