using FluentMigrator;

namespace ReservaRestaurante.Infrastructure.Migrations.Versions
{
	[Migration(5, "Add active collum on Users table")]
	public class Version0000004 : VersionBase
	{
		public override void Up()
		{
			Alter.Table("Users").AddColumn("Active").AsBoolean().NotNullable().SetExistingRowsTo(true);
		}
	}
}
