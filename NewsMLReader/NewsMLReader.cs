using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NewsMLReader.Helper;

namespace NewsMLReader
{
    public class NewsMlReader : INewsMlReader
    {
        public readonly string FtpPath;
        public readonly string Username;
        public readonly string Password;

        public IWebClientCreator WebClientCreator { get; set; }
        public ICredentials Credentials { get; set; }
        public readonly IHttpClientHelper<string> HttpHttpClientHelper;

        public NewsMlReader(string ftpPath, string username, string password)
        {
            FtpPath = ftpPath;
            Username = username;
            Password = password;


            // assume this is done through some sort of dependancy injection
            WebClientCreator = new WebClientCreator();
            Credentials = new NetworkCredential(this.Username, this.Password);
            HttpHttpClientHelper = new HttpClientHelper<string>();
        }

        // This will return the content of the FTP file to the calling service
        // it can either be windows service, an external api, console application, mvc application
        public async Task<string> ReadFromFtp()
        {
            try
            {
                if(string.IsNullOrEmpty(FtpPath)) throw new ArgumentException("Ftp path is null");

                var request = WebClientCreator.CreateWebClient();
                request.Credentials = Credentials;

                var newFileData = await request.DownloadDataTaskAsync(FtpPath);
                
               var fileString = Encoding.UTF8.GetString(newFileData);

                return fileString;
            }
            catch (Exception e)
            {
                // Assume we have a logging mechanism here
                // maybe we are using Log4Net, we can log any exception we encounter to fle or db
                return null;
            }
        }

        // This will post the content of the ftp to a feed
        public async Task<bool> PostToFeed(string content)
        {
            try
            {
                if (string.IsNullOrEmpty(content)) throw new ArgumentException("Invalid arguement");
                var searchClassPath = $"values/post/{content}";

                //fake accessToken for the purpose of displaying what to do.
                const string accessToken = "test";

                var posted = await HttpHttpClientHelper.PostObjectAsync(searchClassPath, content, accessToken);

                return true;
            }
            catch (Exception e)
            {
                // Assume we have a logging mechanism here
                // maybe we are using Log4Net, we can log any exception we encounter to fle or db
                return false;
            }
        }
    }
}
