using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace MoviePlatform.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_movies_studio_StudioId",
                table: "movies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_studio",
                table: "studio");

            migrationBuilder.RenameTable(
                name: "studio",
                newName: "studios");

            migrationBuilder.RenameIndex(
                name: "IX_studio_Name",
                table: "studios",
                newName: "IX_studios_Name");

            migrationBuilder.AddColumn<decimal>(
                name: "AverageRating",
                table: "movies",
                type: "numeric",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "ReviewsCount",
                table: "movies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_studios",
                table: "studios",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    MovieId = table.Column<long>(type: "bigint", nullable: false),
                    Rating = table.Column<int>(type: "integer", nullable: false),
                    Comment = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_movies_studios_StudioId",
                table: "movies",
                column: "StudioId",
                principalTable: "studios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_movies_studios_StudioId",
                table: "movies");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropPrimaryKey(
                name: "PK_studios",
                table: "studios");

            migrationBuilder.DropColumn(
                name: "AverageRating",
                table: "movies");

            migrationBuilder.DropColumn(
                name: "ReviewsCount",
                table: "movies");

            migrationBuilder.RenameTable(
                name: "studios",
                newName: "studio");

            migrationBuilder.RenameIndex(
                name: "IX_studios_Name",
                table: "studio",
                newName: "IX_studio_Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_studio",
                table: "studio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_movies_studio_StudioId",
                table: "movies",
                column: "StudioId",
                principalTable: "studio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
