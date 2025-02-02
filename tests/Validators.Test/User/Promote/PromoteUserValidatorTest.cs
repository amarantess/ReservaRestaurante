using CommomTestUtilities.Requests;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.User.Promote;
using ReservaRestaurante.Exceptions;

namespace Validators.Test.User.Promote
{
	public class PromoteUserValidatorTest
	{
		[Fact]
		public void Success()
		{
			var validator = new PromoteUserValidator();

			var request = RequestPromoteBuilder.Build();

			var result = validator.Validate(request);

			result.IsValid.Should().BeTrue();
		}

		[Fact]
		public void Error_Email_Empty()
		{
			var validator = new PromoteUserValidator();

			var request = RequestPromoteBuilder.Build();
			request.Email = string.Empty;

			var result = validator.Validate(request);

			result.IsValid.Should().BeFalse();

			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_EMPTY));
		}

		[Fact]
		public void Error_Email_Invalid()
		{
			var validator = new PromoteUserValidator();

			var request = RequestPromoteBuilder.Build();
			request.Email = "email.com";

			var result = validator.Validate(request);

			result.IsValid.Should().BeFalse();

			result.Errors.Should().ContainSingle()
				.And.Contain(e => e.ErrorMessage.Equals(ResourceMessagesException.EMAIL_INVALID));
		}
	}
}
