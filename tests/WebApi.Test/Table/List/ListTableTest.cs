using CommomTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;

namespace WebApi.Test.Table.List
{
	public class ListTableTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _httpClient;
		private readonly Guid _userIdentifier;

		public ListTableTest(CustomWebApplicationFactory factory)
		{
			_httpClient = factory.CreateClient();
			_userIdentifier = factory.GetUserIdentifier();
		}

		[Fact]
		public async Task Success()
		{
			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.GetAsync("tables");

			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}

		private void AuthorizeRequest(string token)
		{
			if (string.IsNullOrWhiteSpace(token))
				return;

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
	}
}
