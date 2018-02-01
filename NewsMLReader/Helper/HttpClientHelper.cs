using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace NewsMLReader.Helper
{
    //using Generics because I am not sure what type of content will be passed to these methods.
    public class HttpClientHelper<T> : IHttpClientHelper<T>
    {
        private static readonly string BaseUri = "http://localhost:63518/api/";
        private string fullPath = "";
        public async Task<T> PostObjectAsync<TParam>(string path, TParam content, string accessToken)
        {
            using (var httpClient = new HttpClient())
            {
                try
                {
                    SetBaseUri(httpClient, accessToken, path);

                    var serialisezContent = CreateHttpContent(content);

                    var httpResponse = await httpClient.PostAsync(fullPath, serialisezContent);

                    if (!httpResponse.IsSuccessStatusCode) throw new Exception("Problem accessing the api");

                    return JsonConvert.DeserializeObject<T>(GetResult(httpResponse));
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        private void SetBaseUri(HttpClient httpClient, string accessToken, string path)
        {
            //httpClient.BaseAddress = new Uri(BaseUri); 

            fullPath = $"http://localhost:63518/api/{path}";
            // assume the access token is used to set the bearer for the api, 
            // so that it stays private and can only be accessed if the token is provided along with the call

            //CODE FOR SETTING BEARER/ACCESSTOKEN//
        }

        private static string GetResult(HttpResponseMessage httpResponse)
        {
            if (!httpResponse.IsSuccessStatusCode) throw new Exception("Problem accessing the api");

            var content = httpResponse.Content;

            var jsonString = Task.Run(() => content.ReadAsStringAsync());
            jsonString.Wait();

            return jsonString.Result;
        }

        public ByteArrayContent CreateHttpContent<TParam>(TParam httpObject)
        {
            var content = JsonConvert.SerializeObject(httpObject);
            var buffer = System.Text.Encoding.UTF8.GetBytes(content);
            var byteContent = new ByteArrayContent(buffer);

            byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            return byteContent;
        }
    }
}
