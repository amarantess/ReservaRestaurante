using CommomTestUtilities.Requests;
using CommomTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Test.Table.Update
{
	public class UpdateTableTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _httpClient;
		private readonly Guid _userIdentifier;

		public UpdateTableTest(CustomWebApplicationFactory factory)
		{
			_httpClient = factory.CreateClient();
			_userIdentifier = factory.GetUserIdentifier();
		}

		[Fact]
		public async Task Success()
		{
			long id = 1;
			var request = RequestUpdateTableBuilder.Build();
			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.PutAsJsonAsync($"tables/{id}", request);

			response.StatusCode.Should().Be(HttpStatusCode.NoContent);
		}

		[Fact]
		public async Task Error_Status_Invalid()
		{
			long id = 1;
			var request = RequestUpdateTableBuilder.Build();
			request.Status = string.Empty;
			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.PutAsJsonAsync($"tables/{id}", request);

			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
		}

		private void AuthorizeRequest(string token)
		{
			if (string.IsNullOrWhiteSpace(token))
				return;

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
	}
}
