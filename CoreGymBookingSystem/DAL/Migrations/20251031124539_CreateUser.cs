using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Migrations
{
    /// <inheritdoc />
    public partial class CreateUser : Migration
    {
        /// <inheritdoc />
        protected override void Up( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_AspNetUsers_InstructorId",
                table: "Sessions" );

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims" );

            migrationBuilder.DropTable(
                name: "AspNetUserClaims" );

            migrationBuilder.DropTable(
                name: "AspNetUserLogins" );

            migrationBuilder.DropTable(
                name: "AspNetUserRoles" );

            migrationBuilder.DropTable(
                name: "AspNetUserTokens" );

            migrationBuilder.DropTable(
                name: "AspNetRoles" );

            migrationBuilder.DropTable(
                name: "AspNetUsers" );

            migrationBuilder.AlterColumn<int>(
                name: "InstructorId",
                table: "Sessions",
                type: "int",
                nullable: false,
                oldClrType: typeof( string ),
                oldType: "nvarchar(450)" );

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    UserName = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    NormalizedUserName = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    Email = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    NormalizedEmail = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    EmailConfirmed = table.Column<bool>( type: "bit", nullable: false ),
                    PasswordHash = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    SecurityStamp = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    ConcurrencyStamp = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    PhoneNumber = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    PhoneNumberConfirmed = table.Column<bool>( type: "bit", nullable: false ),
                    TwoFactorEnabled = table.Column<bool>( type: "bit", nullable: false ),
                    LockoutEnd = table.Column<DateTimeOffset>( type: "datetimeoffset", nullable: true ),
                    LockoutEnabled = table.Column<bool>( type: "bit", nullable: false ),
                    AccessFailedCount = table.Column<int>( type: "int", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_User", x => x.Id );
                } );

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_User_InstructorId",
                table: "Sessions",
                column: "InstructorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade );

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_User_InstructorId",
                table: "Sessions" );

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User" );

            migrationBuilder.RenameTable(
                name: "User",
                newName: "AspNetUsers" );

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof( string ),
                oldType: "nvarchar(max)",
                oldNullable: true );

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof( string ),
                oldType: "nvarchar(max)",
                oldNullable: true );

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedEmail",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof( string ),
                oldType: "nvarchar(max)",
                oldNullable: true );

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "AspNetUsers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof( string ),
                oldType: "nvarchar(max)",
                oldNullable: true );

            migrationBuilder.AddPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers",
                column: "Id" );

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    Name = table.Column<string>( type: "nvarchar(256)", maxLength: 256, nullable: true ),
                    NormalizedName = table.Column<string>( type: "nvarchar(256)", maxLength: 256, nullable: true ),
                    ConcurrencyStamp = table.Column<string>( type: "nvarchar(max)", nullable: true )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetRoles", x => x.Id );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    UserId = table.Column<int>( type: "int", nullable: false ),
                    ClaimType = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    ClaimValue = table.Column<string>( type: "nvarchar(max)", nullable: true )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetUserClaims", x => x.Id );
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>( type: "nvarchar(128)", maxLength: 128, nullable: false ),
                    ProviderKey = table.Column<string>( type: "nvarchar(128)", maxLength: 128, nullable: false ),
                    ProviderDisplayName = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    UserId = table.Column<int>( type: "int", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey } );
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<int>( type: "int", nullable: false ),
                    LoginProvider = table.Column<string>( type: "nvarchar(128)", maxLength: 128, nullable: false ),
                    Name = table.Column<string>( type: "nvarchar(128)", maxLength: 128, nullable: false ),
                    Value = table.Column<string>( type: "nvarchar(max)", nullable: true )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name } );
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    RoleId = table.Column<int>( type: "int", nullable: false ),
                    ClaimType = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    ClaimValue = table.Column<string>( type: "nvarchar(max)", nullable: true )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetRoleClaims", x => x.Id );
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<int>( type: "int", nullable: false ),
                    RoleId = table.Column<int>( type: "int", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetUserRoles", x => new { x.UserId, x.RoleId } );
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail" );

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL" );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId" );

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL" );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId" );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId" );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId" );

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_AspNetUsers_InstructorId",
                table: "Sessions",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade );
        }

        /// <inheritdoc />
        protected override void Down( MigrationBuilder migrationBuilder )
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_AspNetUsers_InstructorId",
                table: "Sessions" );

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims" );

            migrationBuilder.DropTable(
                name: "AspNetUserClaims" );

            migrationBuilder.DropTable(
                name: "AspNetUserLogins" );

            migrationBuilder.DropTable(
                name: "AspNetUserRoles" );

            migrationBuilder.DropTable(
                name: "AspNetUserTokens" );

            migrationBuilder.DropTable(
                name: "AspNetRoles" );

            migrationBuilder.DropPrimaryKey(
                name: "PK_AspNetUsers",
                table: "AspNetUsers" );

            migrationBuilder.DropIndex(
                name: "EmailIndex",
                table: "AspNetUsers" );

            migrationBuilder.DropIndex(
                name: "UserNameIndex",
                table: "AspNetUsers" );

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "User" );

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof( string ),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true );

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedUserName",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof( string ),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true );

            migrationBuilder.AlterColumn<string>(
                name: "NormalizedEmail",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof( string ),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true );

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "User",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof( string ),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true );

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "Id" );

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_User_InstructorId",
                table: "Sessions",
                column: "InstructorId",
                principalTable: "User",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade );

            migrationBuilder.DropForeignKey(
                name: "FK_Sessions_User_InstructorId",
                table: "Sessions" );

            migrationBuilder.DropTable(
                name: "User" );

            migrationBuilder.AlterColumn<string>(
                name: "InstructorId",
                table: "Sessions",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof( int ),
                oldType: "int" );

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>( type: "nvarchar(450)", nullable: false ),
                    ConcurrencyStamp = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    Name = table.Column<string>( type: "nvarchar(256)", maxLength: 256, nullable: true ),
                    NormalizedName = table.Column<string>( type: "nvarchar(256)", maxLength: 256, nullable: true )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetRoles", x => x.Id );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>( type: "nvarchar(450)", nullable: false ),
                    AccessFailedCount = table.Column<int>( type: "int", nullable: false ),
                    ConcurrencyStamp = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    Email = table.Column<string>( type: "nvarchar(256)", maxLength: 256, nullable: true ),
                    EmailConfirmed = table.Column<bool>( type: "bit", nullable: false ),
                    LockoutEnabled = table.Column<bool>( type: "bit", nullable: false ),
                    LockoutEnd = table.Column<DateTimeOffset>( type: "datetimeoffset", nullable: true ),
                    NormalizedEmail = table.Column<string>( type: "nvarchar(256)", maxLength: 256, nullable: true ),
                    NormalizedUserName = table.Column<string>( type: "nvarchar(256)", maxLength: 256, nullable: true ),
                    PasswordHash = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    PhoneNumber = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    PhoneNumberConfirmed = table.Column<bool>( type: "bit", nullable: false ),
                    SecurityStamp = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    TwoFactorEnabled = table.Column<bool>( type: "bit", nullable: false ),
                    UserName = table.Column<string>( type: "nvarchar(256)", maxLength: 256, nullable: true )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetUsers", x => x.Id );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    ClaimType = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    ClaimValue = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    RoleId = table.Column<string>( type: "nvarchar(450)", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetRoleClaims", x => x.Id );
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>( type: "int", nullable: false )
                        .Annotation( "SqlServer:Identity", "1, 1" ),
                    ClaimType = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    ClaimValue = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    UserId = table.Column<string>( type: "nvarchar(450)", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetUserClaims", x => x.Id );
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>( type: "nvarchar(128)", maxLength: 128, nullable: false ),
                    ProviderKey = table.Column<string>( type: "nvarchar(128)", maxLength: 128, nullable: false ),
                    ProviderDisplayName = table.Column<string>( type: "nvarchar(max)", nullable: true ),
                    UserId = table.Column<string>( type: "nvarchar(450)", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey } );
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>( type: "nvarchar(450)", nullable: false ),
                    RoleId = table.Column<string>( type: "nvarchar(450)", nullable: false )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetUserRoles", x => new { x.UserId, x.RoleId } );
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>( type: "nvarchar(450)", nullable: false ),
                    LoginProvider = table.Column<string>( type: "nvarchar(128)", maxLength: 128, nullable: false ),
                    Name = table.Column<string>( type: "nvarchar(128)", maxLength: 128, nullable: false ),
                    Value = table.Column<string>( type: "nvarchar(max)", nullable: true )
                },
                constraints: table =>
                {
                    table.PrimaryKey( "PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name } );
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade );
                } );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId" );

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL" );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId" );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId" );

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId" );

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail" );

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL" );

            migrationBuilder.AddForeignKey(
                name: "FK_Sessions_AspNetUsers_InstructorId",
                table: "Sessions",
                column: "InstructorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade );
        }
    }
}
