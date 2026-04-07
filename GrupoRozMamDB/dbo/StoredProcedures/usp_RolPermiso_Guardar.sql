CREATE PROCEDURE [dbo].[usp_RolPermiso_Guardar]
    @IdRol INT,
    @PermisosIds NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    
    -- Eliminamos los permisos anteriores del rol
    DELETE FROM dbo.RolPermiso WHERE IdRol = @IdRol;
    
    -- Insertamos los nuevos permisos enviados separados por comma
    IF @PermisosIds IS NOT NULL AND @PermisosIds <> ''
    BEGIN
        INSERT INTO dbo.RolPermiso (IdRol, IdPermiso)
        SELECT @IdRol, CAST(value AS INT)
        FROM STRING_SPLIT(@PermisosIds, ',');
    END
END
