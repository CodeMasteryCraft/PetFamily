using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddVolunteerEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "volunteer_id",
                table: "photos",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "volunteer_id",
                table: "pets",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: false),
                    years_experience = table.Column<int>(type: "integer", nullable: false),
                    number_of_pets_found_home = table.Column<int>(type: "integer", nullable: false),
                    donation_info = table.Column<string>(type: "text", nullable: false),
                    from_shelter = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "social_medias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    link = table.Column<string>(type: "text", nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: true),
                    social = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_social_medias", x => x.id);
                    table.ForeignKey(
                        name: "fk_social_medias_volunteer_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_photos_volunteer_id",
                table: "photos",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id",
                table: "pets",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "ix_social_medias_volunteer_id",
                table: "social_medias",
                column: "volunteer_id");

            migrationBuilder.AddForeignKey(
                name: "fk_pets_volunteer_volunteer_id",
                table: "pets",
                column: "volunteer_id",
                principalTable: "volunteers",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_photos_volunteer_volunteer_id",
                table: "photos",
                column: "volunteer_id",
                principalTable: "volunteers",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_pets_volunteer_volunteer_id",
                table: "pets");

            migrationBuilder.DropForeignKey(
                name: "fk_photos_volunteer_volunteer_id",
                table: "photos");

            migrationBuilder.DropTable(
                name: "social_medias");

            migrationBuilder.DropTable(
                name: "volunteers");

            migrationBuilder.DropIndex(
                name: "ix_photos_volunteer_id",
                table: "photos");

            migrationBuilder.DropIndex(
                name: "ix_pets_volunteer_id",
                table: "pets");

            migrationBuilder.DropColumn(
                name: "volunteer_id",
                table: "photos");

            migrationBuilder.DropColumn(
                name: "volunteer_id",
                table: "pets");
        }
    }
}
