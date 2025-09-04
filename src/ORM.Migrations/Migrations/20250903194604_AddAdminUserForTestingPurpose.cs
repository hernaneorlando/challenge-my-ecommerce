using UserManagement.Domain.Enums;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ORM.Migrations.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUserForTestingPurpose : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Generated hash for password "Pass@123"
            migrationBuilder.InsertData("Users",
                new[] { "Id", "Name", "Username", "Email", "Password", "Role", "Status", "CreatedAt" },
                new object[] { Guid.NewGuid(), "Admin User", "admin.user", "admin@email.test", "$2a$11$kksL0eB9YFnK7L.9AAyAIuFG1RUvFGpLipUVW4uzEtIJvdepsCc2i", UserRole.Admin.ToString(), UserStatus.Active.ToString(), DateTime.UtcNow });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DELETE FROM Users WHERE Email = 'admin@email.test'");
        }
    }
}
