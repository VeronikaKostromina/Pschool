namespace AzureTrigger
{
    public class EmailConfiguration
    {
        public string ConnectionString { get; set; }
        public string Subject { get; set; }

        public string FromAddress { get; set; }

    }
}
