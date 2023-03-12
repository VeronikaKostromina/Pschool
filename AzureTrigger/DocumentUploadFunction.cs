using System.Collections.Generic;
using System.IO;
using AzureTrigger.Email;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;


namespace AzureTrigger
{
    [StorageAccount("BlobConnectionString")]
    public class DocumentUploadFunction
    {
        private readonly ISendEmail sendEmail;
        private readonly ILogger<DocumentUploadFunction> logger;

        public DocumentUploadFunction(ISendEmail sendEmail, ILogger<DocumentUploadFunction> logger)
        {
            this.sendEmail = sendEmail;
            this.logger = logger;
        }

        [FunctionName("DocumentUploadFunction")]
        public void Run([BlobTrigger("students-container/{name}")] Stream myBlob,
            string name,
            IDictionary<string, string> metadata)
        {
            if (metadata.ContainsKey(MetadataConstants.OriginalFileName) == false
                || metadata.ContainsKey(MetadataConstants.Email) == false)
            {
                this.logger.LogInformation("There is no OriginalFileName OR Email in uploaded file Metadata object.");
                return;
            }

            sendEmail.NotifyUser(metadata[MetadataConstants.OriginalFileName], metadata[MetadataConstants.Email]);
        }
    }
}
