using ActionDemo;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace IntegrationTests
{
    [TestClass]
    public class WeatherforecastApiShould
    {
        private static WebApplicationFactory<Startup> _factory;

        [ClassInitialize]
        public static void ClassInit(TestContext testContext)
        {
            _factory = new WebApplicationFactory<Startup>();
        }

        [TestMethod]
        public async Task ReturnValidJson()
        {

            var client = _factory.CreateClient();
            var response = await client.GetAsync("weatherforecast");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            Assert.AreEqual("application/json; charset=utf-8", response.Content.Headers.ContentType?.ToString());

            var json = await response.Content.ReadAsStringAsync();
            var weatherforcast = JsonConvert.DeserializeObject<List<WeatherForecast>>(json);
            Assert.IsTrue(weatherforcast.Count > 0);
            Assert.IsTrue(!string.IsNullOrEmpty(weatherforcast[0].Summary));

        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            _factory.Dispose();
        }
    }
}
