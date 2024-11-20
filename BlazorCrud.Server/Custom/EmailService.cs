using MailKit.Net.Smtp;
using MimeKit;

public class EmailService
{
    private readonly string smtpServer = "smtp.gmail.com";
    private readonly int smtpPort = 587;
    private readonly string smtpUser = "estefano.jurado.c52@gmail.com"; // Correo Gmail
    private readonly string smtpPassword = "nsmu wleg wogy hyip"; // Contraseña de aplicación

    public async Task EnviarCorreoRecuperacionAsync(string destinatario, string enlace)
    {
        var mensaje = new MimeMessage();
        mensaje.From.Add(new MailboxAddress("Soporte", smtpUser));
        mensaje.To.Add(new MailboxAddress("",destinatario));
        mensaje.Subject = "Recuperación de contraseña";
        mensaje.Body = new TextPart("plain")
        {
            Text = $"Haz clic en el siguiente enlace para restablecer tu contraseña: {enlace}"
        };

        using var cliente = new SmtpClient();
        try
        {
            await cliente.ConnectAsync(smtpServer, smtpPort, MailKit.Security.SecureSocketOptions.StartTls);
            await cliente.AuthenticateAsync(smtpUser, smtpPassword);
            await cliente.SendAsync(mensaje);
            await cliente.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            throw new Exception("Error al enviar el correo.", ex);
        }
    }
}