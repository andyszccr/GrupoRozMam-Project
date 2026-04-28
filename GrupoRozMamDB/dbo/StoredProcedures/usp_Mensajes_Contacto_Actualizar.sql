CREATE PROCEDURE [dbo].[usp_Mensajes_Contacto_Actualizar]
    @IdMensaje INT,
    @NombreRemitente VARCHAR(150),
    @CorreoRemitente VARCHAR(150),
    @TelefonoRemitente VARCHAR(20) = NULL,
    @Asunto VARCHAR(200) = NULL,
    @Mensaje TEXT,
    @Estado BIT,
    @IdUsuario INT = NULL,
    @NotasInternas TEXT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    UPDATE dbo.Mensajes_Contacto
    SET
        NombreRemitente = @NombreRemitente,
        CorreoRemitente = @CorreoRemitente,
        TelefonoRemitente = @TelefonoRemitente,
        Asunto = @Asunto,
        Mensaje = @Mensaje,
        Estado = @Estado,
        IdUsuario = @IdUsuario,
        NotasInternas = @NotasInternas
    WHERE IdMensaje = @IdMensaje;
END
