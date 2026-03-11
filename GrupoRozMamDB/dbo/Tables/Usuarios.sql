CREATE TABLE [dbo].[Usuarios] (
    [IdUsuario]               INT           IDENTITY (1, 1) NOT NULL,
    [NombreUsuario]           VARCHAR (150) NOT NULL,
    [Nombre]                  VARCHAR (150) NOT NULL,
    [Apellidos]               VARCHAR (150) NOT NULL,
    [Correo]                  VARCHAR (150) NOT NULL,
    [PasswordHash]            VARCHAR (300) NOT NULL,
    [IdRol]                   INT           NOT NULL,
    [Activo]                  BIT           NOT NULL,
    [FechaCreacion]           DATETIME      NULL,
    [FechaModificacion]       DATETIME      NULL,
    [FechaDesactivacion]      DATETIME      NULL,
    [Telefono]                INT           NOT NULL,
    [Direccion]               VARCHAR (MAX) NULL,
    [TipoUsuario]             VARCHAR (50)  NOT NULL,
    [ResetPasswordToken]      VARCHAR (255) NULL,
    [ResetPasswordExpiration] DATETIME      NULL,
    CONSTRAINT [PK__Usuarios__5B65BF975F32A816] PRIMARY KEY CLUSTERED ([IdUsuario] ASC),
    CONSTRAINT [FK_Usuario_Rol] FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Roles] ([IdRol])
);

