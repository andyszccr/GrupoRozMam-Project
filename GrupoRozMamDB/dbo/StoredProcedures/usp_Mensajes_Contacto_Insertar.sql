CREATE PROCEDURE [dbo].[usp_Mensajes_Contacto_Insertar]
    @NombreRemitente VARCHAR(150),
    @CorreoRemitente VARCHAR(150),
    @TelefonoRemitente VARCHAR(20) = NULL,
    @Asunto VARCHAR(200) = NULL,
    @Mensaje TEXT,
    @Estado BIT = 0,
    @IdUsuario INT = NULL,
    @NotasInternas TEXT = NULL
AS
BEGIN
    SET NOCOUNT ON;

    INSERT INTO dbo.Mensajes_Contacto
    (
        NombreRemitente,
        CorreoRemitente,
        TelefonoRemitente,
        Asunto,
        Mensaje,
        FechaEnvio,
        Estado,
        IdUsuario,
        NotasInternas
    )
    OUTPUT INSERTED.IdMensaje
    VALUES
    (
        @NombreRemitente,
        @CorreoRemitente,
        @TelefonoRemitente,
        @Asunto,
        @Mensaje,
        SYSDATETIME(),
        @Estado,
        @IdUsuario,
        @NotasInternas
    );
END
