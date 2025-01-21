using CommomTestUtilities.Requests;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Net.Http.Json;

namespace WebApi.Test.User.Register
{
	public class RegisterUserTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _httpClient;

		public RegisterUserTest(CustomWebApplicationFactory factory) => _httpClient = factory.CreateClient();

		[Fact]
		public async Task Success()
		{
			var request = RequestRegisterUserBuilder.Build();

			var response = await _httpClient.PostAsJsonAsync("usuarios/registrar", request);

			response.StatusCode.Should().Be(HttpStatusCode.Created);
		}
	}
}
