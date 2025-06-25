using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebOto_TranThienEm_12201094_LTW.Models;

namespace WebOto_TranThienEm_12201094_LTW.Models.Data
{
    public class CarDbContext : IdentityDbContext<User>
    {
        public CarDbContext(DbContextOptions<CarDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Car> Cars { get; set; }
        public DbSet<Brand> Brands { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Contact> Contacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Brand)
                .WithMany(b => b.Cars)
                .HasForeignKey(c => c.BrandId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Car>()
                .HasOne(c => c.Category)
                .WithMany(cat => cat.Cars)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Brand>().HasData(
                new Brand { Id = 1, Name = "Toyota", LogoUrl = "/images/brands/toyota.png" },
                new Brand { Id = 2, Name = "Honda", LogoUrl = "/images/brands/honda.png" },
                new Brand { Id = 3, Name = "Mazda", LogoUrl = "/images/brands/mazda.png" },
                new Brand { Id = 4, Name = "Hyundai", LogoUrl = "/images/brands/hyundai.png" },
                new Brand { Id = 5, Name = "Chevrolet", LogoUrl = "/images/brands/chevrolet.png" },
                new Brand { Id = 6, Name = "Ford", LogoUrl = "/images/brands/ford.png" },
                new Brand { Id = 7, Name = "Lexus", LogoUrl = "/images/brands/lexus.png" },
                new Brand { Id = 8, Name = "Maserati", LogoUrl = "/images/brands/maserati.png" },
                new Brand { Id = 9, Name = "Ferrari", LogoUrl = "/images/brands/ferrari.png" },
                new Brand { Id = 10, Name = "Porsche", LogoUrl = "/images/brands/porsche.png" },
                new Brand { Id = 11, Name = "Mercedes-Benz", LogoUrl = "/images/brands/mercedes.png" },
                new Brand { Id = 12, Name = "BMW", LogoUrl = "/images/brands/bmw.png" },
                new Brand { Id = 13, Name = "Audi", LogoUrl = "/images/brands/audi.png" },
                new Brand { Id = 14, Name = "Rolls-Royce", LogoUrl = "/images/brands/rollsroyce.png" },
                new Brand { Id = 15, Name = "Aston Martin", LogoUrl = "/images/brands/astonmartin.png" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Sedan", Description = "Xe sedan 4 cửa" },
                new Category { Id = 2, Name = "SUV", Description = "Xe SUV đa dụng" },
                new Category { Id = 3, Name = "Hatchback", Description = "Xe hatchback nhỏ gọn" },
                new Category { Id = 4, Name = "Pickup", Description = "Xe bán tải" },
                new Category { Id = 5, Name = "Luxury", Description = "Xe hạng sang" },
                new Category { Id = 6, Name = "Sports Car", Description = "Xe thể thao" }
            );

            // Seed Cars
            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    Id = 1,
                    Name = "Toyota Camry 2023",
                    BrandId = 1,
                    CategoryId = 1,
                    Year = 2023,
                    Price = 30000.00m,
                    Mileage = 5000,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Đen",
                    Description = "Mô tả Toyota Camry đời mới nhất, sang trọng và tiết kiệm nhiên liệu.",
                    ImageUrl = "/images/cars/camry.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 21, 23, 15, 0, DateTimeKind.Utc),
                    Location = "TP.HCM",
                    ContactPhone = "0123456789"
                },
                new Car
                {
                    Id = 2,
                    Name = "Honda CR-V 2024",
                    BrandId = 2,
                    CategoryId = 2,
                    Year = 2024,
                    Price = 45000.00m,
                    Mileage = 2000,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Trắng",
                    Description = "Mô tả Honda CR-V, rộng rãi và tiện nghi.",
                    ImageUrl = "/images/cars/crv.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 21, 23, 20, 0, DateTimeKind.Utc),
                    Location = "Hà Nội",
                    ContactPhone = "0987654321"
                },
                new Car
                {
                    Id = 3,
                    Name = "Maserati Ghibli 2023",
                    BrandId = 8,
                    CategoryId = 5,
                    Year = 2023,
                    Price = 90000.00m,
                    Mileage = 1500,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Xám",
                    Description = "Maserati Ghibli sang trọng với hiệu suất vượt trội và thiết kế đẳng cấp.",
                    ImageUrl = "/images/cars/maserati_ghibli.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 22, 0, 0, 0, DateTimeKind.Utc),
                    Location = "TP.HCM",
                    ContactPhone = "0901234567"
                },
                new Car
                {
                    Id = 4,
                    Name = "Ferrari F8 Tributo 2024",
                    BrandId = 9,
                    CategoryId = 6,
                    Year = 2024,
                    Price = 350000.00m,
                    Mileage = 500,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Đỏ",
                    Description = "Siêu xe Ferrari F8 Tributo với động cơ mạnh mẽ và thiết kế khí động học.",
                    ImageUrl = "/images/cars/ferrari_f8.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 22, 0, 5, 0, DateTimeKind.Utc),
                    Location = "Hà Nội",
                    ContactPhone = "0912345678"
                },
                new Car
                {
                    Id = 5,
                    Name = "Porsche 911 Carrera S 2024",
                    BrandId = 10,
                    CategoryId = 6,
                    Year = 2024,
                    Price = 180000.00m,
                    Mileage = 800,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Bạc",
                    Description = "Biểu tượng Porsche 911 Carrera S với hiệu suất lái thể thao và sự thoải mái tối ưu.",
                    ImageUrl = "/images/cars/porsche_911.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 22, 0, 10, 0, DateTimeKind.Utc),
                    Location = "Đà Nẵng",
                    ContactPhone = "0923456789"
                },
                // Các xe hạng sang, thể thao cao cấp
                new Car
                {
                    Id = 6,
                    Name = "Mercedes-Benz S-Class 2024",
                    BrandId = 11,
                    CategoryId = 5,
                    Year = 2024,
                    Price = 120000.00m,
                    Mileage = 1000,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Trắng",
                    Description = "Mercedes-Benz S-Class: biểu tượng của sự sang trọng, công nghệ và tiện nghi cao cấp.",
                    ImageUrl = "/images/cars/mercedes_s_class.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 22, 0, 15, 0, DateTimeKind.Utc),
                    Location = "TP.HCM",
                    ContactPhone = "0909112233"
                },
                new Car
                {
                    Id = 7,
                    Name = "BMW 7 Series 2023",
                    BrandId = 12,
                    CategoryId = 5,
                    Year = 2023,
                    Price = 105000.00m,
                    Mileage = 2500,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Đen",
                    Description = "BMW 7 Series: sự kết hợp hoàn hảo giữa hiệu suất, thiết kế tinh tế và trải nghiệm lái đỉnh cao.",
                    ImageUrl = "/images/cars/bmw_7_series.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 22, 0, 20, 0, DateTimeKind.Utc),
                    Location = "Hà Nội",
                    ContactPhone = "0988776655"
                },
                new Car
                {
                    Id = 8,
                    Name = "Audi A8 2024",
                    BrandId = 13,
                    CategoryId = 5,
                    Year = 2024,
                    Price = 98000.00m,
                    Mileage = 1200,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Xám ánh kim",
                    Description = "Audi A8: sự tinh tế trong từng chi tiết, công nghệ tiên tiến và không gian nội thất sang trọng.",
                    ImageUrl = "/images/cars/audi_a8.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 22, 0, 25, 0, DateTimeKind.Utc),
                    Location = "Cần Thơ",
                    ContactPhone = "0919223344"
                },
                new Car
                {
                    Id = 9,
                    Name = "Ferrari 812 Superfast 2024",
                    BrandId = 9,
                    CategoryId = 6,
                    Year = 2024,
                    Price = 400000.00m,
                    Mileage = 300,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Đỏ",
                    Description = "Ferrari 812 Superfast siêu xe, đỉnh cao của tốc độ và công nghệ.",
                    ImageUrl = "/images/cars/ferrari_812.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 22, 1, 0, 0, DateTimeKind.Utc),
                    Location = "Hồ Chí Minh",
                    ContactPhone = "0909000007"
                },
                new Car
                {
                    Id = 10,
                    Name = "BMW M8 2024",
                    BrandId = 12,
                    CategoryId = 6,
                    Year = 2024,
                    Price = 150000.00m,
                    Mileage = 500,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Xám ánh kim",
                    Description = "BMW M8 thể thao, hiệu suất cao, thiết kế đẳng cấp.",
                    ImageUrl = "/images/cars/bmw_m8.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 22, 1, 5, 0, DateTimeKind.Utc),
                    Location = "Đà Nẵng",
                    ContactPhone = "0909000008"
                },
                new Car
                {
                    Id = 11,
                    Name = "Audi RS7 2024",
                    BrandId = 13,
                    CategoryId = 6,
                    Year = 2024,
                    Price = 140000.00m,
                    Mileage = 400,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Đen bóng",
                    Description = "Audi RS7 hiệu suất cao, thiết kế thể thao và công nghệ tiên tiến.",
                    ImageUrl = "/images/cars/audi_rs7.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 22, 1, 10, 0, DateTimeKind.Utc),
                    Location = "Cần Thơ",
                    ContactPhone = "0909000009"
                },
                new Car
                {
                    Id = 12,
                    Name = "Lexus LC 500 2024",
                    BrandId = 7,
                    CategoryId = 6,
                    Year = 2024,
                    Price = 100000.00m,
                    Mileage = 700,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Trắng ngọc trai",
                    Description = "Lexus LC 500, xe thể thao hạng sang, thiết kế đẹp mắt, vận hành mạnh mẽ.",
                    ImageUrl = "/images/cars/lexus_lc.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 22, 1, 15, 0, DateTimeKind.Utc),
                    Location = "Hà Nội",
                    ContactPhone = "0909000010"
                },
                new Car
                {
                    Id = 13,
                    Name = "Porsche 911 Carrera S 2024",
                    BrandId = 10,
                    CategoryId = 6,
                    Year = 2024,
                    Price = 180000.00m,
                    Mileage = 800,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Bạc",
                    Description = "Biểu tượng Porsche 911 Carrera S với hiệu suất lái thể thao và sự thoải mái tối ưu.",
                    ImageUrl = "/images/cars/porsche_911s.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 22, 0, 10, 0, DateTimeKind.Utc),
                    Location = "Đà Nẵng",
                    ContactPhone = "0923456789"
                },
                new Car
                {
                    Id = 14,
                    Name = "Mercedes-Benz S-Class 2024",
                    BrandId = 11,
                    CategoryId = 5,
                    Year = 2024,
                    Price = 120000.00m,
                    Mileage = 1000,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Trắng",
                    Description = "Mercedes-Benz S-Class: biểu tượng của sự sang trọng, công nghệ và tiện nghi cao cấp.",
                    ImageUrl = "/images/cars/mercedes_ss_class.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 22, 0, 15, 0, DateTimeKind.Utc),
                    Location = "TP.HCM",
                    ContactPhone = "0909112233"
                },
                new Car
                {
                    Id = 15,
                    Name = "Rolls-Royce Phantom 2024",
                    BrandId = 14,
                    CategoryId = 5,
                    Year = 2024,
                    Price = 450000.00m,
                    Mileage = 100,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Trắng ngọc trai",
                    Description = "Rolls-Royce Phantom đẳng cấp, biểu tượng của sự sang trọng tuyệt đối.",
                    ImageUrl = "/images/cars/rolls_phantom.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 22, 1, 20, 0, DateTimeKind.Utc),
                    Location = "Hồ Chí Minh",
                    ContactPhone = "0909000011"
                },
                new Car
                {
                    Id = 16,
                    Name = "Aston Martin DB11 2024",
                    BrandId = 15,
                    CategoryId = 6,
                    Year = 2024,
                    Price = 210000.00m,
                    Mileage = 600,
                    FuelType = "Xăng",
                    Transmission = "Tự động",
                    Color = "Đỏ rượu vang",
                    Description = "Aston Martin DB11 xe thể thao hạng sang, đẳng cấp và hiệu suất cao.",
                    ImageUrl = "/images/cars/aston_db11.jpg",
                    IsAvailable = true,
                    CreatedDate = new DateTime(2025, 6, 22, 1, 25, 0, DateTimeKind.Utc),
                    Location = "Hà Nội",
                    ContactPhone = "0909000012"
                }
            );
        }
    }
}