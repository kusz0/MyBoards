using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoards.Migrations
{
    /// <inheritdoc />
    public partial class additionalWorkItemSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(table: "WorkItemState",column: "Value",value: "On Hold");
            migrationBuilder.InsertData(table: "WorkItemState",column: "Value",value: "Rejected");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(table: "WorkItemState", keyColumn: "Value", keyValue: "On Hold");
            migrationBuilder.DeleteData(table: "WorkItemState", keyColumn: "Value", keyValue: "Rejected");
        }
    }
}
