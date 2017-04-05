CREATE TABLE [dbo].[tbrelmenuarticulos] (
    [MenuId]           INT NOT NULL,
    [IdGrupo_Linea]    INT NOT NULL,
    [IdLinea_Articulo] INT NOT NULL,
    [IdArticulo]       INT NOT NULL,
    CONSTRAINT [Pk_RelMenuArt] PRIMARY KEY CLUSTERED ([MenuId] ASC, [IdGrupo_Linea] ASC, [IdLinea_Articulo] ASC, [IdArticulo] ASC)
);

