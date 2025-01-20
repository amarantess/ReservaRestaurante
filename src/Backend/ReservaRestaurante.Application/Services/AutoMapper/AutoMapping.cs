using AutoMapper;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Domain.Entities;

namespace ReservaRestaurante.Application.Services.AutoMapper
{
	public class AutoMapping : Profile
	{
		public AutoMapping()
		{
			RequestToDomain();
		}

		private void RequestToDomain()
		{
			CreateMap<RequestRegisterUser, User>()
				.ForMember(dest => dest.Password, options => options.Ignore());
		}
	}
}
