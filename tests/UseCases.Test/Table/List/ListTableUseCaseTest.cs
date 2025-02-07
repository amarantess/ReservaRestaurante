using CommomTestUtilities.Entities;
using CommomTestUtilities.Mapper;
using CommomTestUtilities.Repositories;
using FluentAssertions;
using ReservaRestaurante.Application.UseCases.Table.List;

namespace UseCases.Test.Table.List
{
	public class ListTableUseCaseTest
	{
		[Fact]
		public async Task Success()
		{
			//Arrange
			var table = TableBuilder.Build();
			var useCase = CreateUseCase(table);

			//Act
			var result = await useCase.Execute();

			//Arrange
			result.Should().NotBeNull();
		}

		private static ListTableUseCase CreateUseCase(ReservaRestaurante.Domain.Entities.Table table)
		{
			var repository = new TableReadOnlyRepositoryBuilder();
			var mapper = MapperBuilder.Build();

			return new ListTableUseCase(repository.Build(), mapper);
		}
	}
}
