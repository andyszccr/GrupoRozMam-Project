CREATE PROCEDURE [dbo].[usp_Usuarios_Actualizar]
    @IdUsuario INT,
    @NombreUsuario VARCHAR(150),
    @Nombre VARCHAR(150),
    @Apellidos VARCHAR(150),
    @Correo VARCHAR(150),
    @IdRol INT,
    @Activo BIT,
    @Telefono INT,
    @Direccion VARCHAR(MAX),
    @TipoUsuario VARCHAR(50)
AS
BEGIN
    UPDATE dbo.Usuarios
    SET NombreUsuario = @NombreUsuario,
        Nombre = @Nombre,
        Apellidos = @Apellidos,
        Correo = @Correo,
        IdRol = @IdRol,
        FechaModificacion = SYSDATETIME(),
        Activo = @Activo,
        FechaDesactivacion = CASE
            WHEN @Activo = 1 THEN NULL
            ELSE ISNULL(FechaDesactivacion, SYSDATETIME())
        END,
        Telefono = @Telefono,
        Direccion = @Direccion,
        TipoUsuario = @TipoUsuario
    WHERE IdUsuario = @IdUsuario;
END
