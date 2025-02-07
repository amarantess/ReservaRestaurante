using CommomTestUtilities.Tokens;
using FluentAssertions;
using System.Net.Http.Headers;
using System.Net;

namespace WebApi.Test.Table.List_Admin
{
	public class ListTableAdminTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _httpClient;
		private readonly Guid _userIdentifier;

		public ListTableAdminTest(CustomWebApplicationFactory factory)
		{
			_httpClient = factory.CreateClient();
			_userIdentifier = factory.GetUserIdentifier();
		}

		[Fact]
		public async Task Success()
		{
			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.GetAsync("tables/admin");

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
