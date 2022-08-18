namespace PypTask.Services
{
    public interface IEmailServices
    {
        bool SendEmail(string email, string subject, string message, string file, byte[] bytes);
    }
}
