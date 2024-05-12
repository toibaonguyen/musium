using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace JobNet.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate1111 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_certifications_users_UserId",
                table: "certifications");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_posts_PostId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_comments_users_AuthorId",
                table: "comments");

            migrationBuilder.DropForeignKey(
                name: "FK_company_page_admins_companies_CompanyId",
                table: "company_page_admins");

            migrationBuilder.DropForeignKey(
                name: "FK_company_page_admins_users_PageAdminId",
                table: "company_page_admins");

            migrationBuilder.DropForeignKey(
                name: "FK_company_post_comments_company_posts_PostId",
                table: "company_post_comments");

            migrationBuilder.DropForeignKey(
                name: "FK_company_post_comments_users_AuthorId",
                table: "company_post_comments");

            migrationBuilder.DropForeignKey(
                name: "FK_company_posts_companies_CompanyId",
                table: "company_posts");

            migrationBuilder.DropForeignKey(
                name: "FK_educations_users_UserId",
                table: "educations");

            migrationBuilder.DropForeignKey(
                name: "FK_experiences_companies_CompanyId",
                table: "experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_experiences_users_AuthorId",
                table: "experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_job_post_skills_job_posts_JobPostId",
                table: "job_post_skills");

            migrationBuilder.DropForeignKey(
                name: "FK_job_post_skills_skills_SkillId",
                table: "job_post_skills");

            migrationBuilder.DropForeignKey(
                name: "FK_job_posts_companies_CompanyId",
                table: "job_posts");

            migrationBuilder.DropForeignKey(
                name: "FK_job_posts_users_AuthorId",
                table: "job_posts");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_users_ReceiverId",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "FK_messages_users_SenderId",
                table: "messages");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_groups_GroupId",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "FK_posts_users_OwnerId",
                table: "posts");

            migrationBuilder.DropForeignKey(
                name: "FK_user_skills_skills_SkillId",
                table: "user_skills");

            migrationBuilder.DropForeignKey(
                name: "FK_user_skills_users_UserId",
                table: "user_skills");

            migrationBuilder.DropTable(
                name: "banned_posts");

            migrationBuilder.DropTable(
                name: "conversations");

            migrationBuilder.DropTable(
                name: "group_members");

            migrationBuilder.DropTable(
                name: "JobPostSkill");

            migrationBuilder.DropTable(
                name: "SkillUser");

            migrationBuilder.DropTable(
                name: "groups");

            migrationBuilder.DropPrimaryKey(
                name: "PK_users",
                table: "users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_skills",
                table: "skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_posts",
                table: "posts");

            migrationBuilder.DropIndex(
                name: "IX_posts_GroupId",
                table: "posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_messages",
                table: "messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_experiences",
                table: "experiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_educations",
                table: "educations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_companies",
                table: "companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_comments",
                table: "comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_certifications",
                table: "certifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_admins",
                table: "admins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_skills",
                table: "user_skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_job_posts",
                table: "job_posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_job_post_skills",
                table: "job_post_skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_company_posts",
                table: "company_posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_company_post_comments",
                table: "company_post_comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_company_page_admins",
                table: "company_page_admins");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "posts");

            migrationBuilder.DropColumn(
                name: "IsImage",
                table: "messages");

            migrationBuilder.RenameTable(
                name: "users",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "skills",
                newName: "Skills");

            migrationBuilder.RenameTable(
                name: "posts",
                newName: "Posts");

            migrationBuilder.RenameTable(
                name: "messages",
                newName: "Messages");

            migrationBuilder.RenameTable(
                name: "experiences",
                newName: "Experiences");

            migrationBuilder.RenameTable(
                name: "educations",
                newName: "Educations");

            migrationBuilder.RenameTable(
                name: "companies",
                newName: "Companies");

            migrationBuilder.RenameTable(
                name: "comments",
                newName: "Comments");

            migrationBuilder.RenameTable(
                name: "certifications",
                newName: "Certifications");

            migrationBuilder.RenameTable(
                name: "admins",
                newName: "Admins");

            migrationBuilder.RenameTable(
                name: "user_skills",
                newName: "UserSkills");

            migrationBuilder.RenameTable(
                name: "job_posts",
                newName: "JobPosts");

            migrationBuilder.RenameTable(
                name: "job_post_skills",
                newName: "JobPostSkills");

            migrationBuilder.RenameTable(
                name: "company_posts",
                newName: "CompanyPosts");

            migrationBuilder.RenameTable(
                name: "company_post_comments",
                newName: "CompanyPostComments");

            migrationBuilder.RenameTable(
                name: "company_page_admins",
                newName: "CompanyPageAdmins");

            migrationBuilder.RenameIndex(
                name: "IX_posts_OwnerId",
                table: "Posts",
                newName: "IX_Posts_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_messages_SenderId",
                table: "Messages",
                newName: "IX_Messages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_messages_ReceiverId",
                table: "Messages",
                newName: "IX_Messages_ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_experiences_AuthorId",
                table: "Experiences",
                newName: "IX_Experiences_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_educations_UserId",
                table: "Educations",
                newName: "IX_Educations_UserId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "Comments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_comments_PostId",
                table: "Comments",
                newName: "IX_Comments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_comments_AuthorId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_certifications_UserId",
                table: "Certifications",
                newName: "IX_Certifications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_user_skills_SkillId",
                table: "UserSkills",
                newName: "IX_UserSkills_SkillId");

            migrationBuilder.RenameColumn(
                name: "DescriptionInHtml",
                table: "JobPosts",
                newName: "Description");

            migrationBuilder.RenameIndex(
                name: "IX_job_posts_CompanyId",
                table: "JobPosts",
                newName: "IX_JobPosts_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_job_posts_AuthorId",
                table: "JobPosts",
                newName: "IX_JobPosts_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_job_post_skills_JobPostId",
                table: "JobPostSkills",
                newName: "IX_JobPostSkills_JobPostId");

            migrationBuilder.RenameIndex(
                name: "IX_company_posts_CompanyId",
                table: "CompanyPosts",
                newName: "IX_CompanyPosts_CompanyId");

            migrationBuilder.RenameColumn(
                name: "AuthorId",
                table: "CompanyPostComments",
                newName: "UserId");

            migrationBuilder.RenameIndex(
                name: "IX_company_post_comments_PostId",
                table: "CompanyPostComments",
                newName: "IX_CompanyPostComments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_company_post_comments_AuthorId",
                table: "CompanyPostComments",
                newName: "IX_CompanyPostComments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_company_page_admins_PageAdminId",
                table: "CompanyPageAdmins",
                newName: "IX_CompanyPageAdmins_PageAdminId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Posts",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "Messages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OtherFile",
                table: "Messages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Video",
                table: "Messages",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Experiences",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Comments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "Comments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string[]>(
                name: "Images",
                table: "Comments",
                type: "text[]",
                nullable: false,
                defaultValue: new string[0]);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "CompanyPostComments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "CompanyPostComments",
                type: "timestamp without time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone");

            migrationBuilder.AddColumn<string>(
                name: "Image",
                table: "CompanyPostComments",
                type: "text",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Skills",
                table: "Skills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Posts",
                table: "Posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Messages",
                table: "Messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Experiences",
                table: "Experiences",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Educations",
                table: "Educations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Companies",
                table: "Companies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Certifications",
                table: "Certifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Admins",
                table: "Admins",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserSkills",
                table: "UserSkills",
                columns: new[] { "UserId", "SkillId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobPosts",
                table: "JobPosts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobPostSkills",
                table: "JobPostSkills",
                columns: new[] { "SkillId", "JobPostId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyPosts",
                table: "CompanyPosts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyPostComments",
                table: "CompanyPostComments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CompanyPageAdmins",
                table: "CompanyPageAdmins",
                columns: new[] { "CompanyId", "PageAdminId" });

            migrationBuilder.CreateTable(
                name: "CloudMessageRegistrationTokens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    Token = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CloudMessageRegistrationTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CloudMessageRegistrationTokens_Users_UserId",
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

            migrationBuilder.CreateTable(
                name: "Connections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SenderId = table.Column<int>(type: "integer", nullable: false),
                    RecieverId = table.Column<int>(type: "integer", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    RequestedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Connections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Connections_Users_RecieverId",
                        column: x => x.RecieverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Connections_Users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobPostBills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    JobPostId = table.Column<int>(type: "integer", nullable: false),
                    Amount = table.Column<decimal>(type: "numeric", nullable: false),
                    BillDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsPayed = table.Column<bool>(type: "boolean", nullable: false),
                    PaymentDate = table.Column<DateTime>(type: "timestamp without time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostBills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobPostBills_JobPosts_JobPostId",
                        column: x => x.JobPostId,
                        principalTable: "JobPosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MessageNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    MessageId = table.Column<int>(type: "integer", nullable: false),
                    RecieverId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MessageNotifications_Messages_MessageId",
                        column: x => x.MessageId,
                        principalTable: "Messages",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MessageNotifications_Users_RecieverId",
                        column: x => x.RecieverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PostId = table.Column<int>(type: "integer", nullable: false),
                    RecieverId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostNotifications_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostNotifications_Users_RecieverId",
                        column: x => x.RecieverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PostReactions",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    PostId = table.Column<int>(type: "integer", nullable: false),
                    React = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostReactions", x => new { x.UserId, x.PostId });
                    table.ForeignKey(
                        name: "FK_PostReactions_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PostReactions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserFollowCompanies",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    CompanyId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserFollowCompanies", x => new { x.UserId, x.CompanyId });
                    table.ForeignKey(
                        name: "FK_UserFollowCompanies_Companies_CompanyId",
                        column: x => x.CompanyId,
                        principalTable: "Companies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserFollowCompanies_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ConnectionRequestNotifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ConnectionRequestId = table.Column<int>(type: "integer", nullable: false),
                    RecieverId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConnectionRequestNotifications", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ConnectionRequestNotifications_Connections_ConnectionReques~",
                        column: x => x.ConnectionRequestId,
                        principalTable: "Connections",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ConnectionRequestNotifications_Users_RecieverId",
                        column: x => x.RecieverId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Experiences_CompanyId",
                table: "Experiences",
                column: "CompanyId");

            migrationBuilder.CreateIndex(
                name: "IX_CloudMessageRegistrationTokens_UserId",
                table: "CloudMessageRegistrationTokens",
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
                name: "IX_ConnectionRequestNotifications_ConnectionRequestId",
                table: "ConnectionRequestNotifications",
                column: "ConnectionRequestId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ConnectionRequestNotifications_RecieverId",
                table: "ConnectionRequestNotifications",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_RecieverId",
                table: "Connections",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_Connections_SenderId_RecieverId",
                table: "Connections",
                columns: new[] { "SenderId", "RecieverId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_JobPostBills_JobPostId",
                table: "JobPostBills",
                column: "JobPostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageNotifications_MessageId",
                table: "MessageNotifications",
                column: "MessageId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MessageNotifications_RecieverId",
                table: "MessageNotifications",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_PostNotifications_PostId",
                table: "PostNotifications",
                column: "PostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostNotifications_RecieverId",
                table: "PostNotifications",
                column: "RecieverId");

            migrationBuilder.CreateIndex(
                name: "IX_PostReactions_PostId",
                table: "PostReactions",
                column: "PostId");

            migrationBuilder.CreateIndex(
                name: "IX_UserFollowCompanies_CompanyId",
                table: "UserFollowCompanies",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Certifications_Users_UserId",
                table: "Certifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPageAdmins_Companies_CompanyId",
                table: "CompanyPageAdmins",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPageAdmins_Users_PageAdminId",
                table: "CompanyPageAdmins",
                column: "PageAdminId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPostComments_CompanyPosts_PostId",
                table: "CompanyPostComments",
                column: "PostId",
                principalTable: "CompanyPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPostComments_Users_UserId",
                table: "CompanyPostComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CompanyPosts_Companies_CompanyId",
                table: "CompanyPosts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Educations_Users_UserId",
                table: "Educations",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Companies_CompanyId",
                table: "Experiences",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Experiences_Users_AuthorId",
                table: "Experiences",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosts_Companies_CompanyId",
                table: "JobPosts",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPosts_Users_AuthorId",
                table: "JobPosts",
                column: "AuthorId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostSkills_JobPosts_JobPostId",
                table: "JobPostSkills",
                column: "JobPostId",
                principalTable: "JobPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_JobPostSkills_Skills_SkillId",
                table: "JobPostSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages",
                column: "SenderId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Posts_Users_OwnerId",
                table: "Posts",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkills_Skills_SkillId",
                table: "UserSkills",
                column: "SkillId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserSkills_Users_UserId",
                table: "UserSkills",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Certifications_Users_UserId",
                table: "Certifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Posts_PostId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPageAdmins_Companies_CompanyId",
                table: "CompanyPageAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPageAdmins_Users_PageAdminId",
                table: "CompanyPageAdmins");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPostComments_CompanyPosts_PostId",
                table: "CompanyPostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPostComments_Users_UserId",
                table: "CompanyPostComments");

            migrationBuilder.DropForeignKey(
                name: "FK_CompanyPosts_Companies_CompanyId",
                table: "CompanyPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_Educations_Users_UserId",
                table: "Educations");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Companies_CompanyId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_Experiences_Users_AuthorId",
                table: "Experiences");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPosts_Companies_CompanyId",
                table: "JobPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPosts_Users_AuthorId",
                table: "JobPosts");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPostSkills_JobPosts_JobPostId",
                table: "JobPostSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_JobPostSkills_Skills_SkillId",
                table: "JobPostSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_ReceiverId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Messages_Users_SenderId",
                table: "Messages");

            migrationBuilder.DropForeignKey(
                name: "FK_Posts_Users_OwnerId",
                table: "Posts");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkills_Skills_SkillId",
                table: "UserSkills");

            migrationBuilder.DropForeignKey(
                name: "FK_UserSkills_Users_UserId",
                table: "UserSkills");

            migrationBuilder.DropTable(
                name: "CloudMessageRegistrationTokens");

            migrationBuilder.DropTable(
                name: "CompanyPostNotifications");

            migrationBuilder.DropTable(
                name: "CompanyPostReactions");

            migrationBuilder.DropTable(
                name: "ConnectionRequestNotifications");

            migrationBuilder.DropTable(
                name: "JobPostBills");

            migrationBuilder.DropTable(
                name: "MessageNotifications");

            migrationBuilder.DropTable(
                name: "PostNotifications");

            migrationBuilder.DropTable(
                name: "PostReactions");

            migrationBuilder.DropTable(
                name: "UserFollowCompanies");

            migrationBuilder.DropTable(
                name: "Connections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Skills",
                table: "Skills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Posts",
                table: "Posts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Messages",
                table: "Messages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Experiences",
                table: "Experiences");

            migrationBuilder.DropIndex(
                name: "IX_Experiences_CompanyId",
                table: "Experiences");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Educations",
                table: "Educations");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Companies",
                table: "Companies");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Certifications",
                table: "Certifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Admins",
                table: "Admins");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserSkills",
                table: "UserSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobPostSkills",
                table: "JobPostSkills");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobPosts",
                table: "JobPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyPosts",
                table: "CompanyPosts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyPostComments",
                table: "CompanyPostComments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CompanyPageAdmins",
                table: "CompanyPageAdmins");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "OtherFile",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Video",
                table: "Messages");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Experiences");

            migrationBuilder.DropColumn(
                name: "Images",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "Image",
                table: "CompanyPostComments");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "users");

            migrationBuilder.RenameTable(
                name: "Skills",
                newName: "skills");

            migrationBuilder.RenameTable(
                name: "Posts",
                newName: "posts");

            migrationBuilder.RenameTable(
                name: "Messages",
                newName: "messages");

            migrationBuilder.RenameTable(
                name: "Experiences",
                newName: "experiences");

            migrationBuilder.RenameTable(
                name: "Educations",
                newName: "educations");

            migrationBuilder.RenameTable(
                name: "Companies",
                newName: "companies");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "comments");

            migrationBuilder.RenameTable(
                name: "Certifications",
                newName: "certifications");

            migrationBuilder.RenameTable(
                name: "Admins",
                newName: "admins");

            migrationBuilder.RenameTable(
                name: "UserSkills",
                newName: "user_skills");

            migrationBuilder.RenameTable(
                name: "JobPostSkills",
                newName: "job_post_skills");

            migrationBuilder.RenameTable(
                name: "JobPosts",
                newName: "job_posts");

            migrationBuilder.RenameTable(
                name: "CompanyPosts",
                newName: "company_posts");

            migrationBuilder.RenameTable(
                name: "CompanyPostComments",
                newName: "company_post_comments");

            migrationBuilder.RenameTable(
                name: "CompanyPageAdmins",
                newName: "company_page_admins");

            migrationBuilder.RenameIndex(
                name: "IX_Posts_OwnerId",
                table: "posts",
                newName: "IX_posts_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_SenderId",
                table: "messages",
                newName: "IX_messages_SenderId");

            migrationBuilder.RenameIndex(
                name: "IX_Messages_ReceiverId",
                table: "messages",
                newName: "IX_messages_ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_Experiences_AuthorId",
                table: "experiences",
                newName: "IX_experiences_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Educations_UserId",
                table: "educations",
                newName: "IX_educations_UserId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "comments",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_PostId",
                table: "comments",
                newName: "IX_comments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "comments",
                newName: "IX_comments_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_Certifications_UserId",
                table: "certifications",
                newName: "IX_certifications_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserSkills_SkillId",
                table: "user_skills",
                newName: "IX_user_skills_SkillId");

            migrationBuilder.RenameIndex(
                name: "IX_JobPostSkills_JobPostId",
                table: "job_post_skills",
                newName: "IX_job_post_skills_JobPostId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "job_posts",
                newName: "DescriptionInHtml");

            migrationBuilder.RenameIndex(
                name: "IX_JobPosts_CompanyId",
                table: "job_posts",
                newName: "IX_job_posts_CompanyId");

            migrationBuilder.RenameIndex(
                name: "IX_JobPosts_AuthorId",
                table: "job_posts",
                newName: "IX_job_posts_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyPosts_CompanyId",
                table: "company_posts",
                newName: "IX_company_posts_CompanyId");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "company_post_comments",
                newName: "AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyPostComments_UserId",
                table: "company_post_comments",
                newName: "IX_company_post_comments_AuthorId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyPostComments_PostId",
                table: "company_post_comments",
                newName: "IX_company_post_comments_PostId");

            migrationBuilder.RenameIndex(
                name: "IX_CompanyPageAdmins_PageAdminId",
                table: "company_page_admins",
                newName: "IX_company_page_admins_PageAdminId");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "posts",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "posts",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsImage",
                table: "messages",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "comments",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "comments",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "company_post_comments",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "company_post_comments",
                type: "timestamp without time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp without time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_users",
                table: "users",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_skills",
                table: "skills",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_posts",
                table: "posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_messages",
                table: "messages",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_experiences",
                table: "experiences",
                columns: new[] { "CompanyId", "AuthorId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_educations",
                table: "educations",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_companies",
                table: "companies",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_comments",
                table: "comments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_certifications",
                table: "certifications",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_admins",
                table: "admins",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_skills",
                table: "user_skills",
                columns: new[] { "UserId", "SkillId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_job_post_skills",
                table: "job_post_skills",
                columns: new[] { "SkillId", "JobPostId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_job_posts",
                table: "job_posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_company_posts",
                table: "company_posts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_company_post_comments",
                table: "company_post_comments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_company_page_admins",
                table: "company_page_admins",
                columns: new[] { "CompanyId", "PageAdminId" });

            migrationBuilder.CreateTable(
                name: "banned_posts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PostId = table.Column<int>(type: "integer", nullable: false),
                    BannedReason = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_banned_posts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_banned_posts_posts_PostId",
                        column: x => x.PostId,
                        principalTable: "posts",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "conversations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ReceiverId = table.Column<int>(type: "integer", nullable: false),
                    SenderId = table.Column<int>(type: "integer", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    IsImage = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conversations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_conversations_users_ReceiverId",
                        column: x => x.ReceiverId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_conversations_users_SenderId",
                        column: x => x.SenderId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AdminId = table.Column<int>(type: "integer", nullable: false),
                    Avatar = table.Column<string>(type: "text", nullable: false),
                    Background = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    Description = table.Column<string>(type: "text", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_groups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_groups_users_AdminId",
                        column: x => x.AdminId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobPostSkill",
                columns: table => new
                {
                    JobPostsId = table.Column<int>(type: "integer", nullable: false),
                    SkillsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobPostSkill", x => new { x.JobPostsId, x.SkillsId });
                    table.ForeignKey(
                        name: "FK_JobPostSkill_job_posts_JobPostsId",
                        column: x => x.JobPostsId,
                        principalTable: "job_posts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_JobPostSkill_skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SkillUser",
                columns: table => new
                {
                    SkillsId = table.Column<int>(type: "integer", nullable: false),
                    UsersId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkillUser", x => new { x.SkillsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_SkillUser_skills_SkillsId",
                        column: x => x.SkillsId,
                        principalTable: "skills",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SkillUser_users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "group_members",
                columns: table => new
                {
                    MemberId = table.Column<int>(type: "integer", nullable: false),
                    GroupId = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_group_members", x => new { x.MemberId, x.GroupId });
                    table.ForeignKey(
                        name: "FK_group_members_groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_group_members_users_MemberId",
                        column: x => x.MemberId,
                        principalTable: "users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_posts_GroupId",
                table: "posts",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_banned_posts_PostId",
                table: "banned_posts",
                column: "PostId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_conversations_ReceiverId",
                table: "conversations",
                column: "ReceiverId");

            migrationBuilder.CreateIndex(
                name: "IX_conversations_SenderId",
                table: "conversations",
                column: "SenderId");

            migrationBuilder.CreateIndex(
                name: "IX_group_members_GroupId",
                table: "group_members",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_groups_AdminId",
                table: "groups",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_JobPostSkill_SkillsId",
                table: "JobPostSkill",
                column: "SkillsId");

            migrationBuilder.CreateIndex(
                name: "IX_SkillUser_UsersId",
                table: "SkillUser",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_certifications_users_UserId",
                table: "certifications",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_posts_PostId",
                table: "comments",
                column: "PostId",
                principalTable: "posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_comments_users_AuthorId",
                table: "comments",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_company_page_admins_companies_CompanyId",
                table: "company_page_admins",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_company_page_admins_users_PageAdminId",
                table: "company_page_admins",
                column: "PageAdminId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_company_post_comments_company_posts_PostId",
                table: "company_post_comments",
                column: "PostId",
                principalTable: "company_posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_company_post_comments_users_AuthorId",
                table: "company_post_comments",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_company_posts_companies_CompanyId",
                table: "company_posts",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_educations_users_UserId",
                table: "educations",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_experiences_companies_CompanyId",
                table: "experiences",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_experiences_users_AuthorId",
                table: "experiences",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_job_post_skills_job_posts_JobPostId",
                table: "job_post_skills",
                column: "JobPostId",
                principalTable: "job_posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_job_post_skills_skills_SkillId",
                table: "job_post_skills",
                column: "SkillId",
                principalTable: "skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_job_posts_companies_CompanyId",
                table: "job_posts",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_job_posts_users_AuthorId",
                table: "job_posts",
                column: "AuthorId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_users_ReceiverId",
                table: "messages",
                column: "ReceiverId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_messages_users_SenderId",
                table: "messages",
                column: "SenderId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_posts_groups_GroupId",
                table: "posts",
                column: "GroupId",
                principalTable: "groups",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_posts_users_OwnerId",
                table: "posts",
                column: "OwnerId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_skills_skills_SkillId",
                table: "user_skills",
                column: "SkillId",
                principalTable: "skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_skills_users_UserId",
                table: "user_skills",
                column: "UserId",
                principalTable: "users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
