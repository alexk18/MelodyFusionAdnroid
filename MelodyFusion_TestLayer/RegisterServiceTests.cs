using FluentAssertions;
using MelodyFusion.DLL.Models.Request;
using MelodyFusion.DLL.Models.Response;
using MelodyFusion.DLL.Service;
using Moq;
using Moq.Protected;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace MelodyFusion_TestLayer;

public class RegisterServiceTests
{
    private Mock<IHttpClientFactory> _mockFactory = null!;
    private RegisterService _registerService;

    [SetUp]
    public void Setup()
    {
        _mockFactory = new Mock<IHttpClientFactory>();
        _registerService = new RegisterService(_mockFactory.Object);
    }

    [Test]
    public async Task Register_WhenUserRegisteredSuccessfully_ReturnsSuccessfulResponse()
    {
        // Arrange

        var requestBody = new RegistrationRequest()
        {
            Firstname = "Test",
            Lastname = "Test",
            Email = "test@test.com",
            Password = "password",
            Role = 1,
            UserName = "Test",
        };
        
        var expectedResult = new RegistrationResponse()
        {
            Id = "Some registered user id",
            FirstName = "Test",
            LastName = "Test"
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
        var actualResult = await _registerService.Registr(requestBody);

        // Assert
        actualResult.Should().NotBeNull();
        actualResult.Should().BeEquivalentTo(expectedResult);

        _mockFactory.Verify(m => m.CreateClient(It.IsAny<string>()), Times.Once);
    }
}