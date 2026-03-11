CREATE TABLE [dbo].[Categorias] (
    [IdCategoria]       INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]            VARCHAR (100) NOT NULL,
    [FechaCreacion]     DATETIME      NULL,
    [FechaModificacion] DATETIME      NULL,
    [Activo]            BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([IdCategoria] ASC)
);

