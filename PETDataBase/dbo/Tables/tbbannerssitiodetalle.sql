CREATE TABLE [dbo].[tbbannerssitiodetalle] (
    [iddetbanners] INT           IDENTITY (1, 1) NOT NULL,
    [idbanner]     INT           DEFAULT (NULL) NULL,
    [titulo]       VARCHAR (100) DEFAULT (NULL) NULL,
    [subtitulo]    VARCHAR (250) DEFAULT (NULL) NULL,
    [pathimagen]   VARCHAR (150) DEFAULT (NULL) NULL,
    [orden]        INT           DEFAULT (NULL) NULL,
    CONSTRAINT [Pk_iddetbanners] PRIMARY KEY CLUSTERED ([iddetbanners] ASC)
);

