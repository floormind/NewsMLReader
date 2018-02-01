using System.Threading.Tasks;

namespace NewsMLReader
{
    public interface INewsMlReader
    {
        Task<string> ReadFromFtp();

        Task<bool> PostToFeed(string content);
    }
}