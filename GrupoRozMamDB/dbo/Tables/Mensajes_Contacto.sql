CREATE TABLE [dbo].[Mensajes_Contacto] (
    [IdMensaje]         INT           IDENTITY (1, 1) NOT NULL,
    [NombreRemitente]   VARCHAR (150) NOT NULL,
    [CorreoRemitente]   VARCHAR (150) NOT NULL,
    [TelefonoRemitente] VARCHAR (20)  NULL,
    [Asunto]            VARCHAR (200) NULL,
    [Mensaje]           TEXT          NOT NULL,
    [FechaEnvio]        DATETIME      NOT NULL,
    [Estado]            BIT           NOT NULL,
    [IdUsuario]         INT           NULL,
    [NotasInternas]     TEXT          NULL,
    PRIMARY KEY CLUSTERED ([IdMensaje] ASC),
    CONSTRAINT [FK_Mensajes_Contacto_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario])
);

