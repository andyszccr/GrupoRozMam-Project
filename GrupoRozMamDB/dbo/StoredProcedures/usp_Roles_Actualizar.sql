CREATE PROCEDURE [dbo].[usp_Roles_Actualizar]
    @IdRol INT,
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(200),
    @Activo BIT
AS
BEGIN
    UPDATE dbo.Roles
    SET Nombre = @Nombre,
        Descripcion = @Descripcion,
        FechaModificacion = SYSDATETIME(),
        Activo = @Activo,
        FechaDesactivacion = CASE
            WHEN @Activo = 1 THEN NULL
            ELSE ISNULL(FechaDesactivacion, SYSDATETIME())
        END
    WHERE IdRol = @IdRol;
END
