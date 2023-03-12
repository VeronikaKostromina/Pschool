namespace AzureTrigger.Email
{
    public interface ISendEmail
    {
        void NotifyUser(string fileName, string email);
    }
}
