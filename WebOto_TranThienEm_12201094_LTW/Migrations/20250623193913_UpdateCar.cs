using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WebOto_TranThienEm_12201094_LTW.Migrations
{
    /// <inheritdoc />
    public partial class UpdateCar : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Brands",
                columns: new[] { "Id", "LogoUrl", "Name" },
                values: new object[,]
                {
                    { 14, "/images/brands/rollsroyce.png", "Rolls-Royce" },
                    { 15, "/images/brands/astonmartin.png", "Aston Martin" }
                });

            migrationBuilder.InsertData(
                table: "Cars",
                columns: new[] { "Id", "BrandId", "CategoryId", "Color", "ContactPhone", "CreatedDate", "Description", "FuelType", "ImageUrl", "IsAvailable", "IsFeatured", "Location", "Mileage", "Name", "Price", "Transmission", "Year" },
                values: new object[,]
                {
                    { 9, 9, 6, "Đỏ", "0909000007", new DateTime(2025, 6, 22, 1, 0, 0, 0, DateTimeKind.Utc), "Ferrari 812 Superfast siêu xe, đỉnh cao của tốc độ và công nghệ.", "Xăng", "/images/cars/ferrari_812.jpg", true, false, "Hồ Chí Minh", 300, "Ferrari 812 Superfast 2024", 400000.00m, "Tự động", 2024 },
                    { 10, 12, 6, "Xám ánh kim", "0909000008", new DateTime(2025, 6, 22, 1, 5, 0, 0, DateTimeKind.Utc), "BMW M8 thể thao, hiệu suất cao, thiết kế đẳng cấp.", "Xăng", "/images/cars/bmw_m8.jpg", true, false, "Đà Nẵng", 500, "BMW M8 2024", 150000.00m, "Tự động", 2024 },
                    { 11, 13, 6, "Đen bóng", "0909000009", new DateTime(2025, 6, 22, 1, 10, 0, 0, DateTimeKind.Utc), "Audi RS7 hiệu suất cao, thiết kế thể thao và công nghệ tiên tiến.", "Xăng", "/images/cars/audi_rs7.jpg", true, false, "Cần Thơ", 400, "Audi RS7 2024", 140000.00m, "Tự động", 2024 },
                    { 12, 7, 6, "Trắng ngọc trai", "0909000010", new DateTime(2025, 6, 22, 1, 15, 0, 0, DateTimeKind.Utc), "Lexus LC 500, xe thể thao hạng sang, thiết kế đẹp mắt, vận hành mạnh mẽ.", "Xăng", "/images/cars/lexus_lc.jpg", true, false, "Hà Nội", 700, "Lexus LC 500 2024", 100000.00m, "Tự động", 2024 },
                    { 13, 10, 6, "Bạc", "0923456789", new DateTime(2025, 6, 22, 0, 10, 0, 0, DateTimeKind.Utc), "Biểu tượng Porsche 911 Carrera S với hiệu suất lái thể thao và sự thoải mái tối ưu.", "Xăng", "/images/cars/porsche_911s.jpg", true, false, "Đà Nẵng", 800, "Porsche 911 Carrera S 2024", 180000.00m, "Tự động", 2024 },
                    { 14, 11, 5, "Trắng", "0909112233", new DateTime(2025, 6, 22, 0, 15, 0, 0, DateTimeKind.Utc), "Mercedes-Benz S-Class: biểu tượng của sự sang trọng, công nghệ và tiện nghi cao cấp.", "Xăng", "/images/cars/mercedes_ss_class.jpg", true, false, "TP.HCM", 1000, "Mercedes-Benz S-Class 2024", 120000.00m, "Tự động", 2024 },
                    { 15, 14, 5, "Trắng ngọc trai", "0909000011", new DateTime(2025, 6, 22, 1, 20, 0, 0, DateTimeKind.Utc), "Rolls-Royce Phantom đẳng cấp, biểu tượng của sự sang trọng tuyệt đối.", "Xăng", "/images/cars/rolls_phantom.jpg", true, false, "Hồ Chí Minh", 100, "Rolls-Royce Phantom 2024", 450000.00m, "Tự động", 2024 },
                    { 16, 15, 6, "Đỏ rượu vang", "0909000012", new DateTime(2025, 6, 22, 1, 25, 0, 0, DateTimeKind.Utc), "Aston Martin DB11 xe thể thao hạng sang, đẳng cấp và hiệu suất cao.", "Xăng", "/images/cars/aston_db11.jpg", true, false, "Hà Nội", 600, "Aston Martin DB11 2024", 210000.00m, "Tự động", 2024 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 15);

            migrationBuilder.DeleteData(
                table: "Cars",
                keyColumn: "Id",
                keyValue: 16);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Brands",
                keyColumn: "Id",
                keyValue: 15);
        }
    }
}
