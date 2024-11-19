using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ITCoursesWeb.Migrations
{
    /// <inheritdoc />
    public partial class ChangedModelCreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Courses_Persons_PersonId",
                table: "Courses");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Courses_CourseId",
                table: "Persons");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Groups_GroupId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Persons_CourseId",
                table: "Persons");

            migrationBuilder.DropIndex(
                name: "IX_Courses_PersonId",
                table: "Courses");

            migrationBuilder.DropColumn(
                name: "CourseId",
                table: "Persons");

            migrationBuilder.DropColumn(
                name: "PersonId",
                table: "Courses");

            migrationBuilder.AlterColumn<string>(
                name: "PersonType",
                table: "Persons",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "CoursePerson",
                columns: table => new
                {
                    CourseId = table.Column<int>(type: "int", nullable: false),
                    PersonId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CoursePerson", x => new { x.CourseId, x.PersonId });
                    table.ForeignKey(
                        name: "FK_CoursePerson_Courses_CourseId",
                        column: x => x.CourseId,
                        principalTable: "Courses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CoursePerson_Persons_PersonId",
                        column: x => x.PersonId,
                        principalTable: "Persons",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Groups_TeacherId",
                table: "Groups",
                column: "TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_CoursePerson_PersonId",
                table: "CoursePerson",
                column: "PersonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Persons_TeacherId",
                table: "Groups",
                column: "TeacherId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Groups_GroupId",
                table: "Persons",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Persons_TeacherId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Persons_Groups_GroupId",
                table: "Persons");

            migrationBuilder.DropTable(
                name: "CoursePerson");

            migrationBuilder.DropIndex(
                name: "IX_Groups_TeacherId",
                table: "Groups");

            migrationBuilder.AlterColumn<int>(
                name: "PersonType",
                table: "Persons",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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

            migrationBuilder.AddForeignKey(
                name: "FK_Courses_Persons_PersonId",
                table: "Courses",
                column: "PersonId",
                principalTable: "Persons",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Courses_CourseId",
                table: "Persons",
                column: "CourseId",
                principalTable: "Courses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Persons_Groups_GroupId",
                table: "Persons",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }
    }
}
