using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

public class EmailNotiService
{
    private readonly string _smtpServer;
    private readonly int _smtpPort;
    private readonly string _smtpUser;
    private readonly string _smtpPassword;

    public EmailNotiService(string smtpServer, int smtpPort, string smtpUser, string smtpPassword)
    {
        _smtpServer = smtpServer;
        _smtpPort = smtpPort;
        _smtpUser = smtpUser;
        _smtpPassword = smtpPassword;
    }

    public async Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpo)
    {
        using var smtp = new SmtpClient(_smtpServer, _smtpPort)
        {
            Credentials = new NetworkCredential(_smtpUser, _smtpPassword),
            EnableSsl = true
        };

        var mensaje = new MailMessage
        {
            From = new MailAddress(_smtpUser),
            Subject = asunto,
            Body = cuerpo,
            IsBodyHtml = true
        };

        mensaje.To.Add(destinatario);

        await smtp.SendMailAsync(mensaje);
    }
}
