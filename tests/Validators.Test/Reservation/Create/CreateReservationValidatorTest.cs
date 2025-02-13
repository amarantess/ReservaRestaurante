using CommomTestUtilities.Requests;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.Reservation.Create;
using ReservaRestaurante.Exceptions;

namespace Validators.Test.Reservation.Create
{
	public class CreateReservationValidatorTest
	{
		[Fact]
		public void Success()
		{
			//Arrange
			var validator = new CreateReservationValidator();
			var request = RequestCreateReservationBuilder.Build();

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeTrue();
		}

		[Fact]
		public void Error_Table_Number_Invalid()
		{
			//Arrange
			var validator = new CreateReservationValidator();
			var request = RequestCreateReservationBuilder.Build();
			request.TableNumber = 0;

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.TABLE_NUMBER_INVALID));
		}

		[Fact]
		public void Error_People_Number_Invalid()
		{
			//Arrange
			var validator = new CreateReservationValidator();
			var request = RequestCreateReservationBuilder.Build();
			request.PeopleNumber = 0;

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.PEOPLE_NUMBER_INVALID));
		}

		[Fact]
		public void Error_Date_Format_Invalid()
		{
			//Arrange
			var validator = new CreateReservationValidator();
			var request = RequestCreateReservationBuilder.Build();
			request.ReservationDateTime = string.Empty;

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.DATE_TIME_FORMAT_INVALID));
		}

		[Fact]
		public void Error_Date_Invalid()
		{
			//Arrange
			var validator = new CreateReservationValidator();
			var request = RequestCreateReservationBuilder.Build();
			request.ReservationDateTime = "MM/dd/yyyy HH:mm";

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.DATE_TIME_FORMAT_INVALID));
		}
	}
}
