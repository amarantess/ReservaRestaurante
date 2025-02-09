using CommomTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;

namespace WebApi.Test.Table.Delete
{
	public class DeleteTableTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _httpClient;
		private readonly Guid _userIdentifier;

		public DeleteTableTest(CustomWebApplicationFactory factory)
		{
			_httpClient = factory.CreateClient();
			_userIdentifier = factory.GetUserIdentifier();
		}

		[Fact]
		public async Task Success()
		{
			long id = 1;
			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.DeleteAsync($"tables/{id}");

			response.StatusCode.Should().Be(HttpStatusCode.NoContent);
		}

		[Fact]
		public async Task Table_NotFound()
		{
			long id = 0;
			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.DeleteAsync($"tables/{id}");

			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}

		private void AuthorizeRequest(string token)
		{
			if (string.IsNullOrWhiteSpace(token))
				return;

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
	}
}
