using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FoodDelivery.Migrations
{
    /// <inheritdoc />
    public partial class AdditionalRel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RestaurantId",
                table: "Baskets",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_RestaurantId",
                table: "Baskets",
                column: "RestaurantId");

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Restaurants_RestaurantId",
                table: "Baskets",
                column: "RestaurantId",
                principalTable: "Restaurants",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Restaurants_RestaurantId",
                table: "Baskets");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_RestaurantId",
                table: "Baskets");

            migrationBuilder.DropColumn(
                name: "RestaurantId",
                table: "Baskets");
        }
    }
}
