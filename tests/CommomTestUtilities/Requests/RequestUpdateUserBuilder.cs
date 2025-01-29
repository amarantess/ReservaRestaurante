using Bogus;
using ReservaRestaurante.Communication.Requests;

namespace CommomTestUtilities.Requests
{
	public class RequestUpdateUserBuilder
	{
		public static RequestUpdateUser Build()
		{
			return new Faker<RequestUpdateUser>()
				.RuleFor(user => user.Name, (f) => f.Person.FirstName)
				.RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name));
		}
	}
}
