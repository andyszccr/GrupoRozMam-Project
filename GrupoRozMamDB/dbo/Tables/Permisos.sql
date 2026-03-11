CREATE TABLE [dbo].[Permisos] (
    [IdPermiso]          INT           IDENTITY (1, 1) NOT NULL,
    [Nombre]             VARCHAR (100) NOT NULL,
    [Descripcion]        VARCHAR (200) NOT NULL,
    [FechaCreacion]      DATETIME      NULL,
    [FechaModificacion]  DATETIME      NULL,
    [FechaDesactivacion] DATETIME      NULL,
    [Activo]             BIT           NOT NULL,
    PRIMARY KEY CLUSTERED ([IdPermiso] ASC)
);

