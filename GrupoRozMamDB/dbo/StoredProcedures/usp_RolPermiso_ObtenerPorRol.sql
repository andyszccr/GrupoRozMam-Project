CREATE PROCEDURE [dbo].[usp_RolPermiso_ObtenerPorRol]
    @IdRol INT
AS
BEGIN
    SET NOCOUNT ON;
    SELECT IdPermiso 
    FROM dbo.RolPermiso 
    WHERE IdRol = @IdRol;
END
