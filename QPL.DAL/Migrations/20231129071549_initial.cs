using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace QPL.DAL.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Manufacturers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Code = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Manufacturers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CPC = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    NortelCpc = table.Column<string>(type: "nvarchar(8)", maxLength: 8, nullable: false),
                    EngineeringCode = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    SpecNo = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Comments = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Concept = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PackageType = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RoshStatus = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    FlammabilityRating = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    RadiassionRating = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    DisassembledOrReusable = table.Column<bool>(type: "bit", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EngineeringStatus = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    PrevEngineeringStatus = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    ManufacturerId = table.Column<int>(type: "int", nullable: false),
                    ManufacturerCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductDefinitionId = table.Column<int>(type: "int", nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Manufacturers_ManufacturerId",
                        column: x => x.ManufacturerId,
                        principalTable: "Manufacturers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_ProductDefinitions_ProductDefinitionId",
                        column: x => x.ProductDefinitionId,
                        principalTable: "ProductDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    ActiveQPLSearch = table.Column<string>(type: "nvarchar(max)", maxLength: 2147483647, nullable: false),
                    CreatedById = table.Column<int>(type: "int", nullable: true),
                    UpdatedById = table.Column<int>(type: "int", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UpdatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Users_Users_CreatedById",
                        column: x => x.CreatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Users_Users_UpdatedById",
                        column: x => x.UpdatedById,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Manufacturers",
                columns: new[] { "Id", "Code", "CreatedById", "CreatedDate", "IsActive", "IsDeleted", "Name", "UpdatedById", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "UCH010", null, new DateTime(2023, 11, 29, 10, 15, 49, 526, DateTimeKind.Local).AddTicks(5188), false, false, "ARDUINO", null, new DateTime(2023, 11, 29, 10, 15, 49, 526, DateTimeKind.Local).AddTicks(5191) },
                    { 2, "RAD090", null, new DateTime(2023, 11, 29, 10, 15, 49, 526, DateTimeKind.Local).AddTicks(5193), false, false, "RADIAN HEATSINKS", null, new DateTime(2023, 11, 29, 10, 15, 49, 526, DateTimeKind.Local).AddTicks(5194) }
                });

            migrationBuilder.InsertData(
                table: "ProductDefinitions",
                columns: new[] { "Id", "CPC", "Comments", "Concept", "CreatedById", "CreatedDate", "Description", "DisassembledOrReusable", "EngineeringCode", "FlammabilityRating", "IsActive", "IsDeleted", "NortelCpc", "PackageType", "RadiassionRating", "RoshStatus", "SpecNo", "UpdatedById", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, "A9904473", "NT : A0609386", "RS1F2212", null, new DateTime(2023, 11, 29, 10, 15, 49, 525, DateTimeKind.Local).AddTicks(6141), "1/4W 200 OHM 1% SM-RES.", false, "CHR01F2212", "", false, false, "A0609386", "", "", "", "NPS25161-20", null, new DateTime(2023, 11, 29, 10, 15, 49, 525, DateTimeKind.Local).AddTicks(6156) },
                    { 2, "A9904474", "NT : A0609369  ", "RS1F2212", null, new DateTime(2023, 11, 29, 10, 15, 49, 525, DateTimeKind.Local).AddTicks(6158), "1/16W 33,2KOHM 1%SM-RES ", false, "CHR01F3322", "", false, false, "A0609369", "", "", "", "NPS25161-20", null, new DateTime(2023, 11, 29, 10, 15, 49, 525, DateTimeKind.Local).AddTicks(6159) }
                });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "IsActive", "IsDeleted", "RoleName", "UpdatedById", "UpdatedDate" },
                values: new object[] { 1, null, new DateTime(2023, 11, 29, 10, 15, 49, 530, DateTimeKind.Local).AddTicks(3793), false, false, 3, null, new DateTime(2023, 11, 29, 10, 15, 49, 530, DateTimeKind.Local).AddTicks(3799) });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedById", "CreatedDate", "EngineeringStatus", "IsActive", "IsDeleted", "ManufacturerCode", "ManufacturerId", "PrevEngineeringStatus", "ProductDefinitionId", "UpdatedById", "UpdatedDate" },
                values: new object[,]
                {
                    { 1, null, new DateTime(2023, 11, 29, 10, 15, 49, 526, DateTimeKind.Local).AddTicks(1822), "DHR01F2212", false, false, "", 1, "RS1F2212", 1, null, new DateTime(2023, 11, 29, 10, 15, 49, 526, DateTimeKind.Local).AddTicks(1826) },
                    { 2, null, new DateTime(2023, 11, 29, 10, 15, 49, 526, DateTimeKind.Local).AddTicks(1827), "DDCFEEE", false, false, "", 1, "RE21232", 1, null, new DateTime(2023, 11, 29, 10, 15, 49, 526, DateTimeKind.Local).AddTicks(1828) }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "ActiveQPLSearch", "CreatedById", "CreatedDate", "IsActive", "IsDeleted", "RoleId", "UpdatedById", "UpdatedDate", "UserName" },
                values: new object[] { 1, "", null, new DateTime(2023, 11, 29, 10, 15, 49, 531, DateTimeKind.Local).AddTicks(7513), false, false, 1, null, new DateTime(2023, 11, 29, 10, 15, 49, 531, DateTimeKind.Local).AddTicks(7516), "hiarslan" });

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_CreatedById",
                table: "Manufacturers",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Manufacturers_UpdatedById",
                table: "Manufacturers",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDefinitions_CreatedById",
                table: "ProductDefinitions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_ProductDefinitions_UpdatedById",
                table: "ProductDefinitions",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CreatedById",
                table: "Products",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ManufacturerId",
                table: "Products",
                column: "ManufacturerId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_ProductDefinitionId",
                table: "Products",
                column: "ProductDefinitionId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_UpdatedById",
                table: "Products",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_CreatedById",
                table: "Roles",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_UpdatedById",
                table: "Roles",
                column: "UpdatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_CreatedById",
                table: "Users",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleId",
                table: "Users",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UpdatedById",
                table: "Users",
                column: "UpdatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Manufacturers_Users_CreatedById",
                table: "Manufacturers",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Manufacturers_Users_UpdatedById",
                table: "Manufacturers",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDefinitions_Users_CreatedById",
                table: "ProductDefinitions",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_ProductDefinitions_Users_UpdatedById",
                table: "ProductDefinitions",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_CreatedById",
                table: "Products",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Products_Users_UpdatedById",
                table: "Products",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_CreatedById",
                table: "Roles",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Roles_Users_UpdatedById",
                table: "Roles",
                column: "UpdatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_CreatedById",
                table: "Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_Roles_Users_UpdatedById",
                table: "Roles");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Manufacturers");

            migrationBuilder.DropTable(
                name: "ProductDefinitions");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");
        }
    }
}
