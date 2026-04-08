CREATE PROCEDURE [dbo].[usp_Usuarios_Insertar]
    @NombreUsuario VARCHAR(150),
    @Nombre VARCHAR(150),
    @Apellidos VARCHAR(150),
    @Correo VARCHAR(150),
    @PasswordHash VARCHAR(300),
    @IdRol INT,
    @Activo BIT,
    @Telefono INT,
    @Direccion VARCHAR(MAX),
    @TipoUsuario VARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Usuarios (NombreUsuario, Nombre, Apellidos, Correo, PasswordHash, IdRol, Activo, FechaCreacion, FechaModificacion, FechaDesactivacion, Telefono, Direccion, TipoUsuario)
    OUTPUT INSERTED.IdUsuario
    VALUES (@NombreUsuario, @Nombre, @Apellidos, @Correo, @PasswordHash, @IdRol, @Activo, SYSDATETIME(), NULL, CASE WHEN @Activo = 0 THEN SYSDATETIME() ELSE NULL END, @Telefono, @Direccion, @TipoUsuario);
END
