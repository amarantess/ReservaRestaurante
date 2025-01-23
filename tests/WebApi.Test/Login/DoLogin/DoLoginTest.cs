using ReservaRestaurante.Communication.Requests;
using System.Net.Http.Json;
using System.Net;
using FluentAssertions;
using CommomTestUtilities.Requests;

namespace WebApi.Test.Login.DoLogin
{
	public class DoLoginTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _httpClient;

		private readonly string _email;
		private readonly string _password;

		public DoLoginTest(CustomWebApplicationFactory factory)
		{
			_httpClient = factory.CreateClient();

			_email = factory.GetEmail();
			_password = factory.GetPassword();
		}

		[Fact]
		public async Task Success()
		{
			var request = new RequestLogin
			{
				Email = _email,
				Password = _password
			};

			var response = await _httpClient.PostAsJsonAsync("login", request);

			response.StatusCode.Should().Be(HttpStatusCode.OK);
		}

		[Fact]
		public async Task Error_Invalid_Login()
		{
			var request = RequestLoginBuilder.Build();

			var response = await _httpClient.PostAsJsonAsync("login", request);

			response.StatusCode.Should().Be(HttpStatusCode.Unauthorized);
		}
	}
}
