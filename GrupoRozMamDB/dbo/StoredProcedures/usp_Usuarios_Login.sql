CREATE PROCEDURE [dbo].[usp_Usuarios_Login]
    @UsuarioOCorreo VARCHAR(150),
    @PasswordHash VARCHAR(300)
AS
BEGIN
    SET NOCOUNT ON;

    SELECT TOP (1)
        u.IdUsuario,
        u.NombreUsuario,
        u.Nombre,
        u.Apellidos,
        u.Correo,
        u.IdRol,
        r.Nombre AS NombreRol,
        u.Activo,
        u.FechaCreacion,
        u.FechaModificacion,
        u.FechaDesactivacion,
        u.Telefono,
        u.Direccion,
        u.TipoUsuario,
        u.PasswordHash
    FROM dbo.Usuarios u
    INNER JOIN dbo.Roles r ON r.IdRol = u.IdRol
    WHERE u.Activo = 1
      AND (u.NombreUsuario = @UsuarioOCorreo OR u.Correo = @UsuarioOCorreo)
      AND u.PasswordHash = @PasswordHash;
END
