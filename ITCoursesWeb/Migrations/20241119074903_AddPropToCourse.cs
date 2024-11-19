using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITCoursesWeb.Migrations
{
    /// <inheritdoc />
    public partial class AddPropToCourse : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CoursePerson");

            migrationBuilder.AddColumn<int>(
                name: "CourseId",
                table: "Persons",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PersonId",
                table: "Courses",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persons_CourseId",
                table: "Persons",
                column: "CourseId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_PersonId",
                table: "Courses",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Persons_PersonId",
                table: "Courses",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Persons_TeacherId",
                table: "Courses",
                column: "TeacherId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Courses_CourseId",
                table: "Persons",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Persons_PersonId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Persons_TeacherId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Courses_CourseId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_CourseId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Courses_PersonId",
                table: "Courses");

            migrationBuilder.DropIndex(
                name: "IX_Courses_TeacherId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Courses");

            migrationBuilder.CreateTable(
                name: "CoursePerson",
                columns: table => new
                {
                    CoursesId = table.Column<int>(type: "int", nullable: false),
                    PersonsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePerson", x => new { x.CoursesId, x.PersonsId });
                    table.ForeignKey(
                        name: "FK_CoursePerson_Courses_CoursesId",
                        column: x => x.CoursesId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursePerson_Persons_PersonsId",
                        column: x => x.PersonsId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CoursePerson_PersonsId",
                table: "CoursePerson",
                column: "PersonsId");
        }
    }
}
