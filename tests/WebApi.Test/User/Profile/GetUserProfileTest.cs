using CommomTestUtilities.Tokens;
using FluentAssertions;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Net.Http.Headers;
using System.Text.Json;

namespace WebApi.Test.User.Profile
{
	public class GetUserProfileTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _httpClient;

		private readonly string _name;
		private readonly string _email;
		private readonly Guid _userIdentifier;

		public GetUserProfileTest(CustomWebApplicationFactory factory)
		{
			_httpClient = factory.CreateClient();

			_name = factory.GetName();
			_email = factory.GetEmail();
			_userIdentifier = factory.GetUserIdentifier();
		}

		[Fact]
		public async Task Success()
		{
			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.GetAsync("users/get/profile");

			response.StatusCode.Should().Be(HttpStatusCode.OK);

			await using var responseBody = await response.Content.ReadAsStreamAsync();

			var responseData = await JsonDocument.ParseAsync(responseBody);

			responseData.RootElement.GetProperty("name").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_name);
			responseData.RootElement.GetProperty("email").GetString().Should().NotBeNullOrWhiteSpace().And.Be(_email);
		}

		private void AuthorizeRequest(string token)
		{
			if (string.IsNullOrWhiteSpace(token))
				return;

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
	}
}
