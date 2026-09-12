using LearnHub_Api.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;

namespace LearnHub_Api.Health
{
    public class MailProviderHealthCheck(IOptions<MailSettings> mailSetting) : IHealthCheck
    {
        private readonly MailSettings _mailSetting = mailSetting.Value;

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using var smtp = new SmtpClient();


                // ✅ تعديل 1 — تجاهل الـ SSL Certificate
                //smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;
                smtp.CheckCertificateRevocation = false;

                await smtp.ConnectAsync(_mailSetting.Host, _mailSetting.Port, SecureSocketOptions.StartTls, cancellationToken);
                await smtp.AuthenticateAsync(_mailSetting.Mail, _mailSetting.Password, cancellationToken);
                await smtp.DisconnectAsync(true, cancellationToken);

                return HealthCheckResult.Healthy();
            }
            catch (Exception ex)
            {
                return await Task.FromResult(HealthCheckResult.Unhealthy(exception: ex));
            }
        }
    }
}
