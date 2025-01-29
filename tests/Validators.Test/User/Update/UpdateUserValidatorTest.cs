using CommomTestUtilities.Requests;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.User.Register;
using ReservaRestaurante.Application.UseCases.User.Update;
using ReservaRestaurante.Exceptions;

namespace Validators.Test.User.Update
{
	public class UpdateUserValidatorTest
	{
		[Fact]
		public void Success()
		{
			//Arrange
			var validator = new UpdateUserValidator();

			var request = RequestUpdateUserBuilder.Build();

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeTrue();
		}

		[Fact]
		public void Error_Name_Empty()
		{
			// Arrange
			var validator = new UpdateUserValidator();

			var request = RequestUpdateUserBuilder.Build();
			request.Name = string.Empty;

			// Act
			var result = validator.Validate(request);

			// Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.NAME_EMPTY));
		}

		[Fact]
		public void Error_Email_Empty()
		{
			// Arrange
			var validator = new UpdateUserValidator();

			var request = RequestUpdateUserBuilder.Build();
			request.Email = string.Empty;

			// Act
			var result = validator.Validate(request);

			// Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_EMPTY));
		}

		[Fact]
		public void Error_Email_Invalid()
		{
			// Arrange
			var validator = new UpdateUserValidator();

			var request = RequestUpdateUserBuilder.Build();
			request.Email = "email.com";

			// Act
			var result = validator.Validate(request);

			// Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_INVALID));
		}
	}
}
