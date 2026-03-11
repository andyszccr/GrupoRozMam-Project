CREATE TABLE [dbo].[Etiquetas] (
    [IdEtiqueta]        INT            IDENTITY (1, 1) NOT NULL,
    [Nombre]            NVARCHAR (100) NOT NULL,
    [FechaCreacion]     DATETIME       NULL,
    [FechaModificacion] DATETIME       NULL,
    [Activo]            BIT            NOT NULL,
    PRIMARY KEY CLUSTERED ([IdEtiqueta] ASC)
);

