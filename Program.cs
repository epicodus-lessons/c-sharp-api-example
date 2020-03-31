using System;
using System.Threading.Tasks;
using RestSharp;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace ApiTest
{
  class Program
  {
    static void Main()
    {
      var apiCallTask = ApiHelper.ApiCall("[YOUR-API-KEY-HERE]");
      var result = apiCallTask.Result;

      JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(result);

      // Grabbing and displayinng all ojects in the results array
      List<Article> articleList = JsonConvert.DeserializeObject<List<Article>>(jsonResponse["results"].ToString());

      foreach (Article article in articleList)
      {
        Console.WriteLine($"Section: {article.Section}");
        Console.WriteLine($"Title: {article.Title}");
        Console.WriteLine($"Abstract: {article.Abstract}");
        Console.WriteLine($"Url: {article.Url}");
        Console.WriteLine($"Byline: {article.Byline}");
      }

      // Grabbing and displaying num_results property in jsonResponse object and storing it in the MetaData class.
      MetaData metaData = JsonConvert.DeserializeObject<MetaData>(jsonResponse.ToString()); 
      Console.WriteLine($"Num_results: {metaData.Num_Results}");

      // Grabbing and displaying all objects in the multimedia array within the first object in the results array, and storing them in a List of the Multimedia type.
      List<Multimedia> multimediaList = JsonConvert.DeserializeObject<List<Multimedia>>(jsonResponse["results"][0]["multimedia"].ToString());
      foreach (Multimedia media in multimediaList)
      {
        Console.WriteLine("---------");
        Console.WriteLine($"MULTIMEDIA");
        Console.WriteLine($"Type: {media.Type}");
        Console.WriteLine($"SubType: {media.SubType}");
        Console.WriteLine($"Caption: {media.Caption}");
      }
    }
  }

  class ApiHelper
  {
    public static async Task<string> ApiCall(string apiKey)
    {
      RestClient client = new RestClient("https://api.nytimes.com/svc/topstories/v2");
      RestRequest request = new RestRequest($"home.json?api-key={apiKey}", Method.GET);
      var response = await client.ExecuteTaskAsync(request);
      return response.Content;
    }
  }
}