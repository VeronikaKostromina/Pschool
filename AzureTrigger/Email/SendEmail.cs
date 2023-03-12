using System;
using System.Threading;
using Azure;
using Azure.Communication.Email;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace AzureTrigger.Email
{
    public class SendEmail : ISendEmail
    {
        private readonly ILogger logger;
        private readonly EmailConfiguration configuration;

        public SendEmail(ILogger<SendEmail> logger, IOptions<EmailConfiguration> options)
        {
            this.logger = logger;
            this.configuration = options.Value;
        }

        public void NotifyUser(string fileName, string email)
        {
            var emailClient = new EmailClient(configuration.ConnectionString);

            var emailContent = new EmailContent(configuration.Subject)
            {
                PlainText = $"Your document {fileName} was successfully uploaded",
            };

            var emailMessage = new EmailMessage(configuration.FromAddress, email, emailContent);

            try
            {
                var sendEmailResult = emailClient.Send(WaitUntil.Completed, emailMessage, CancellationToken.None);

                if (sendEmailResult.HasCompleted)
                {
                    logger.LogInformation($"Email sent, MessageId = {sendEmailResult.Id}");
                }
                else
                {
                    logger.LogError($"Failed to send email.");
                    return;
                }
            }
            catch (Exception ex)
            {
                logger.LogError($"Error in sending email, {ex}");
            }
        }
    }
}
