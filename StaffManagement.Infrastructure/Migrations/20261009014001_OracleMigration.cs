using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StaffManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OracleMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DEPARTMENTS",
                columns: table => new
                {
                    DEPARTMENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NAME = table.Column<string>(type: "NVARCHAR2(30)", maxLength: 30, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DEPARTMENTS", x => x.DEPARTMENT_ID);
                });

            migrationBuilder.CreateTable(
                name: "JOB_CATEGORIES",
                columns: table => new
                {
                    CATEGORY_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    DESCRIPTION = table.Column<string>(type: "NVARCHAR2(60)", maxLength: 60, nullable: false),
                    MIN_SALARY = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: true),
                    MAX_SALARY = table.Column<decimal>(type: "DECIMAL(10,2)", precision: 10, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JOB_CATEGORIES", x => x.CATEGORY_ID);
                });

            migrationBuilder.CreateTable(
                name: "EMPLOYEES",
                columns: table => new
                {
                    EMPLOYEE_ID = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    FIRST_NAME = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    LAST_NAME = table.Column<string>(type: "NVARCHAR2(25)", maxLength: 25, nullable: true),
                    EMAIL = table.Column<string>(type: "NVARCHAR2(25)", maxLength: 25, nullable: true),
                    PHONE_NUMBER = table.Column<string>(type: "NVARCHAR2(20)", maxLength: 20, nullable: true),
                    HIRE_DATE = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    TAX_ID = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    JOB_CATEGORY_ID = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    DEPARTMENT_ID = table.Column<int>(type: "NUMBER(10)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMPLOYEES", x => x.EMPLOYEE_ID);
                    table.ForeignKey(
                        name: "FK_EMPLOYEES_DEPARTMENTS_DEPARTMENT_ID",
                        column: x => x.DEPARTMENT_ID,
                        principalTable: "DEPARTMENTS",
                        principalColumn: "DEPARTMENT_ID",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_EMPLOYEES_JOB_CATEGORIES_JOB_CATEGORY_ID",
                        column: x => x.JOB_CATEGORY_ID,
                        principalTable: "JOB_CATEGORIES",
                        principalColumn: "CATEGORY_ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEES_DEPARTMENT_ID",
                table: "EMPLOYEES",
                column: "DEPARTMENT_ID");

            migrationBuilder.CreateIndex(
                name: "IX_EMPLOYEES_JOB_CATEGORY_ID",
                table: "EMPLOYEES",
                column: "JOB_CATEGORY_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMPLOYEES");

            migrationBuilder.DropTable(
                name: "DEPARTMENTS");

            migrationBuilder.DropTable(
                name: "JOB_CATEGORIES");
        }
    }
}
