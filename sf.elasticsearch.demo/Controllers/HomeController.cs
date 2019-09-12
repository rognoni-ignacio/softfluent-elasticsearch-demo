using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using sf.elasticsearch.demo.Models;
using Elasticsearch.Net;
using System;
using sf.elasticsearch.demo.Domain;
using Nest;

namespace sf.elasticsearch.demo.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public async System.Threading.Tasks.Task<IActionResult> LowLevelNewIndexAsync()
        {
            // Connecting using additional settings
            var settings = new ConnectionConfiguration(new Uri("http://localhost:9200/"))
    .RequestTimeout(TimeSpan.FromMinutes(2));

            var lowlevelClient = new ElasticLowLevelClient(settings);

            // Indexing
            var person = new
            {
                firstName = "Ignacio",
                lastName = "Rognoni"
            };

            // Async method
            var asyncIndexResponse = await lowlevelClient.IndexAsync<StringResponse>("people", "1", PostData.Serializable(person));
            string responseString = asyncIndexResponse.Body;

            return Ok(responseString);
        }

        public IActionResult LowLevelSearchPersonByNameAsync(string name)
        {
            // Connecting using additional settings
            var settings = new ConnectionConfiguration(new Uri("http://localhost:9200/"))
    .RequestTimeout(TimeSpan.FromMinutes(2));

            var lowlevelClient = new ElasticLowLevelClient(settings);

            var searchResponse = lowlevelClient.Search<StringResponse>("people", PostData.Serializable(new
            {
                from = 0,
                size = 10,
                query = new
                {
                    multi_match = new
                    {
                        fields = "firstName",
                        query = name
                    }
                }
            }));

            var responseJson = searchResponse.Body;

            return Ok(responseJson);
        }

        public async System.Threading.Tasks.Task<IActionResult> HighLevelNewIndexAsync()
        {
            // Connecting using additional settings
            var settings = new ConnectionSettings(new Uri("http://localhost:9200")).DefaultIndex("people");

            var client = new ElasticClient(settings);

            // Indexing
            var person = new Person(2, "Sebastien", "Gissinger");

            var asyncIndexResponse = await client.IndexDocumentAsync(person);

            return Ok(asyncIndexResponse);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
