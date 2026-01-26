using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyBoards.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "StatesDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StatesDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TagsDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TagsDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AddressesDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Street = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AddressesDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AddressesDb_UsersDb_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkItemsDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Area = table.Column<string>(type: "varchar(200)", nullable: true),
                    Iteration_Path = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Priority = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    AuthorId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    StateId = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    EndDate = table.Column<DateTime>(type: "datetime2(3)", precision: 3, nullable: true),
                    Efford = table.Column<decimal>(type: "decimal(5,2)", nullable: true),
                    Activity = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    RemaningWork = table.Column<decimal>(type: "decimal(14,2)", precision: 14, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItemsDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WorkItemsDb_StatesDb_StateId",
                        column: x => x.StateId,
                        principalTable: "StatesDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkItemsDb_UsersDb_AuthorId",
                        column: x => x.AuthorId,
                        principalTable: "UsersDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CommentsDb",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()"),
                    UpdateTime = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WorkItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommentsDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommentsDb_WorkItemsDb_WorkItemId",
                        column: x => x.WorkItemId,
                        principalTable: "WorkItemsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WorkItemTag",
                columns: table => new
                {
                    WorkItemId = table.Column<int>(type: "int", nullable: false),
                    TagId = table.Column<int>(type: "int", nullable: false),
                    PublicationDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getutcdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WorkItemTag", x => new { x.TagId, x.WorkItemId });
                    table.ForeignKey(
                        name: "FK_WorkItemTag_TagsDb_TagId",
                        column: x => x.TagId,
                        principalTable: "TagsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WorkItemTag_WorkItemsDb_WorkItemId",
                        column: x => x.WorkItemId,
                        principalTable: "WorkItemsDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AddressesDb_UserId",
                table: "AddressesDb",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CommentsDb_WorkItemId",
                table: "CommentsDb",
                column: "WorkItemId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemsDb_AuthorId",
                table: "WorkItemsDb",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemsDb_StateId",
                table: "WorkItemsDb",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_WorkItemTag_WorkItemId",
                table: "WorkItemTag",
                column: "WorkItemId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AddressesDb");

            migrationBuilder.DropTable(
                name: "CommentsDb");

            migrationBuilder.DropTable(
                name: "WorkItemTag");

            migrationBuilder.DropTable(
                name: "TagsDb");

            migrationBuilder.DropTable(
                name: "WorkItemsDb");

            migrationBuilder.DropTable(
                name: "StatesDb");

            migrationBuilder.DropTable(
                name: "UsersDb");
        }
    }
}
