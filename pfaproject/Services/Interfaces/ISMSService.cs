using Twilio.Rest.Api.V2010.Account;

namespace pfaproject.Services.Interfaces
{
    public interface ISMSService
    {
        Task<MessageResource> SendAsynch(string message, string to);
    }
}
