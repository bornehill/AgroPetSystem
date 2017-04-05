CREATE TABLE [dbo].[tbdirsclientes] (
    [dirsClientesId] INT           IDENTITY (1, 1) NOT NULL,
    [clienteId]      INT           DEFAULT (NULL) NULL,
    [calle]          VARCHAR (100) DEFAULT (NULL) NULL,
    [numero]         INT           DEFAULT (NULL) NULL,
    [interior]       INT           DEFAULT (NULL) NULL,
    [colonia]        VARCHAR (100) DEFAULT (NULL) NULL,
    [estado]         INT           DEFAULT (NULL) NULL,
    [email]          VARCHAR (100) DEFAULT (NULL) NULL,
    [telefono]       VARCHAR (15)  DEFAULT (NULL) NULL,
    [celular]        VARCHAR (15)  DEFAULT (NULL) NULL,
    [codigoPostal]   INT           DEFAULT (NULL) NULL,
    [ciudad]         INT           DEFAULT (NULL) NULL,
    [pais]           INT           DEFAULT (NULL) NULL,
    CONSTRAINT [Pk_dirsClientesId] PRIMARY KEY CLUSTERED ([dirsClientesId] ASC)
);

