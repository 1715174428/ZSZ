using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestIService;

namespace TestService
{
    public class NewsService : INewsService
    {
        public string AddNews(string title, string body)
        {
            
            return title+body;
        }
    }
}
