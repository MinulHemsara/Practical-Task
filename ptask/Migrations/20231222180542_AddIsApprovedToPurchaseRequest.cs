using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ptask.Migrations
{
    public partial class AddIsApprovedToPurchaseRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ApprovalStatus",
                table: "PurchaseRequests");

            migrationBuilder.RenameColumn(
                name: "IsPendingApproval",
                table: "PurchaseRequests",
                newName: "IsApproved");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsApproved",
                table: "PurchaseRequests",
                newName: "IsPendingApproval");

            migrationBuilder.AddColumn<int>(
                name: "ApprovalStatus",
                table: "PurchaseRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
