using ReservaRestaurante.Domain.Repositories;
using ReservaRestaurante.Domain.Repositories.Table;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace ReservaRestaurante.Application.UseCases.Table.Delete
{
	public class DeleteTableUseCase : IDeleteTableUseCase
	{
		private readonly ITableWriteOnlyRepository _writeOnlyRepository;
		private readonly ITableReadOnlyRepository _readOnlyRepository;
		private readonly IUnitOfWork _unitOfWork;

		public DeleteTableUseCase(
			ITableWriteOnlyRepository writeOnlyRepository,
			ITableReadOnlyRepository readOnlyRepository,
			IUnitOfWork unitOfWork)
		{
			_writeOnlyRepository = writeOnlyRepository;
			_readOnlyRepository = readOnlyRepository;
			_unitOfWork = unitOfWork;
		}

		public async Task Execute(long id)
		{
			var existTable = await _readOnlyRepository.ExistTableWithId(id);
			if (!existTable)
				throw new NotFoundException(ResourceMessagesException.TABLE_NOT_FOUND);

			await _writeOnlyRepository.Delete(id);

			await _unitOfWork.Commit();
		}
	}
}
