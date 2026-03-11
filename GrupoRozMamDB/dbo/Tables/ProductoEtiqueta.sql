CREATE TABLE [dbo].[ProductoEtiqueta] (
    [IdProducto] INT NOT NULL,
    [IdEtiqueta] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdProducto] ASC, [IdEtiqueta] ASC),
    CONSTRAINT [FK_PE_Etiqueta] FOREIGN KEY ([IdEtiqueta]) REFERENCES [dbo].[Etiquetas] ([IdEtiqueta]),
    CONSTRAINT [FK_PE_Producto] FOREIGN KEY ([IdProducto]) REFERENCES [dbo].[Productos] ([IdProducto])
);

