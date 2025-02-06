using CommomTestUtilities.Requests;
using CommomTestUtilities.Tokens;
using FluentAssertions;
using ReservaRestaurante.Communication.Requests;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Test.Table.Create
{
	public class CreateTableTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _httpClient;
		private readonly Guid _userIdentifier;

		public CreateTableTest(CustomWebApplicationFactory factory)
		{
			_httpClient = factory.CreateClient();
			_userIdentifier = factory.GetUserIdentifier();
		}

		[Fact]
		public async Task Success()
		{
			var request = RequestCreateTableBuilder.Build();

			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.PostAsJsonAsync("tables", request);

			response.StatusCode.Should().Be(HttpStatusCode.Created);
		}

		[Fact]
		public async Task Error_Capacity_Invalid()
		{
			var request = new RequestCreateTable
			{
				Number = 1,
				Capacity = 0
			};

			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.PostAsJsonAsync("tables", request);

			response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
		}

		[Fact]
		public async Task Error_Number_Invalid()
		{
			var request = new RequestCreateTable
			{
				Number = 0,
				Capacity = 1
			};

			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.PostAsJsonAsync("tables", request);

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
