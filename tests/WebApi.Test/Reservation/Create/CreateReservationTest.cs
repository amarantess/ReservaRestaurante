using CommomTestUtilities.Requests;
using CommomTestUtilities.Tokens;
using FluentAssertions;
using ReservaRestaurante.Communication.Requests;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Test.Reservation.Create
{
	public class CreateReservationTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _httpClient;
		private readonly Guid _userIdentifier;
		private readonly int _tableNumber;

		public CreateReservationTest(CustomWebApplicationFactory factory)
		{
			_httpClient = factory.CreateClient();
			_userIdentifier = factory.GetUserIdentifier();
			_tableNumber = factory.GetTableNumber();
		}

		[Fact]
		public async Task Success()
		{
			var request = RequestCreateReservationBuilder.Build();
			request.TableNumber = _tableNumber;

			if(request.TableNumber != _tableNumber)
				request.TableNumber = _tableNumber;

			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.PostAsJsonAsync("reservation", request);

			response.StatusCode.Should().Be(HttpStatusCode.Created);
		}

		[Fact]
		public async Task Error_Table_Number_Invalid()
		{
			var request = new RequestCreateReservation
			{
				TableNumber = 0
			};

			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.PostAsJsonAsync("reservation", request);

			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task Error_People_Number_Invalid()
		{
			var request = new RequestCreateReservation
			{
				TableNumber = _tableNumber,
				PeopleNumber = 0
			};

			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.PostAsJsonAsync("reservation", request);

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
