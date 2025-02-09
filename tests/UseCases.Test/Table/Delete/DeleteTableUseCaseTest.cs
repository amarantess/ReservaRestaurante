using CommomTestUtilities.Repositories;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.Table.Delete;
using ReservaRestaurante.Exceptions;
using ReservaRestaurante.Exceptions.ExceptionsBase;

namespace UseCases.Test.Table.Delete
{
	public class DeleteTableUseCaseTest
	{
		[Fact]
		public async Task Success()
		{
			//Arrange
			long id = 1;
			var useCase = CreateUseCase(id);

			//Act
			var act = async () => await useCase.Execute(id);

			//Assert
			await act.Should().NotThrowAsync();
		}

		[Fact]
		public async Task Table_NotFound()
		{
			//Arrange
			long id = 0;
			var useCase = CreateUseCase(id);

			//Act
			var act = async () => await useCase.Execute(id);

			//Assert
			(await act.Should().ThrowAsync<NotFoundException>())
			.Where(e => e.Message.Equals(ResourceMessagesException.TABLE_NOT_FOUND));
		}

		private static DeleteTableUseCase CreateUseCase(long id = 0)
		{
			var writeRepositoyBuilder = TableWriteOnlyRepositoryBuilder.Build();
			var readRepositoryBuilder = new TableReadOnlyRepositoryBuilder();
			var unitOfWork = UnitOfWorkBuilder.Build();

			if (id != 0)
			{
				readRepositoryBuilder.ExistTableWithId(id);
			}

			return new DeleteTableUseCase(writeRepositoyBuilder, readRepositoryBuilder.Build(), unitOfWork);
		}
	}
}
