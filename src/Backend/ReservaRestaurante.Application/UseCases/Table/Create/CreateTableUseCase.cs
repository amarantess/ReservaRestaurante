using AutoMapper;
using FluentValidation.Results;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Communication.Responses;
using ReservaRestaurante.Domain.Repositories;
using ReservaRestaurante.Domain.Repositories.Table;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace ReservaRestaurante.Application.UseCases.Table.Create
{
	public class CreateTableUseCase : ICreateTableUseCase
	{
		private readonly ITableReadOnlyRepository _readOnlyRepository;
		private readonly ITableWriteOnlyRepository _writeOnlyRepository;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public CreateTableUseCase(
			ITableReadOnlyRepository readOnlyRepository,
			ITableWriteOnlyRepository writeOnlyRepository,
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_readOnlyRepository = readOnlyRepository;
			_writeOnlyRepository = writeOnlyRepository;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task<ResponseTable> Execute(RequestCreateTable request)
		{
			//Validar request
			await Validate(request);

			//Mapear
			var table = _mapper.Map<Domain.Entities.Table>(request);

			//Adicionar no DB
			await _writeOnlyRepository.Add(table);

			//Persistir
			await _unitOfWork.Commit();

			//Retornar uma resposta
			return _mapper.Map<ResponseTable>(table);
		}

		private async Task Validate(RequestCreateTable request)
		{
			var validator = new CreateTableValidator();
			var result = validator.Validate(request);

			//Validar se já existe uma mesa com o mesmo número da request
			var exist = await _readOnlyRepository.ExistTableWithNumber(request.Number);
			if (exist)
				result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.TABLE_NUMBER_ALREADY_REGISTERED));

			if (!result.IsValid)
			{
				var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

				throw new ErrorOnValidationException(errorMessages);
			}
		}
	}
}
