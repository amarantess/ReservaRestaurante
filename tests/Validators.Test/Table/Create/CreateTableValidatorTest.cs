using CommomTestUtilities.Requests;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.Table.Create;
using ReservaRestaurante.Exceptions;

namespace Validators.Test.Table.Create
{
	public class CreateTableValidatorTest
	{
		[Fact]
		public void Success()
		{
			//Arrange
			var validator = new CreateTableValidator();
			var request = RequestCreateTableBuilder.Build();

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeTrue();
		}

		[Fact]
		public void Error_Number_Equals_Zero()
		{
			//Arrange
			var validator = new CreateTableValidator();
			var request = RequestCreateTableBuilder.Build();
			request.Number = 0;

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.TABLE_NUMBER_INVALID));
		}

		[Fact]
		public void Error_Negative_Number()
		{
			//Arrange
			var validator = new CreateTableValidator();
			var request = RequestCreateTableBuilder.Build();
			request.Number = -1;

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.TABLE_NUMBER_INVALID));
		}

		[Fact]
		public void Error_Capacity_Invalid()
		{
			//Arrange
			var validator = new CreateTableValidator();
			var request = RequestCreateTableBuilder.Build();
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
			var validator = new CreateTableValidator();
			var request = RequestCreateTableBuilder.Build();
			request.Capacity = -1;

			//Act
			var result = validator.Validate(request);

			//Assert
			result.IsValid.Should().BeFalse();
			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.TABLE_CAPACITY_INVALID));
		}
	}
}
