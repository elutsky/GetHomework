using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace HW.Infrastructure
{
    using System.IO;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Xml.Serialization;

    using HW.Infrastructure.DTO;
    using HW.Infrastructure.Entities;

    public class HomeworkProvider
    {
        public List<CourseHomework> GetHomeWork()
        {
            var httpClient = new HttpClient();
            var url = "http://www.2k.2017.yhd.edu2.org.il/BRPortal/br/page?p=xml&g=rss&u=73:63:68:6F:6F:6C:A:6D:61:6E:62:61:73:A:36:31:32:39:36:30:A:32:A:32";

            using (var request = new HttpRequestMessage(new HttpMethod("GET"), url))
            {
                //request.Headers.TryAddWithoutValidation("Connection", "keep-alive");
                //request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
                //request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                //request.Headers.TryAddWithoutValidation("Accept-Language", "en-US,en;q=0.9,he;q=0.8,ru;q=0.7");
                request.Headers.TryAddWithoutValidation("Accept", "text/html,application/xhtml+xml,application/xml;");
                request.Headers.TryAddWithoutValidation("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/73.0.3683.86 Safari/537.36");
                //request.Headers.TryAddWithoutValidation("Upgrade-Insecure-Requests", "1");
                //request.Headers.TryAddWithoutValidation("Cookie", "JSESSIONID=CFBD7A8B3DDE40EEE7420C3AA1CA0641; JSESSIONID=29342A04CFB940D81A7458CA7C62EA6B");

                var response = httpClient.SendAsync(request).Result;

                var content = response.Content.ReadAsStringAsync().Result;

                var xmls = new XmlSerializer(typeof(Rss));
                var rss =
                    (Rss)xmls.Deserialize(new StringReader(content));


                //var isok = rss.Channel.Item?.Capacity > 0;
                //var xmlStream = httpClient.GetStreamAsync();
                //var serializer = new XmlSerializer(typeof(Rss));
                //var rss = (Rss)serializer.Deserialize(xmlStream.Result);
                //then do whatever you want


                return new RssParserHelper().ParseHomework(rss.Channel.Item);

            }

            //httpClient.DefaultRequestHeaders.Add("Accept", "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,image/apng,*/*;q=0.8,application/signed-exchange;v=b3");
            ////httpClient.DefaultRequestHeaders.Add("Content-Type","text/plain; charset=UTF-8");
            ////var response = httpClient.GetAsync(url).Result;

            //httpClient.BaseAddress = new Uri(url);
            //httpClient.DefaultRequestHeaders.Accept.Clear();
            //httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/xml"));


            //var response = httpClient.GetAsync(url).Result;
            //var xmlStream = httpClient.GetStreamAsync(url);

            ////var responseString = response.Content.ReadAsStringAsync().Result;
            ////var xmls = new XmlSerializer(typeof(Rss));
            ////var rss  =
            ////    (Rss)xmls.Deserialize(new StringReader(responseString));


            //var buffer = Encoding.UTF8.GetBytes(responseString);
            //using (var stream = new MemoryStream(buffer))
            //{
            //var serializer = new XmlSerializer(typeof(Rss));
            //    var rss = (Rss)serializer.Deserialize(xmlStream.Result);
            //    //then do whatever you want
            ////}



            //var isOK = rss.Channel;

            //var streamTask = httpClient.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            //var repositories = serializer.ReadObject(await streamTask) as List<repo>;
        }
    }
}
