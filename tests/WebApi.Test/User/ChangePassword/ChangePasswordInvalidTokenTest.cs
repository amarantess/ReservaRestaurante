using CommomTestUtilities.Requests;
using CommomTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Test.User.ChangePassword
{
	public class ChangePasswordInvalidTokenTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _httpClient;

		public ChangePasswordInvalidTokenTest(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

		[Fact]
		public async Task Error_Token_Invalid()
		{
			var request = RequestChangePasswordBuilder.Build();

			AuthorizeRequest("TokenInvalid");

			var response = await _httpClient.PutAsJsonAsync("users/change-password", request);

			response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}


		[Fact]
		public async Task Error_Without_Token()
		{
			var request = RequestChangePasswordBuilder.Build();

			AuthorizeRequest(string.Empty);

			var response = await _httpClient.PutAsJsonAsync("users/change-password", request);

			response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}

		[Fact]
		public async Task Error_Token_With_User_NotFound()
		{
			var request = RequestChangePasswordBuilder.Build();

			var token = JwtTokenGeneratorBuilder.Build().Generate(Guid.NewGuid());

			AuthorizeRequest(token);

			var response = await _httpClient.PutAsJsonAsync("users/change-password", request);

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
