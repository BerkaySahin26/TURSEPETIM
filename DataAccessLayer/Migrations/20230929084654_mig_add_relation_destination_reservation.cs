using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class mig_add_relation_destination_reservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DestinationID",
                table: "Reservation2s",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation2s_DestinationID",
                table: "Reservation2s",
                column: "DestinationID");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation2s_Destinations_DestinationID",
                table: "Reservation2s",
                column: "DestinationID",
                principalTable: "Destinations",
                principalColumn: "DestinationID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation2s_Destinations_DestinationID",
                table: "Reservation2s");

            migrationBuilder.DropIndex(
                name: "IX_Reservation2s_DestinationID",
                table: "Reservation2s");

            migrationBuilder.DropColumn(
                name: "DestinationID",
                table: "Reservation2s");
        }
    }
}
