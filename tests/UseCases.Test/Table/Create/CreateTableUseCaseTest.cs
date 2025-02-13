using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using CommomTestUtilities.Requests;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.Table.Create;
using ReservaRestaurante.Exceptions.ExceptionsBase;
using ReservaRestaurante.Exceptions;

namespace UseCases.Test.Table.Create
{
	public class CreateTableUseCaseTest
	{
		[Fact]
		public async Task Success()
		{
			//Arrange
			var request = RequestCreateTableBuilder.Build();
			var useCase = CreateUseCase();

			//Act
			var result = await useCase.Execute(request);

			//Assert
			result.Should().NotBeNull();
			result.Number.Should().Be(request.Number);
			result.Capacity.Should().Be(request.Capacity);
		}

		[Fact]
		public async Task Error_Exist_Table_With_Number()
		{
			//Arrange
			var request = RequestCreateTableBuilder.Build();
			var useCase = CreateUseCase(request.Number);

			//Act
			Func<Task> act = async () => await useCase.Execute(request);

			//Assert
			(await act.Should().ThrowAsync<ErrorOnValidationException>())
				.Where(error => error.GetErrorMessages().Count == 1 && error.GetErrorMessages().Contains(ResourceMessagesException.TABLE_NUMBER_ALREADY_REGISTERED));
		}

		[Fact]
		public async Task Error_Number_Invalid()
		{
			//Arrange
			var request = RequestCreateTableBuilder.Build();
			request.Number = 0;

			var useCase = CreateUseCase();

			//Act | Salvando a função dentro da var act
			Func<Task> act = async () => await useCase.Execute(request);

			//Assert
			(await act.Should().ThrowAsync<ErrorOnValidationException>())
				.Where(error => error.GetErrorMessages().Count == 1 && error.GetErrorMessages().Contains(ResourceMessagesException.TABLE_NUMBER_INVALID));
		}

		private static CreateTableUseCase CreateUseCase(int number = 0)
		{
			var readRepositoryBuilder = new TableReadOnlyRepositoryBuilder();
			var writeRepositoyBuilder = TableWriteOnlyRepositoryBuilder.Build();
			var mapper = MapperBuilder.Build();
			var unitOfWord = UnitOfWorkBuilder.Build();

			if(number != 0)
				readRepositoryBuilder.ExistTableWithNumber(number);

			return new CreateTableUseCase(readRepositoryBuilder.Build(), writeRepositoyBuilder, mapper, unitOfWord);
		}
	}
}
