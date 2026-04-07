CREATE PROCEDURE [dbo].[usp_Permisos_Listar]
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IdPermiso, Nombre, Descripcion, FechaCreacion, FechaModificacion, FechaDesactivacion, Activo
    FROM dbo.Permisos
    ORDER BY Nombre;
END
