using CommomTestUtilities.Requests;
using CommomTestUtilities.Tokens;
using FluentAssertions;
using ReservaRestaurante.Communication.Requests;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace WebApi.Test.Reservation.Cancel
{
    public class CancelReservationTest : IClassFixture<CustomWebApplicationFactory>
	{
		private readonly HttpClient _httpClient;
		private readonly Guid _userIdentifier;

		public CancelReservationTest(CustomWebApplicationFactory factory)
		{
			_httpClient = factory.CreateClient();
			_userIdentifier = factory.GetUserIdentifier();
		}

		[Fact]
		public async Task Error_Table_Number_Invalid()
		{
			var request = new RequestCancelReservation
			{
				TableNumber = 0
			};

			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.PatchAsJsonAsync("reservation", request);

			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}

		[Fact]
		public async Task Error_Reservation_DateTime_Invalid()
		{
			var request = new RequestCancelReservation
			{
				ReservationDateTime = string.Empty
			};

			var token = JwtTokenGeneratorBuilder.Build().Generate(_userIdentifier);

			AuthorizeRequest(token);

			var response = await _httpClient.PatchAsJsonAsync("reservation", request);

			response.StatusCode.Should().Be(HttpStatusCode.NotFound);
		}

		private void AuthorizeRequest(string token)
		{
			if (string.IsNullOrWhiteSpace(token))
				return;

			_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
		}
	}
}
