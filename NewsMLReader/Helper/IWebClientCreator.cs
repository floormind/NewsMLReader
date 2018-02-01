using System.Net;

namespace NewsMLReader.Helper
{
    public interface IWebClientCreator
    {
        WebClient CreateWebClient();
    }
}