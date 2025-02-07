using AutoMapper;
using ReservaRestaurante.Communication.Responses;
using ReservaRestaurante.Domain.Repositories.Table;

namespace ReservaRestaurante.Application.UseCases.Table.List
{
	public class ListTableUseCase : IListTableUseCase
	{
		private readonly ITableReadOnlyRepository _repository;
		private readonly IMapper _mapper;

		public ListTableUseCase(ITableReadOnlyRepository repository, IMapper mapper)
		{
			_repository = repository;
			_mapper = mapper;
		}

		public async Task<List<ResponseListTable>> Execute()
		{
			//Listar todas as mesas
			var tables = await _repository.ListTables();

			//Mapear para a response
			var response = _mapper.Map<List<ResponseListTable>>(tables);

			return response;
		}
	}
}
