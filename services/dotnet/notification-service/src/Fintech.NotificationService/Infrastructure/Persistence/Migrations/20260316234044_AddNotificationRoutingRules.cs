using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fintech.NotificationService.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddNotificationRoutingRules : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "notification_routing_rules",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MessageType = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    Channel = table.Column<string>(type: "character varying(32)", maxLength: 32, nullable: false),
                    ProviderId = table.Column<Guid>(type: "uuid", nullable: false),
                    Priority = table.Column<int>(type: "integer", nullable: false),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false),
                    CreatedAtUtc = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_notification_routing_rules", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_notification_routing_rules_MessageType_Channel_Priority",
                table: "notification_routing_rules",
                columns: new[] { "MessageType", "Channel", "Priority" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "notification_routing_rules");
        }
    }
}
