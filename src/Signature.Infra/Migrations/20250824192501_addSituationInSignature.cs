using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Signature.Infra.Migrations
{
    /// <inheritdoc />
    public partial class addSituationInSignature : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "situation",
                table: "Signatures",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "situation",
                table: "Signatures");
        }
    }
}
