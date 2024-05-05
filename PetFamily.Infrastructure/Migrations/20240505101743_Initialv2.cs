using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetFamily.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "volunteers",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    years_experience = table.Column<int>(type: "integer", nullable: false),
                    number_of_pets_found_home = table.Column<int>(type: "integer", nullable: true),
                    donation_info = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    from_shelter = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_volunteers", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "pets",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    nickname = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    description = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    breed = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    color = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    people_attitude = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    animal_attitude = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: true),
                    health = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    castration = table.Column<bool>(type: "boolean", nullable: false),
                    only_one_in_family = table.Column<bool>(type: "boolean", nullable: false),
                    on_treatment = table.Column<bool>(type: "boolean", nullable: false),
                    height = table.Column<int>(type: "integer", nullable: true),
                    birth_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false),
                    created_date = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: false, defaultValue: new DateTimeOffset(new DateTime(2024, 5, 5, 10, 17, 42, 497, DateTimeKind.Unspecified).AddTicks(1063), new TimeSpan(0, 0, 0, 0, 0))),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: true),
                    building = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    city = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    index = table.Column<string>(type: "character varying(6)", maxLength: 6, nullable: false),
                    street = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    contact_phone_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    place = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    volunteer_phone_number = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    weight = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_pets", x => x.id);
                    table.ForeignKey(
                        name: "fk_pets_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "social_medias",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    link = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: true),
                    social = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_social_medias", x => x.id);
                    table.ForeignKey(
                        name: "fk_social_medias_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "photos",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    path = table.Column<string>(type: "text", nullable: false),
                    is_main = table.Column<bool>(type: "boolean", nullable: false),
                    pet_id = table.Column<Guid>(type: "uuid", nullable: true),
                    volunteer_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_photos", x => x.id);
                    table.ForeignKey(
                        name: "fk_photos_pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_photos_volunteers_volunteer_id",
                        column: x => x.volunteer_id,
                        principalTable: "volunteers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "vaccinations",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: false),
                    applied = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    pet_id = table.Column<Guid>(type: "uuid", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_vaccinations", x => x.id);
                    table.ForeignKey(
                        name: "fk_vaccinations_pets_pet_id",
                        column: x => x.pet_id,
                        principalTable: "pets",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "ix_pets_volunteer_id",
                table: "pets",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "ix_photos_pet_id",
                table: "photos",
                column: "pet_id");

            migrationBuilder.CreateIndex(
                name: "ix_photos_volunteer_id",
                table: "photos",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "ix_social_medias_volunteer_id",
                table: "social_medias",
                column: "volunteer_id");

            migrationBuilder.CreateIndex(
                name: "ix_vaccinations_pet_id",
                table: "vaccinations",
                column: "pet_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "photos");

            migrationBuilder.DropTable(
                name: "social_medias");

            migrationBuilder.DropTable(
                name: "vaccinations");

            migrationBuilder.DropTable(
                name: "pets");

            migrationBuilder.DropTable(
                name: "volunteers");
        }
    }
}
