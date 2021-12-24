using Generation.Dto;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Generation.Api.IntegrationTest
{
    public class PowerPlantControllerTests
    {
        private readonly HttpClient _client;

        public PowerPlantControllerTests()
        {
            var testServer = new TestServer(new WebHostBuilder()
                .UseStartup<TestStartup>()
                .UseEnvironment("Development"));
            _client = testServer.CreateClient();
        }

        [Fact]
        public async Task Post_Should_Return_OK_With_Empty_Response_When_Insert_Success()
        {
            var expectedResult = string.Empty;
            var expectedStatusCode = HttpStatusCode.OK;

            // Arrange
            var request = new PowerPlantDto
            {
                WebId = "WebId_1"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/powerPlant", content);

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        [Theory]
        [InlineData("/api/powerPlant", "/api/powerPlant")]
        public async Task Get_Should_Return_OK_With_Inserted_Powerplant_When_Insert_Success(string postUrl, string getUrl)
        {
            var expectedResult = string.Empty;
            var expectedStatusCode = HttpStatusCode.OK;

            // Arrange-1
            var request = new PowerPlantDto
            {
                WebId = "WebId_1"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act-1
            var response = await _client.PostAsync(postUrl, content);

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();

            // Assert-1
            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedStatusCode, actualStatusCode);

            // Act-2
            var responseGet = await _client.GetAsync(getUrl);

            var actualGetResult = await responseGet.Content.ReadAsStringAsync();
            var getResultList = JsonConvert.DeserializeObject<List<PowerPlantDto>>(actualGetResult);

            var insertedPowerPlantExist = getResultList.Any(x => x.WebId == request.WebId);

            // Assert-2
            Assert.NotEmpty(getResultList);
            Assert.True(insertedPowerPlantExist);
        }

        [Theory]
        [InlineData("/api/powerPlant", "/api/powerPlant")]
        public async Task Insert_GetAll_Update_Should_Return_Expected_Result(string postUrl, string getUrl)
        {
            #region Insert
            var expectedResult = string.Empty;
            var expectedStatusCode = HttpStatusCode.OK;

            // Arrange-1
            var request = new PowerPlantDto
            {
                WebId = "WebId_1"
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act-1
            var response = await _client.PostAsync(postUrl, content);

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();

            // Assert-1
            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedStatusCode, actualStatusCode);
            #endregion

            #region GetAll
            // Act-2
            var responseGet = await _client.GetAsync(getUrl);
            responseGet.EnsureSuccessStatusCode();

            var actualGetResult = await responseGet.Content.ReadAsStringAsync();
            var getResultList = JsonConvert.DeserializeObject<List<PowerPlantDto>>(actualGetResult);

            var insertedPowerPlantExist = getResultList.Any(c => c.WebId == request.WebId);

            // Assert-2
            Assert.NotEmpty(getResultList);
            Assert.True(insertedPowerPlantExist);
            #endregion

            #region Update
            // Arrange-3
            var insertedPowerPlant = getResultList.FirstOrDefault(c => c.WebId == request.WebId);
            var requestUpdate = new PowerPlantDto
            {
                WebId = "WebId_2",
                Id = insertedPowerPlant.Id
            };
            var contentUpdate = new StringContent(JsonConvert.SerializeObject(requestUpdate), Encoding.UTF8, "application/json");

            // Act-3
            var responseUpdate = await _client.PutAsync(postUrl, contentUpdate);
            responseUpdate.EnsureSuccessStatusCode();
            var updateActualResult = await responseUpdate.Content.ReadAsAsync<PowerPlantDto>();

            // Assert-3
            Assert.Equal(updateActualResult.WebId, requestUpdate.WebId);
            #endregion
        }
    }
}
