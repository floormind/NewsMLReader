using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NewsMLReader.Helper
{
    // I use this pattern to take advantage of some level of dependancy injection
    // when a public interface isnt available for the clas i wish to use
    // I inverse control to a proxy class which creates the interface for me
    // ideally, the interface is the injected into the class that is needed
    public class WebClientCreator : IWebClientCreator
    {
        public WebClient CreateWebClient()
        {
            return new WebClient();
        }
    }
}
