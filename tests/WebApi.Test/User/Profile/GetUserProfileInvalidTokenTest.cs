using CommomTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;

namespace WebApi.Test.User.Profile
{
	public class GetUserProfileInvalidTokenTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _httpClient;

		public GetUserProfileInvalidTokenTest(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

		[Fact]
		public async Task Error_Token_Invalid()
		{
			AuthorizeRequest("TokenInvalid");

			var response = await _httpClient.GetAsync("users/profile");

			response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}

		[Fact]
		public async Task Error_Without_Token()
		{
			AuthorizeRequest(string.Empty);

			var response = await _httpClient.GetAsync("users/profile");

			response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}

		[Fact]
		public async Task Error_Token_With_User_NotFound()
		{
			var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

			AuthorizeRequest(token);

			var response = await _httpClient.GetAsync("users/profile");

			response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}

		private void AuthorizeRequest(string token)
		{
			if (string.IsNullOrWhiteSpace(token))
				return;

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
	}
}
