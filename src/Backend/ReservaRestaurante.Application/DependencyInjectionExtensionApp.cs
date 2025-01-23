using Microsoft.Extensions.DependencyInjection;
using ReservaRestaurante.Application.Services.AutoMapper;
using ReservaRestaurante.Application.Services.Criptography;
using ReservaRestaurante.Application.UseCases.Login.DoLogin;
using ReservaRestaurante.Application.UseCases.User.Register;

namespace ReservaRestaurante.Application
{
	public static class DependencyInjectionExtensionApp
	{
		public static void AddApplication(this IServiceCollection services)
		{
			AddPasswordEncripter(services);
			AddAutoMapper(services);
			AddUseCases(services);
		}

		private static void AddAutoMapper(IServiceCollection services)
		{
			services.AddScoped(option => new AutoMapper.MapperConfiguration(options =>
			{
				options.AddProfile(new AutoMapping());
			}).CreateMapper());
		}

		private static void AddUseCases(IServiceCollection services)
		{
			services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
			services.AddScoped<IDoLoginUseCase, DoLoginUseCase>();
		}

		private static void AddPasswordEncripter(IServiceCollection services)
		{
			services.AddScoped(option => new PasswordEncripter());
		}
	}
}
