using MelodyFusion.DLL.Models.Request;
using MelodyFusion.DLL.Models.Response;
using MelodyFusion.DLL.Service;
using Moq.Protected;
using Moq;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MelodyFusion.DLL.Infrastructure;

namespace MelodyFusion_TestLayer
{
    public class SongServiceTests
    {
        private Mock<IHttpClientFactory> _mockFactory = null!;
        private SongService _songService;
        private LocalStorage _localStorage = null!;

        [SetUp]
        public void Setup()
        {
            _mockFactory = new Mock<IHttpClientFactory>();
            _localStorage = new LocalStorage();
            _localStorage.Add(LocalStorageKeys.AuthToken, "fake-token");
            _songService = new SongService(_mockFactory.Object, _localStorage);
        }

        [Test]
        public async Task GetSongsBySearchString_ReturnsSuccessfulResponse()
        {
            // Arrange

            var searchString = "test"; 

            var expectedResult = new List<SongDbResponse>()
            {
                new SongDbResponse { Id = "1", Name = "Test Song 1" },
                new SongDbResponse { Id = "2", Name = "Test Song 2" },
                new SongDbResponse { Id = "3", Name = "Test Song 3" }
            };

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(expectedResult, jsonSerializerSettings),
                Encoding.UTF8, "application/json");

            var httpResponse = new HttpResponseMessage
            {
                Content = httpContent,
                StatusCode = HttpStatusCode.OK
            };
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://baseaddress.com/")
            };

            _mockFactory.Setup(m => m.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Act
            var actualResult = await _songService.GetSongsBySearchString(searchString);

            // Assert
            Assert.IsNotNull(actualResult, "actualResult should not be null");
            Assert.AreEqual(expectedResult.Count, actualResult.Count);

            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i].Id, actualResult[i].Id);
                Assert.AreEqual(expectedResult[i].Name, actualResult[i].Name);
            }

            _mockFactory.Verify(m => m.CreateClient(It.IsAny<string>()), Times.Once);
            
        }
        
        [Test]

        public async Task GetRecomendation_ReturnsSuccessfulResponse()
        {
            // Arrange

            var sonrRecomedtaionRequest = new SongRecommendationRequest  { FirstSongId = "testId", SecondSongId = "Testid" } ;

            var expectedResult = new List<SongSpotifyResponse>
            {
                new SongSpotifyResponse { Id = "1", Name = "Spotify Song 1", PreviewUrl = "http://preview1.url", SongLink = "http://songlink1.url", ArtistName = "Artist 1" },
                new SongSpotifyResponse { Id = "2", Name = "Spotify Song 2", PreviewUrl = "http://preview2.url", SongLink = "http://songlink2.url", ArtistName = "Artist 2" },
                new SongSpotifyResponse { Id = "3", Name = "Spotify Song 3", PreviewUrl = "http://preview3.url", SongLink = "http://songlink3.url", ArtistName = "Artist 3" }
            };

            var jsonSerializerSettings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            };

            var httpContent = new StringContent(JsonConvert.SerializeObject(expectedResult, jsonSerializerSettings),
                Encoding.UTF8, "application/json");

            var httpResponse = new HttpResponseMessage
            {
                Content = httpContent,
                StatusCode = HttpStatusCode.OK
            };
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();

            mockHttpMessageHandler.Protected().Setup<Task<HttpResponseMessage>>(
                    "SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(httpResponse);

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://baseaddress.com/")
            };

            _mockFactory.Setup(m => m.CreateClient(It.IsAny<string>())).Returns(httpClient);

            // Act
            var actualResult = await _songService.GetRecommendation(sonrRecomedtaionRequest);

            // Assert
            Assert.IsNotNull(actualResult, "actualResult should not be null");
            Assert.AreEqual(expectedResult.Count, actualResult.Count);

            for (int i = 0; i < expectedResult.Count; i++)
            {
                Assert.AreEqual(expectedResult[i].Id, actualResult[i].Id);
                Assert.AreEqual(expectedResult[i].Name, actualResult[i].Name);
            }

            _mockFactory.Verify(m => m.CreateClient(It.IsAny<string>()), Times.Once);

        }
    }
}
