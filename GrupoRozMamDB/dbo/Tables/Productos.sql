CREATE TABLE [dbo].[Productos] (
    [IdProducto]         INT             IDENTITY (1, 1) NOT NULL,
    [Nombre]             VARCHAR (150)   NOT NULL,
    [Descripcion]        VARCHAR (500)   NULL,
    [Codigo]             VARCHAR (50)    NULL,
    [IdMarca]            INT             NOT NULL,
    [IdCategoria]        INT             NOT NULL,
    [Precio]             DECIMAL (18, 2) NULL,
    [Activo]             BIT             NOT NULL,
    [FechaCreacion]      DATETIME        NULL,
    [FechaModificacion]  DATETIME        NULL,
    [FechaDesactivacion] DATETIME        NULL,
    [MetaTitle]          VARCHAR (200)   NULL,
    [MetaDescription]    VARCHAR (100)   NULL,
    [slug]               VARCHAR (100)   NOT NULL,
    [IdUsuario]          INT             NOT NULL,
    CONSTRAINT [PK__Producto__0988921076820AB5] PRIMARY KEY CLUSTERED ([IdProducto] ASC),
    CONSTRAINT [FK_Producto_Categoria] FOREIGN KEY ([IdCategoria]) REFERENCES [dbo].[Categorias] ([IdCategoria]),
    CONSTRAINT [FK_Producto_Marca] FOREIGN KEY ([IdMarca]) REFERENCES [dbo].[Marcas] ([IdMarca]),
    CONSTRAINT [FK_Productos_Usuarios] FOREIGN KEY ([IdUsuario]) REFERENCES [dbo].[Usuarios] ([IdUsuario])
);

