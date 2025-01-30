using Bogus;
using ReservaRestaurante.Communication.Requests;

namespace CommomTestUtilities.Requests
{
	public class RequestChangePasswordBuilder
	{
		public static RequestChangePassword Build(int passwordLength = 10)
		{
			return new Faker<RequestChangePassword>()
				.RuleFor(user => user.Password, (f) => f.Internet.Password())
				.RuleFor(user => user.NewPassword, (f) => f.Internet.Password(passwordLength));
		}
	}
}
