using Microsoft.Extensions.DependencyInjection;
using ReservaRestaurante.Application.Services.AutoMapper;
using ReservaRestaurante.Application.Services.DateTimeConverter;
using ReservaRestaurante.Application.UseCases.Login.DoLogin;
using ReservaRestaurante.Application.UseCases.Reservation.Cancel;
using ReservaRestaurante.Application.UseCases.Reservation.Create;
using ReservaRestaurante.Application.UseCases.Reservation.List;
using ReservaRestaurante.Application.UseCases.Table.Create;
using ReservaRestaurante.Application.UseCases.Table.Delete;
using ReservaRestaurante.Application.UseCases.Table.List;
using ReservaRestaurante.Application.UseCases.Table.List_Admin;
using ReservaRestaurante.Application.UseCases.Table.Update;
using ReservaRestaurante.Application.UseCases.User.ChangePassword;
using ReservaRestaurante.Application.UseCases.User.Profile;
using ReservaRestaurante.Application.UseCases.User.Promote;
using ReservaRestaurante.Application.UseCases.User.Register;
using ReservaRestaurante.Application.UseCases.User.Update;

namespace ReservaRestaurante.Application
{
	public static class DependencyInjectionExtensionApp
	{
		public static void AddApplication(this IServiceCollection services)
		{
			AddAutoMapper(services);
			AddUseCases(services);
			AddDateTimeConverter(services);
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
			services.AddScoped<IGetUserProfileUseCase, GetUserProfileUseCase>();
			services.AddScoped<IUpdateUserUseCase,  UpdateUserUseCase>();
			services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();
			services.AddScoped<IPromoteUserUseCase, PromoteUserUseCase>();

			services.AddScoped<ICreateTableUseCase, CreateTableUseCase>();
			services.AddScoped<IListTableUseCase, ListTableUseCase>();
			services.AddScoped<IListTableAdminUseCase, ListTableAdminUseCase>();
			services.AddScoped<IUpdateTableUseCase, UpdateTableUseCase>();
			services.AddScoped<IDeleteTableUseCase, DeleteTableUseCase>();

			services.AddScoped<ICreateReservationUseCase, CreateReservationUseCase>();
			services.AddScoped<IListReservationUseCase, ListReservationUseCase>();
			services.AddScoped<ICancelReservationUseCase, CancelReservationUseCase>();
		}

		private static void AddDateTimeConverter(IServiceCollection services)
		{
			services.AddScoped<IDateTimeConverter, Converter>();
		}
	}
}
