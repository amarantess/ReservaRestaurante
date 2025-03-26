using CommomTestUtilities.Requests;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.Reservation.Cancel;
using ReservaRestaurante.Exceptions;

namespace Validators.Test.Reservation.Cancel
{
    public class CancelReservationValidatorTest
    {
		[Fact]
		public void Success()
		{
			//Arrange
			var validator = new CancelReservationValidator();
			var request = RequestCancelReservationBuilder.Build();

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeTrue();
		}

		[Fact]
		public void Error_Table_Number_Invalid()
		{
			//Arrange
			var validator = new CancelReservationValidator();
			var request = RequestCancelReservationBuilder.Build();
			request.TableNumber = 0;

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.TABLE_NUMBER_INVALID));
		}

		[Fact]
		public void Error_Date_Format_Invalid()
		{
			//Arrange
			var validator = new CancelReservationValidator();
			var request = RequestCancelReservationBuilder.Build();
			request.ReservationDateTime = string.Empty;

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.DATE_TIME_FORMAT_INVALID));
		}
	}
}
