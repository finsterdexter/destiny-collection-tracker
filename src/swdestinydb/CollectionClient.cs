using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using swdestinydb.Models;

namespace swdestinydb
{
    public class CollectionClient
    {
		private const string BaseUrl = "http://swdestinydb.com";
		private const string RememberMeCookie = "QXBwQnVuZGxlXEVudGl0eVxVc2VyOlJtbHVjM1JsY2c9PToxNTE1MDk1NzM4OjVmZDM0NDNjMWVmYjVkNWRhOWQ3YTVkMjQ2YWFhMzA2ZWRhMmFjNjNmMmJiZmZjNDgyYTFkZjc0Njc5OGMzMDY";

		public async Task<IEnumerable<CollectionItem>> GetCollectionAsync()
        {
			var apiUrl = "/collection/";
			var baseUri = new Uri(BaseUrl);
			var cookies = new CookieContainer();
			if (RememberMeCookie == "xxx") throw new Exception("You need to set the value of the REMEMBERME cookie in CollectionClient.cs. Log into swdestinydb.com and find the REMEMBERME cookie and copy-paste the value into the RememberMeCookie value at the top of the CollectionClient class.");
			cookies.Add(baseUri, new Cookie("REMEMBERME", RememberMeCookie));

			string responseHtml;
			using (var handler = new HttpClientHandler() { CookieContainer = cookies })
			using (var client = new HttpClient(handler))
			{
				var result = await client.GetAsync(BaseUrl + apiUrl);
				result.EnsureSuccessStatusCode();
				responseHtml = await result.Content.ReadAsStringAsync();
			}

			var html = new HtmlAgilityPack.HtmlDocument();
			html.LoadHtml(responseHtml);
			var script = html.DocumentNode.Descendants().First(n => n.Name == "script" && n.InnerHtml.Contains("app.collection.init")).InnerText;

			// At this point, I'd use Jurassic to parse and get the data, but can't because Jurassic doesn't support .net core yet
			string collectionJson = script.Split('(', ')')[1];

			//var obj = JObject.Parse(collectionJson);
	        var results = JsonConvert.DeserializeObject<IEnumerable<CollectionItem>>(collectionJson, new CollectionItemJsonConverter());

			return results;

		}

		public async Task<IEnumerable<Card>> GetCardsAsync()
		{
			var apiUrl = "/api/public/cards/AW.json";

			using (var client = new HttpClient())
			{
				var response = await client.GetAsync(BaseUrl + apiUrl);
				response.EnsureSuccessStatusCode();

				var json = await response.Content.ReadAsStringAsync();
				var obj = JsonConvert.DeserializeObject<IEnumerable<Card>>(json);
				return obj;
			}
		}
    }
}
