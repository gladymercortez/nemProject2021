using Microsoft.EntityFrameworkCore.Migrations;

namespace MoviesAPI.Migrations
{
    public partial class PostItem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostItems",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Picture = table.Column<string>(maxLength: 500, nullable: false),
                    Title = table.Column<string>(maxLength: 50, nullable: false),
                    ItemDescription = table.Column<string>(maxLength: 300, nullable: true),
                    Condition = table.Column<int>(nullable: false),
                    MeetingLocation = table.Column<string>(maxLength: 200, nullable: false),
                    UserId = table.Column<string>(maxLength: 500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostItems");
        }
    }
}
