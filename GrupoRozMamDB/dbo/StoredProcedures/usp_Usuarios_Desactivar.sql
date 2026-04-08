CREATE PROCEDURE [dbo].[usp_Usuarios_Desactivar]
    @IdUsuario INT
AS
BEGIN
    UPDATE dbo.Usuarios
    SET Activo = 0,
        FechaDesactivacion = SYSDATETIME()
    WHERE IdUsuario = @IdUsuario AND Activo = 1;
END
