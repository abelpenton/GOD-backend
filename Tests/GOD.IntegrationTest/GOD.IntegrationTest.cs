using System;
using System.Net.Http;
using System.Threading.Tasks;
using backend;
using Newtonsoft.Json;
using Xunit;


namespace GOD.IntegrationTests
{
    public class GODIntegrationTest : IClassFixture<TestFixture<Startup>>
    {
        private HttpClient Client;

        public GODIntegrationTest(TestFixture<Startup> fixture)
        {
            Client = fixture.Client;
        }


        [Fact]
        public async void GetPlayer1()
        {
            var request = "/api/v1/Game/GetPlayer/1";
            
            var response = await Client.GetAsync(request);

            Console.WriteLine(response.StatusCode);

            Assert.True(response.EnsureSuccessStatusCode().IsSuccessStatusCode);
            Assert.Equal(response.StatusCode, System.Net.HttpStatusCode.OK);
        }

        [Fact]
        public async void NewGame()
        {
            var request = new
            {
                Url = "/api/v1/Game/NewGame",
                Body = new
                {
                    Player1 = "Juan",
                    Player2 = "Pepe"
                }
            };

            var response = await Client.PostAsync(request.Url, ContentHelper.GetStringContent(request.Body));
            var value = await response.Content.ReadAsStringAsync();

            Console.WriteLine(response.StatusCode);
            Console.WriteLine(value);

            Assert.True(response.EnsureSuccessStatusCode().IsSuccessStatusCode);
            Assert.Equal(response.StatusCode, System.Net.HttpStatusCode.OK);
        }
    }
}
