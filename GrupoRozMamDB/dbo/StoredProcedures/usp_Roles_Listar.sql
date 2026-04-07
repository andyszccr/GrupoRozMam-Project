CREATE PROCEDURE [dbo].[usp_Roles_Listar]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IdRol, Nombre, Descripcion, FechaCreacion, FechaModificacion, FechaDesactivacion, Activo
    FROM dbo.Roles
    ORDER BY Nombre;
END
