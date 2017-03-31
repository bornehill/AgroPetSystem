CREATE TABLE [dbo].[tbimagenesarticulos] (
    [idimagenarticulo]  INT          IDENTITY (1, 1) NOT NULL,
    [idarticulo]        INT          DEFAULT (NULL) NULL,
    [fecharegistro]     VARCHAR (20) DEFAULT (NULL) NULL,
    [fechamodificacion] VARCHAR (20) DEFAULT (NULL) NULL,
    [idusuariocreo]     INT          DEFAULT (NULL) NULL,
    [idusuariomodifico] INT          DEFAULT (NULL) NULL,
    CONSTRAINT [Pk_IdImagenArticulo] PRIMARY KEY CLUSTERED ([idimagenarticulo] ASC)
);

