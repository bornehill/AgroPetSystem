CREATE TABLE [dbo].[tbdetaccesosxperfil] (
    [iddetaccesoxperfil] INT         IDENTITY (1, 1) NOT NULL,
    [idperfil]           INT         NOT NULL,
    [idmenu]             INT         NOT NULL,
    [tipoacceso]         VARCHAR (2) NOT NULL,
    CONSTRAINT [Pk_iddetaccesoxperfil] PRIMARY KEY CLUSTERED ([iddetaccesoxperfil] ASC)
);

