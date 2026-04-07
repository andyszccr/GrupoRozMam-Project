CREATE PROCEDURE [dbo].[usp_Roles_Insertar]
    @Nombre VARCHAR(100),
    @Descripcion VARCHAR(200),
    @Activo BIT
AS
BEGIN
    SET NOCOUNT ON;
    INSERT INTO dbo.Roles (Nombre, Descripcion, FechaCreacion, FechaModificacion, FechaDesactivacion, Activo)
    OUTPUT INSERTED.IdRol
    VALUES (@Nombre, @Descripcion, SYSDATETIME(), NULL, NULL, @Activo);
END
