CREATE TABLE [dbo].[tbrelmenuarticulos] (
    [MenuId]           INT NOT NULL,
    [IdGrupo_Linea]    INT NOT NULL,
    [IdLinea_Articulo] INT NOT NULL,
    [IdArticulo]       INT NOT NULL,
    [Activo]           BIT CONSTRAINT [DF_tbrelmenuarticulos_Activo] DEFAULT ((1)) NOT NULL,
    CONSTRAINT [PK_tbrelmenuarticulos] PRIMARY KEY CLUSTERED ([MenuId] ASC, [IdGrupo_Linea] ASC, [IdLinea_Articulo] ASC, [IdArticulo] ASC)
);





