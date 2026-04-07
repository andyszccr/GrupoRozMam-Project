CREATE PROCEDURE [dbo].[usp_Roles_ObtenerPorId]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IdRol, Nombre, Descripcion, FechaCreacion, FechaModificacion, FechaDesactivacion, Activo
    FROM dbo.Roles
    WHERE IdRol = @Id;
END
