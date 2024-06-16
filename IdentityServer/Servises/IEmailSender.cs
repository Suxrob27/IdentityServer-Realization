namespace IdentityServer.Servises
{
    public interface IEmailSender
    {
        Task SendAsync(string from, string to, string title, string body);
    }
}
