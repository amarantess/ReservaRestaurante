using Bogus;
using FluentValidation;
using ReservaRestaurante.Communication.Requests;

namespace CommomTestUtilities.Requests
{
	public class RequestPromoteBuilder : AbstractValidator<RequestPromoteUser>
	{
		public static RequestPromoteUser Build()
		{
			return new Faker<RequestPromoteUser>()
				.RuleFor(user => user.Email, (f) => f.Internet.Email());
		}
	}
}
