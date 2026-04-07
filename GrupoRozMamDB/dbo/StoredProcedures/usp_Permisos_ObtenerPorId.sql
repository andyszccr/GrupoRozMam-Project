CREATE PROCEDURE [dbo].[usp_Permisos_ObtenerPorId]
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IdPermiso, Nombre, Descripcion, FechaCreacion, FechaModificacion, FechaDesactivacion, Activo
    FROM dbo.Permisos
    WHERE IdPermiso = @Id;
END
