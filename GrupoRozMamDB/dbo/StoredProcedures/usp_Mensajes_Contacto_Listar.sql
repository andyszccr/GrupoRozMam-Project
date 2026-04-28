CREATE PROCEDURE [dbo].[usp_Mensajes_Contacto_Listar]
AS
BEGIN
    SET NOCOUNT ON;

    SELECT
        mc.IdMensaje,
        mc.NombreRemitente,
        mc.CorreoRemitente,
        mc.TelefonoRemitente,
        mc.Asunto,
        mc.Mensaje,
        mc.FechaEnvio,
        mc.Estado,
        mc.IdUsuario,
        u.NombreUsuario AS NombreUsuarioGestiona,
        mc.NotasInternas
    FROM dbo.Mensajes_Contacto mc
    LEFT JOIN dbo.Usuarios u ON u.IdUsuario = mc.IdUsuario
    ORDER BY mc.FechaEnvio DESC;
END
