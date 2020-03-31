using System.Collections.Generic;

namespace ApiTest
{
    class Article
    {
        public string Section { get; set; }
        public string Title { get; set; }
        public string Abstract { get; set; }
        public string Url { get; set; }
        public string Byline { get; set; }
        public List<Multimedia> Multimedia { get; set; }
    }
}