using CommomTestUtilities.Requests;
using CommomTestUtilities.Tokens;
using FluentAssertions;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Test.User.Promote
{
	public class PromoteUserTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _httpClient;
		private readonly string _email;
		private readonly Guid _userIdentifier;

		public PromoteUserTest(CustomWebApplicationFactory factory)
		{
			_httpClient = factory.CreateClient();
			_email = factory.GetEmail();
			_userIdentifier = factory.GetUserIdentifier();
		}

		[Fact]
		public async Task User_Already_Admin()
		{
			var request = RequestPromoteBuilder.Build();
			request.Email = _email;

			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.PutAsJsonAsync("users/promote", request);

			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
		}

		[Fact]
		public async Task User_NotFound()
		{
			var request = RequestPromoteBuilder.Build();

			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.PutAsJsonAsync("users/promote", request);

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
