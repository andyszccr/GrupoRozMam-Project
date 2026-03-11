CREATE TABLE [dbo].[ImagenesProducto] (
    [IdImagen]          INT           IDENTITY (1, 1) NOT NULL,
    [IdProducto]        INT           NOT NULL,
    [RutaImagen]        VARCHAR (300) NOT NULL,
    [EsPrincipal]       BIT           NOT NULL,
    [FechaCreacion]     DATETIME      NULL,
    [FechaModificacion] DATETIME      NULL,
    PRIMARY KEY CLUSTERED ([IdImagen] ASC),
    CONSTRAINT [FK_Imagen_Producto] FOREIGN KEY ([IdProducto]) REFERENCES [dbo].[Productos] ([IdProducto])
);

