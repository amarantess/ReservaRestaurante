using CommomTestUtilities.Requests;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.Table.Create;
using ReservaRestaurante.Application.UseCases.Table.Update;
using ReservaRestaurante.Exceptions;

namespace Validators.Test.Table.Update
{
	public class UpdateTableValidatorTest
	{
		[Fact]
		public void Success()
		{
			//Arrange
			var validator = new UpdateTableValidator();
			var request = RequestUpdateTableBuilder.Build();

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeTrue();
		}

		[Fact]
		public void Error_Capacity_Invalid()
		{
			//Arrange
			var validator = new UpdateTableValidator();
			var request = RequestUpdateTableBuilder.Build();
			request.Capacity = 0;

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.TABLE_CAPACITY_INVALID));
		}

		[Fact]
		public void Error_Negative_Capacity()
		{
			//Arrange
			var validator = new UpdateTableValidator();
			var request = RequestUpdateTableBuilder.Build();
			request.Capacity = -1;

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.TABLE_CAPACITY_INVALID));
		}

		[Fact]
		public void Error_Status_Empty()
		{
			//Arrange
			var validator = new UpdateTableValidator();
			var request = RequestUpdateTableBuilder.Build();
			request.Status = string.Empty;

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.TABLE_STATUS_INVALID));
		}

		[Fact]
		public void Error_Status_Invalid()
		{
			//Arrange
			var validator = new UpdateTableValidator();
			var request = RequestUpdateTableBuilder.Build();
			request.Status = "status";

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.TABLE_STATUS_INVALID));
		}
	}
}
