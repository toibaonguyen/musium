using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobNet.Migrations
{
    /// <inheritdoc />
    public partial class modify1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CompanyPageAdmins");

            migrationBuilder.DropTable(
                name: "CompanyPostComments");

            migrationBuilder.DropTable(
                name: "CompanyPostNotifications");

            migrationBuilder.DropTable(
                name: "CompanyPostReactions");

            migrationBuilder.DropTable(
                name: "CompanyPosts");

            migrationBuilder.DropColumn(
                name: "IsHiring",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IndustryId",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "BackgroundImage",
                table: "Companies",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Comments",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Comments");

            migrationBuilder.AddColumn<bool>(
                name: "IsHiring",
                table: "Users",
                type: "boolean",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BackgroundImage",
                table: "Companies",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "IndustryId",
                table: "Companies",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string[]>(
                name: "Images",
                table: "Comments",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.CreateTable(
                name: "CompanyPageAdmins",
                columns: table => new
                {
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    PageAdminId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPageAdmins", x => new { x.CompanyId, x.PageAdminId });
                    table.ForeignKey(
                        name: "FK_CompanyPageAdmins_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyPageAdmins_Users_PageAdminId",
                        column: x => x.PageAdminId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Images = table.Column<string[]>(type: "text[]", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    OtherFiles = table.Column<string[]>(type: "text[]", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Videos = table.Column<string[]>(type: "text[]", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPosts_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPostComments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PostId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Image = table.Column<string>(type: "text", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPostComments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPostComments_CompanyPosts_PostId",
                        column: x => x.PostId,
                        principalTable: "CompanyPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyPostComments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPostNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CompanyPostId = table.Column<int>(type: "integer", nullable: false),
                    RecieverId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPostNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CompanyPostNotifications_CompanyPosts_CompanyPostId",
                        column: x => x.CompanyPostId,
                        principalTable: "CompanyPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyPostNotifications_Users_RecieverId",
                        column: x => x.RecieverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CompanyPostReactions",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CompanyPostId = table.Column<int>(type: "integer", nullable: false),
                    React = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CompanyPostReactions", x => new { x.UserId, x.CompanyPostId });
                    table.ForeignKey(
                        name: "FK_CompanyPostReactions_CompanyPosts_CompanyPostId",
                        column: x => x.CompanyPostId,
                        principalTable: "CompanyPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CompanyPostReactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPageAdmins_PageAdminId",
                table: "CompanyPageAdmins",
                column: "PageAdminId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPostComments_PostId",
                table: "CompanyPostComments",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPostComments_UserId",
                table: "CompanyPostComments",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPostNotifications_CompanyPostId",
                table: "CompanyPostNotifications",
                column: "CompanyPostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPostNotifications_RecieverId",
                table: "CompanyPostNotifications",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPostReactions_CompanyPostId",
                table: "CompanyPostReactions",
                column: "CompanyPostId");

            migrationBuilder.CreateIndex(
                name: "IX_CompanyPosts_CompanyId",
                table: "CompanyPosts",
                column: "CompanyId");
        }
    }
}
