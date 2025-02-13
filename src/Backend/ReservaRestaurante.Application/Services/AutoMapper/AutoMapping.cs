using AutoMapper;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;
using ReservaRestaurante.Domain.Entities;

namespace ReservaRestaurante.Application.Services.AutoMapper
{
	public class AutoMapping : Profile
	{
		public AutoMapping()
		{
			RequestToDomain();
			DomainToResponse();
		}

		private void RequestToDomain()
		{
			CreateMap<RequestRegisterUser, User>()
				.ForMember(dest => dest.Password, options => options.Ignore());

			CreateMap<RequestUpdateUser, User>();

			CreateMap<RequestCreateTable, Table>();

			CreateMap<RequestUpdateTable, Table>();

			CreateMap<RequestCreateReservation, Reservation>();
		}

		private void DomainToResponse()
		{
			CreateMap<User, ResponseUserProfile>();
			CreateMap<Table, ResponseTable>();
			CreateMap<Table, ResponseListTable>();
		}
	}
}
