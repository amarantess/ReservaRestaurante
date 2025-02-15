using AutoMapper;
using ReservaRestaurante.Communication.Responses;
using ReservaRestaurante.Domain.Repositories.Table;

namespace ReservaRestaurante.Application.UseCases.Table.List_Admin
{
	public class ListTableAdminUseCase : IListTableAdminUseCase
	{
		private readonly ITableReadOnlyRepository _repository;
		private readonly IMapper _mapper;

		public ListTableAdminUseCase(ITableReadOnlyRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<List<ResponseTable>> Execute()
		{
			//Listar todas as mesas
			var tables = await _repository.ListTables();

			//Mapear para a response
			return _mapper.Map<List<ResponseTable>>(tables);
		}
	}
}
