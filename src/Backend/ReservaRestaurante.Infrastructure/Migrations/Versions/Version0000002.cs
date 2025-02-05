using FluentMigrator;

namespace ReservaRestaurante.Infrastructure.Migrations.Versions
{
	[Migration(2, "Create table to save table's information")]
	public class Version0000002 : VersionBase
	{
		public override void Up()
		{
			CreateTable("Table")
				.WithColumn("Number").AsInt32().NotNullable()
				.WithColumn("Capacity").AsInt32().NotNullable()
				.WithColumn("Status").AsString(50).NotNullable();
		}
	}
}
