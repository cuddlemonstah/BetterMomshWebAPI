using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BetterMomshWebAPI.Migrations
{
    /// <inheritdoc />
    public partial class PatientIniDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_credential",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    Username = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Salt = table.Column<string>(type: "text", nullable: false),
                    Role = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_credential", x => x.user_id);
                });

            migrationBuilder.CreateTable(
                name: "BabyBook",
                columns: table => new
                {
                    BookId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Title = table.Column<string>(type: "text", nullable: false),
                    Created = table.Column<DateOnly>(type: "date", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BabyBook", x => x.BookId);
                    table.ForeignKey(
                        name: "FK_BabyBook_user_credential_user_id",
                        column: x => x.user_id,
                        principalTable: "user_credential",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RefreshTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    RefreshToken = table.Column<string>(type: "text", nullable: true),
                    TokenCreated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    TokenExpired = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RefreshTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RefreshTokens_user_credential_user_id",
                        column: x => x.user_id,
                        principalTable: "user_credential",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_profile",
                columns: table => new
                {
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    FirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Birthdate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Religion = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Occupation = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    RelationshipStatus = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    ContactNumber = table.Column<decimal>(type: "numeric(12,2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_profile", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_user_profile_user_credential_user_id",
                        column: x => x.user_id,
                        principalTable: "user_credential",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Trimester",
                columns: table => new
                {
                    TrimesterId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Trimesters = table.Column<string>(type: "text", nullable: false),
                    BookId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trimester", x => x.TrimesterId);
                    table.ForeignKey(
                        name: "FK_Trimester_BabyBook_BookId",
                        column: x => x.BookId,
                        principalTable: "BabyBook",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Months",
                columns: table => new
                {
                    MonthId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    Month = table.Column<string>(type: "text", nullable: false),
                    TrimesterId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Months", x => x.MonthId);
                    table.ForeignKey(
                        name: "FK_Months_Trimester_TrimesterId",
                        column: x => x.TrimesterId,
                        principalTable: "Trimester",
                        principalColumn: "TrimesterId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Journals",
                columns: table => new
                {
                    journalId = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    JournalName = table.Column<string>(type: "text", nullable: false),
                    Entry_Date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    journalEntry = table.Column<string>(type: "text", nullable: false),
                    PhotoData = table.Column<byte[]>(type: "bytea", nullable: false),
                    BookId = table.Column<long>(type: "bigint", nullable: false),
                    MonthId = table.Column<long>(type: "bigint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journals", x => x.journalId);
                    table.ForeignKey(
                        name: "FK_Journals_BabyBook_BookId",
                        column: x => x.BookId,
                        principalTable: "BabyBook",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Journals_Months_MonthId",
                        column: x => x.MonthId,
                        principalTable: "Months",
                        principalColumn: "MonthId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BabyBook_user_id",
                table: "BabyBook",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_BookId",
                table: "Journals",
                column: "BookId");

            migrationBuilder.CreateIndex(
                name: "IX_Journals_MonthId",
                table: "Journals",
                column: "MonthId");

            migrationBuilder.CreateIndex(
                name: "IX_Months_TrimesterId",
                table: "Months",
                column: "TrimesterId");

            migrationBuilder.CreateIndex(
                name: "IX_RefreshTokens_user_id",
                table: "RefreshTokens",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trimester_BookId",
                table: "Trimester",
                column: "BookId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Journals");

            migrationBuilder.DropTable(
                name: "RefreshTokens");

            migrationBuilder.DropTable(
                name: "user_profile");

            migrationBuilder.DropTable(
                name: "Months");

            migrationBuilder.DropTable(
                name: "Trimester");

            migrationBuilder.DropTable(
                name: "BabyBook");

            migrationBuilder.DropTable(
                name: "user_credential");
        }
    }
}
