CREATE PROCEDURE [dbo].[usp_Permisos_Actualizar]
    @IdPermiso INT,
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(200),
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;
    UPDATE dbo.Permisos
    SET Nombre = @Nombre,
        Descripcion = @Descripcion,
        FechaModificacion = SYSDATETIME(),
        Activo = @Activo,
        FechaDesactivacion = CASE
            WHEN @Activo = 1 THEN NULL
            ELSE ISNULL(FechaDesactivacion, SYSDATETIME())
        END
    WHERE IdPermiso = @IdPermiso;
END
