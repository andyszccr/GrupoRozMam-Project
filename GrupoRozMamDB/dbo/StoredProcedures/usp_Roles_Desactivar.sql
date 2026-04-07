CREATE PROCEDURE [dbo].[usp_Roles_Desactivar]
    @IdRol INT
AS
BEGIN
    UPDATE dbo.Roles
    SET Activo = 0,
        FechaModificacion = SYSDATETIME(),
        FechaDesactivacion = SYSDATETIME()
    WHERE IdRol = @IdRol;
END
