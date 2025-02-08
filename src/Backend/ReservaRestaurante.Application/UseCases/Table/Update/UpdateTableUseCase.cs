using AutoMapper;
using FluentValidation.Results;
using ReservaRestaurante.Communication.Requests;
using ReservaRestaurante.Domain.Repositories;
using ReservaRestaurante.Domain.Repositories.Table;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace ReservaRestaurante.Application.UseCases.Table.Update
{
	public class UpdateTableUseCase : IUpdateTableUseCase
	{
		private readonly ITableReadOnlyRepository _readOnlyRepository;
		private readonly ITableUpdateOnlyRepository _updateOnlyRepository;
		private readonly IMapper _mapper;
		private readonly IUnitOfWork _unitOfWork;

		public UpdateTableUseCase(
			ITableReadOnlyRepository readOnlyRepository,
			ITableUpdateOnlyRepository updateOnlyRepository,
			IMapper mapper,
			IUnitOfWork unitOfWork)
		{
			_readOnlyRepository = readOnlyRepository;
			_updateOnlyRepository = updateOnlyRepository;
			_mapper = mapper;
			_unitOfWork = unitOfWork;
		}

		public async Task Execute(long tableId, RequestUpdateTable request)
		{
			//Validar request
			await Validate(tableId, request);

			//Recuperar as informações da mesa por id
			var table = await _updateOnlyRepository.GetById(tableId);

			//Mapear
			_mapper.Map(request, table);

			//Adicionar
			_updateOnlyRepository.Update(table);

			//Salvar
			await _unitOfWork.Commit();
		}

		public async Task Validate(long tableId, RequestUpdateTable request)
		{
			var validator = new UpdateTableValidator();
			var result = validator.Validate(request);

			//Validar se existe uma mesa com o id
			var existTableWithId = await _readOnlyRepository.ExistTableWithId(tableId);
			if(!existTableWithId)
				result.Errors.Add(new ValidationFailure(string.Empty, ResourceMessagesException.TABLE_NOT_FOUND));

			if (!result.IsValid)
			{
				var errorMessages = result.Errors.Select(e => e.ErrorMessage).ToList();

				throw new ErrorOnValidationException(errorMessages);
			}
		}
	}
}
