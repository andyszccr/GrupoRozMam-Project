CREATE TABLE [dbo].[Logs] (
    [IdLog]              INT           IDENTITY (1, 1) NOT NULL,
    [IdUsuario]          INT           NULL,
    [Descripcion]        VARCHAR (MAX) NULL,
    [Accion]             VARCHAR (200) NOT NULL,
    [TablaAfectada]      VARCHAR (100) NOT NULL,
    [FechaCreacion]      DATETIME      NULL,
    [FechaModificacion]  DATETIME      NULL,
    [FechaDesactivacion] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([IdLog] ASC),
    CONSTRAINT [FK_Log_Usuario] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario])
);

