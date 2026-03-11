CREATE TABLE [dbo].[RolPermiso] (
    [IdRol]     INT NOT NULL,
    [IdPermiso] INT NOT NULL,
    PRIMARY KEY CLUSTERED ([IdRol] ASC, [IdPermiso] ASC),
    CONSTRAINT [FK_RP_Permiso] FOREIGN KEY ([IdPermiso]) REFERENCES [dbo].[Permisos] ([IdPermiso]),
    CONSTRAINT [FK_RP_Rol] FOREIGN KEY ([IdRol]) REFERENCES [dbo].[Roles] ([IdRol])
);

