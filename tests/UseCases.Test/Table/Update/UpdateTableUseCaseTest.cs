using CommomTestUtilities.Entities;
using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.Table.Update;
using ReservaRestaurante.Exceptions.ExceptionsBase;
using ReservaRestaurante.Exceptions;

namespace UseCases.Test.Table.Update
{
	public class UpdateTableUseCaseTest
	{
		[Fact]
		public async Task Success()
		{
			//Arrange
			long id = 1;
			var table = TableBuilder.Build();
			var request = RequestUpdateTableBuilder.Build();
			var useCase = CreateUseCase(table, id);

			//Act
			Func<Task> act = async () => await useCase.Execute(id, request);

			//Assert
			await act.Should().NotThrowAsync();

			table.Capacity.Should().Be(request.Capacity);
			table.Status.Should().Be(request.Status);
		}

		[Fact]
		public async Task Error_Capacity_Invalid()
		{
			//Arrange
			long id = 1;
			var table = TableBuilder.Build();
			var request = RequestUpdateTableBuilder.Build();
			request.Capacity = 0;
			var useCase = CreateUseCase(table, id);

			//Act
			Func<Task> act = async () => await useCase.Execute(id, request);

			//Assert
			(await act.Should().ThrowAsync<ErrorOnValidationException>())
				.Where(e => e.GetErrorMessages().Count == 1 &&
				e.GetErrorMessages().Contains(ResourceMessagesException.TABLE_CAPACITY_INVALID));
		}

		[Fact]
		public async Task Error_Status_Invalid()
		{
			//Arrange
			long id = 1;
			var table = TableBuilder.Build();
			var request = RequestUpdateTableBuilder.Build();
			request.Status = string.Empty;
			var useCase = CreateUseCase(table, id);

			//Act
			Func<Task> act = async () => await useCase.Execute(id, request);

			//Assert
			(await act.Should().ThrowAsync<ErrorOnValidationException>())
				.Where(e => e.GetErrorMessages().Count == 1 &&
				e.GetErrorMessages().Contains(ResourceMessagesException.TABLE_STATUS_INVALID));
		}

		[Fact]
		public async Task Error_Table_NotFound()
		{
			//Arrange
			long id = 0;
			var table = TableBuilder.Build();
			var request = RequestUpdateTableBuilder.Build();
			var useCase = CreateUseCase(table, id);

			//Act
			Func<Task> act = async () => await useCase.Execute(id, request);

			//Assert
			(await act.Should().ThrowAsync<ErrorOnValidationException>())
				.Where(e => e.GetErrorMessages().Count == 1 &&
				e.GetErrorMessages().Contains(ResourceMessagesException.TABLE_NOT_FOUND));
		}

		private static UpdateTableUseCase CreateUseCase(ReservaRestaurante.Domain.Entities.Table table, long id = 0)
		{
			var readRepository = new TableReadOnlyRepositoryBuilder();
			var updateRepository = new TableUpdateOnlyRepositoryBuilder().GetById(id, table).Build();
			var mapper = MapperBuilder.Build();
			var unitOfWork = UnitOfWorkBuilder.Build();

			if(id != 0)
			{
				readRepository.ExistTableWithId(id);
			}

			return new UpdateTableUseCase(readRepository.Build(), updateRepository, mapper, unitOfWork);
		}
	}
}
