using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fora.API.Migrations.AppDb
{
    public partial class next : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Threads_ThreadId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Interests_InterestId",
                table: "Threads");

            migrationBuilder.AlterColumn<int>(
                name: "InterestId",
                table: "Threads",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "ThreadId",
                table: "Messages",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Threads_ThreadId",
                table: "Messages",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Interests_InterestId",
                table: "Threads",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Threads_ThreadId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Threads_Interests_InterestId",
                table: "Threads");

            migrationBuilder.AlterColumn<int>(
                name: "InterestId",
                table: "Threads",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "ThreadId",
                table: "Messages",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Threads_ThreadId",
                table: "Messages",
                column: "ThreadId",
                principalTable: "Threads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_Interests_InterestId",
                table: "Threads",
                column: "InterestId",
                principalTable: "Interests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
