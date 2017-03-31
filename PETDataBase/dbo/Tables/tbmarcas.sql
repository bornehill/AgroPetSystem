CREATE TABLE [dbo].[tbmarcas] (
    [Id_Marca]        INT           IDENTITY (1, 1) NOT NULL,
    [Descripcion]     VARCHAR (200) DEFAULT (NULL) NULL,
    [UsuarioCreacion] VARCHAR (200) DEFAULT (NULL) NULL,
    [FechaCreacion]   DATETIME      DEFAULT (NULL) NULL,
    CONSTRAINT [Pk_IdMarca] PRIMARY KEY CLUSTERED ([Id_Marca] ASC)
);

