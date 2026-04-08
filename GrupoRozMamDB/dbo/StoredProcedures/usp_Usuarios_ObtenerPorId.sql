CREATE PROCEDURE [dbo].[usp_Usuarios_ObtenerPorId]
    @IdUsuario INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT 
        u.IdUsuario, 
        u.NombreUsuario, 
        u.Nombre, 
        u.Apellidos, 
        u.Correo, 
        u.IdRol, 
        r.Nombre as NombreRol, 
        u.Activo, 
        u.FechaCreacion, 
        u.FechaModificacion, 
        u.FechaDesactivacion, 
        u.Telefono, 
        u.Direccion, 
        u.TipoUsuario,
        u.PasswordHash
    FROM dbo.Usuarios u
    INNER JOIN dbo.Roles r ON u.IdRol = r.IdRol
    WHERE u.IdUsuario = @IdUsuario;
END
