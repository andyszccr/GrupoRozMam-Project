CREATE PROCEDURE [dbo].[usp_Usuarios_Listar]
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
        u.TipoUsuario
    FROM dbo.Usuarios u
    INNER JOIN dbo.Roles r ON u.IdRol = r.IdRol
    ORDER BY u.IdUsuario DESC;
END
