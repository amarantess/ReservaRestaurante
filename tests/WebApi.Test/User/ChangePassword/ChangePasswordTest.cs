using CommomTestUtilities.Requests;
using CommomTestUtilities.Tokens;
using FluentAssertions;
using ReservaRestaurante.Communication.Requests;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Test.User.ChangePassword
{
	public class ChangePasswordTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly string _password;
		private readonly HttpClient _httpClient;
		private readonly Guid _userIdentifier;

		public ChangePasswordTest(CustomWebApplicationFactory factory)
		{
			_httpClient = factory.CreateClient();
			_password = factory.GetPassword();
			_userIdentifier = factory.GetUserIdentifier();
		}

		[Fact]
		public async Task Success()
		{
			var request = RequestChangePasswordBuilder.Build();
			request.Password = _password;

			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.PutAsJsonAsync("users/change-password", request);

			response.StatusCode.Should().Be(HttpStatusCode.NoContent);
		}

		[Fact]
		public async Task Error_NewPassword_Empty()
		{
			var request = new RequestChangePassword
			{
				Password = _password,
				NewPassword = string.Empty
			};

			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.PutAsJsonAsync("users/change-password", request);

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
