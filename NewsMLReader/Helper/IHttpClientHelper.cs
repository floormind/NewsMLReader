using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewsMLReader.Helper
{
    public interface IHttpClientHelper<T>
    {
        Task<T> PostObjectAsync<TParam>(string path, TParam content, string accessToken);
    }
}