CREATE TABLE [dbo].[tbimagenesarticulos] (
    [idimagenarticulo]  INT            IDENTITY (1, 1) NOT NULL,
    [idarticulo]        INT            CONSTRAINT [DF__tbimagene__idart__64B7E415] DEFAULT (NULL) NULL,
    [fecharegistro]     DATETIME       CONSTRAINT [DF__tbimagene__fecha__65AC084E] DEFAULT (NULL) NULL,
    [fechamodificacion] DATETIME       CONSTRAINT [DF__tbimagene__fecha__66A02C87] DEFAULT (NULL) NULL,
    [idusuariocreo]     INT            CONSTRAINT [DF__tbimagene__idusu__679450C0] DEFAULT (NULL) NULL,
    [idusuariomodifico] INT            CONSTRAINT [DF__tbimagene__idusu__688874F9] DEFAULT (NULL) NULL,
    [pathimagen]        NVARCHAR (300) NULL,
    CONSTRAINT [Pk_IdImagenArticulo] PRIMARY KEY CLUSTERED ([idimagenarticulo] ASC)
);



