using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JobNet.Migrations
{
    /// <inheritdoc />
    public partial class modifyrelationshipofpostandnotification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PostNotifications_PostId",
                table: "PostNotifications");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotifications_PostId",
                table: "PostNotifications",
                column: "PostId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_PostNotifications_PostId",
                table: "PostNotifications");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotifications_PostId",
                table: "PostNotifications",
                column: "PostId",
                unique: true);
        }
    }
}
