using Bogus;
using ReservaRestaurante.Communication.Requests;

namespace CommomTestUtilities.Requests
{
	public class RequestRegisterUserBuilder
	{
		public static RequestRegisterUser Build(int passwordLength = 10)
		{
			return new Faker<RequestRegisterUser>()
				.RuleFor(user => user.Name, (f) => f.Person.FirstName)
				.RuleFor(user => user.Email, (f, user) => f.Internet.Email(user.Name))
				.RuleFor(user => user.Password, (f) => f.Internet.Password(passwordLength));
		}
	}
}
