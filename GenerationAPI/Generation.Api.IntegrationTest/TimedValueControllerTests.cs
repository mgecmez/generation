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
    public class TimedValueControllerTests
    {
        private readonly HttpClient _client;

        public TimedValueControllerTests()
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
            var request = new TimedValueDto
            {
                Timestamp = DateTime.Now,
                Good = true,
                Value = 3.50M
            };
            var content = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/timedValue", content);

            var actualStatusCode = response.StatusCode;
            var actualResult = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.Equal(expectedResult, actualResult);
            Assert.Equal(expectedStatusCode, actualStatusCode);
        }

        [Theory]
        [InlineData("/api/timedValue", "/api/timedValue")]
        public async Task Get_Should_Return_OK_With_Inserted_Timed_Value_When_Insert_Success(string postUrl, string getUrl)
        {
            var expectedResult = string.Empty;
            var expectedStatusCode = HttpStatusCode.OK;

            // Arrange-1
            var request = new TimedValueDto
            {
                Timestamp = DateTime.Now,
                Good = true,
                Value = 3.50M
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
            var getResultList = JsonConvert.DeserializeObject<List<TimedValueDto>>(actualGetResult);

            var insertedTimedValueExist = getResultList.Any(x => x.Value == request.Value);

            // Assert-2
            Assert.NotEmpty(getResultList);
            Assert.True(insertedTimedValueExist);
        }

        [Theory]
        [InlineData("/api/timedValue", "/api/timedValue")]
        public async Task Insert_GetAll_Update_Should_Return_Expected_Result(string postUrl, string getUrl)
        {
            #region Insert
            var expectedResult = string.Empty;
            var expectedStatusCode = HttpStatusCode.OK;

            // Arrange-1
            var request = new TimedValueDto
            {
                Timestamp = DateTime.Now,
                Good = true,
                Value = 3.50M
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
            var getResultList = JsonConvert.DeserializeObject<List<TimedValueDto>>(actualGetResult);

            var insertedTimedValueExist = getResultList.Any(c => c.Value == request.Value);

            // Assert-2
            Assert.NotEmpty(getResultList);
            Assert.True(insertedTimedValueExist);
            #endregion

            #region Update
            // Arrange-3
            var insertedTimedValue = getResultList.FirstOrDefault(c => c.Value == request.Value);
            var requestUpdate = new TimedValueDto
            {
                Timestamp = DateTime.Now.AddDays(-2),
                Good = false,
                Value = 1.0M,
                Id = insertedTimedValue.Id
            };
            var contentUpdate = new StringContent(JsonConvert.SerializeObject(requestUpdate), Encoding.UTF8, "application/json");

            // Act-3
            var responseUpdate = await _client.PutAsync(postUrl, contentUpdate);
            responseUpdate.EnsureSuccessStatusCode();
            var updateActualResult = await responseUpdate.Content.ReadAsAsync<TimedValueDto>();

            // Assert-3
            Assert.Equal(updateActualResult.Value, requestUpdate.Value);
            #endregion
        }
    }
}
