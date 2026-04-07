CREATE PROCEDURE [dbo].[usp_Permisos_Insertar]
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(200),
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Permisos (Nombre, Descripcion, FechaCreacion, FechaModificacion, FechaDesactivacion, Activo)
    OUTPUT INSERTED.IdPermiso
    VALUES (@Nombre, @Descripcion, SYSDATETIME(), NULL, NULL, @Activo);
END
