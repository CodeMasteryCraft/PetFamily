using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RenameVaccinations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_vaccination_pets_pet_id",
                table: "vaccination");

            migrationBuilder.DropPrimaryKey(
                name: "pk_vaccination",
                table: "vaccination");

            migrationBuilder.RenameTable(
                name: "vaccination",
                newName: "vaccinations");

            migrationBuilder.RenameIndex(
                name: "ix_vaccination_pet_id",
                table: "vaccinations",
                newName: "ix_vaccinations_pet_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_vaccinations",
                table: "vaccinations",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_vaccinations_pets_pet_id",
                table: "vaccinations",
                column: "pet_id",
                principalTable: "pets",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_vaccinations_pets_pet_id",
                table: "vaccinations");

            migrationBuilder.DropPrimaryKey(
                name: "pk_vaccinations",
                table: "vaccinations");

            migrationBuilder.RenameTable(
                name: "vaccinations",
                newName: "vaccination");

            migrationBuilder.RenameIndex(
                name: "ix_vaccinations_pet_id",
                table: "vaccination",
                newName: "ix_vaccination_pet_id");

            migrationBuilder.AddPrimaryKey(
                name: "pk_vaccination",
                table: "vaccination",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "fk_vaccination_pets_pet_id",
                table: "vaccination",
                column: "pet_id",
                principalTable: "pets",
                principalColumn: "id");
        }
    }
}
