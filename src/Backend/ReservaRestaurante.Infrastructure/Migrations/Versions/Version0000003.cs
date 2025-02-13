using FluentMigrator;

namespace ReservaRestaurante.Infrastructure.Migrations.Versions
{
	[Migration(4, "Create table to save reservation information")]
	public class Version0000003 : VersionBase
	{
		public override void Up()
		{
			CreateTable("Reservations")
				.WithColumn("UserId").AsInt64().NotNullable().ForeignKey("FK_Reservation_User_Id", "Users", "Id")
				.WithColumn("TableId").AsInt64().NotNullable().ForeignKey("FK_Reservation_Table_Id", "Table", "Id")
				.WithColumn("ReservationDate").AsDateTime().NotNullable()
				.WithColumn("Status").AsString(50).NotNullable();
		}
	}
}
