CREATE TABLE [dbo].[tblibresarticulos] (
    [Id_Art]          INT          NOT NULL,
    [Articulo_Id]     INT          DEFAULT (NULL) NULL,
    [Clasificacion]   VARCHAR (10) DEFAULT (NULL) NULL,
    [Proveedor]       VARCHAR (25) DEFAULT (NULL) NULL,
    [Marca]           VARCHAR (25) DEFAULT (NULL) NULL,
    [Tipo]            VARCHAR (25) DEFAULT (NULL) NULL,
    [Categoria]       VARCHAR (25) DEFAULT (NULL) NULL,
    [Familia]         VARCHAR (30) DEFAULT (NULL) NULL,
    [Activo]          CHAR (1)     DEFAULT (NULL) NULL,
    [Novedad]         CHAR (1)     DEFAULT (NULL) NULL,
    [Permiso_Exotico] VARCHAR (99) DEFAULT (NULL) NULL,
    [Visible_En_Web]  CHAR (1)     DEFAULT (NULL) NULL,
    CONSTRAINT [Pk_IdArtLibre] PRIMARY KEY CLUSTERED ([Id_Art] ASC)
);

