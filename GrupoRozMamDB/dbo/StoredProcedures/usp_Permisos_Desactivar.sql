CREATE PROCEDURE [dbo].[usp_Permisos_Desactivar]
    @IdPermiso INT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Permisos
    SET Activo = 0,
        FechaModificacion = SYSDATETIME(),
        FechaDesactivacion = SYSDATETIME()
    WHERE IdPermiso = @IdPermiso;
END
