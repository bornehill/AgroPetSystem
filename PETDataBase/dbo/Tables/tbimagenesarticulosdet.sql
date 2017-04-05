CREATE TABLE [dbo].[tbimagenesarticulosdet] (
    [iddetimagenarticulo] INT           IDENTITY (1, 1) NOT NULL,
    [idimagenarticulo]    INT           DEFAULT (NULL) NULL,
    [pathimagen]          VARCHAR (300) DEFAULT (NULL) NULL,
    [fecharegistro]       VARCHAR (20)  DEFAULT (NULL) NULL,
    CONSTRAINT [Pk_iddetimagenarticulo] PRIMARY KEY CLUSTERED ([iddetimagenarticulo] ASC)
);

