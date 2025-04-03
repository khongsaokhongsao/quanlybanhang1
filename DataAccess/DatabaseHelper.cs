using System.Data.SQLite;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.DataAccess
{
    public static class DatabaseHelper
    {
        private static readonly string connectionString = "Data Source=QuanLyBanHang.db;Version=3;";

        public static SQLiteConnection GetConnection()
        {
            return new SQLiteConnection(connectionString);
        }

        public static void InitializeDatabase()
        {
            using (var connection = GetConnection())
            {
                connection.Open();

                // Kiểm tra xem bảng Users đã tồn tại chưa
                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Users'";
                bool tableExists = false;
                using (var command = new SQLiteCommand(checkTableQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tableExists = true;
                        }
                    }
                }

                // Nếu bảng Users chưa tồn tại, tạo bảng và thêm dữ liệu mặc định
                if (!tableExists)
                {
                    // Tạo bảng Users
                    string createTableQuery = @"
                        CREATE TABLE Users (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            HoTen TEXT NOT NULL,
                            TaiKhoan TEXT NOT NULL,
                            MatKhau TEXT NOT NULL,
                            VaiTro TEXT,
                            Email TEXT,
                            SoDienThoai TEXT,
                            DiaChi TEXT,
                            GioiTinh TEXT,
                            HinhAnhPath TEXT
                        )";
                    using (var command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    // Thêm 2 người dùng mặc định
                    string insertUserQuery = @"
                        INSERT INTO Users (HoTen, TaiKhoan, MatKhau, VaiTro, Email, SoDienThoai, DiaChi, GioiTinh, HinhAnhPath)
                        VALUES (@HoTen, @TaiKhoan, @MatKhau, @VaiTro, @Email, @SoDienThoai, @DiaChi, @GioiTinh, @HinhAnhPath)";

                    // Người dùng 1: Quản trị viên
                    using (var command = new SQLiteCommand(insertUserQuery, connection))
                    {
                        command.Parameters.AddWithValue("@HoTen", "Nguyễn Văn Admin");
                        command.Parameters.AddWithValue("@TaiKhoan", "admin");
                        command.Parameters.AddWithValue("@MatKhau", "admin123");
                        command.Parameters.AddWithValue("@VaiTro", "Quản lý");
                        command.Parameters.AddWithValue("@Email", "admin@example.com");
                        command.Parameters.AddWithValue("@SoDienThoai", "0123456789");
                        command.Parameters.AddWithValue("@DiaChi", "Hà Nội");
                        command.Parameters.AddWithValue("@GioiTinh", "Nam");
                        command.Parameters.AddWithValue("@HinhAnhPath", "");
                        command.ExecuteNonQuery();
                    }

                    // Người dùng 2: Người dùng thông thường
                    using (var command = new SQLiteCommand(insertUserQuery, connection))
                    {
                        command.Parameters.AddWithValue("@HoTen", "Trần Thị User");
                        command.Parameters.AddWithValue("@TaiKhoan", "user");
                        command.Parameters.AddWithValue("@MatKhau", "user123");
                        command.Parameters.AddWithValue("@VaiTro", "Khách hàng");
                        command.Parameters.AddWithValue("@Email", "user@example.com");
                        command.Parameters.AddWithValue("@SoDienThoai", "0987654321");
                        command.Parameters.AddWithValue("@DiaChi", "TP. Hồ Chí Minh");
                        command.Parameters.AddWithValue("@GioiTinh", "Nữ");
                        command.Parameters.AddWithValue("@HinhAnhPath", "");
                        command.ExecuteNonQuery();
                    }
                }
                // Kiểm tra và tạo bảng Categories
                string checkCategoriesTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Categories'";
                bool categoriesTableExists = false;
                using (var command = new SQLiteCommand(checkCategoriesTableQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            categoriesTableExists = true;
                        }
                    }
                }

                if (!categoriesTableExists)
                {
                    string createCategoriesTableQuery = @"
                        CREATE TABLE Categories (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL
                        )";
                    using (var command = new SQLiteCommand(createCategoriesTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    string insertCategoriesQuery = @"
                        INSERT INTO Categories (Name) VALUES ('Điện tử');
                        INSERT INTO Categories (Name) VALUES ('Thời trang');
                    ";
                    using (var command = new SQLiteCommand(insertCategoriesQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                // Kiểm tra và tạo bảng Products
                string checkProductsTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Products'";
                bool productsTableExists = false;
                using (var command = new SQLiteCommand(checkProductsTableQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            productsTableExists = true;
                        }
                    }
                }

                if (!productsTableExists)
                {
                    string createProductsTableQuery = @"
                        CREATE TABLE Products (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            CategoryId INTEGER NOT NULL,
                            Price DECIMAL NOT NULL,
                            Stock INTEGER NOT NULL,
                            IsActive BOOLEAN NOT NULL,
                            FOREIGN KEY (CategoryId) REFERENCES Categories(Id)
                        )";
                    using (var command = new SQLiteCommand(createProductsTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    string insertProductsQuery = @"
                        INSERT INTO Products (Name, CategoryId, Price, Stock, IsActive)
                        VALUES ('Điện thoại', 1, 10000000, 50, 1);
                        INSERT INTO Products (Name, CategoryId, Price, Stock, IsActive)
                        VALUES ('Áo thun', 2, 200000, 100, 0);
                    ";
                    using (var command = new SQLiteCommand(insertProductsQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                // Kiểm tra và tạo bảng Manufacturers
                string checkManufacturersTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Manufacturers'";
                bool manufacturersTableExists = false;
                using (var command = new SQLiteCommand(checkManufacturersTableQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            manufacturersTableExists = true;
                        }
                    }
                }

                if (!manufacturersTableExists)
                {
                    string createManufacturersTableQuery = @"
                        CREATE TABLE Manufacturers (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            Name TEXT NOT NULL,
                            Address TEXT,
                            Phone TEXT
                        )";
                    using (var command = new SQLiteCommand(createManufacturersTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    string insertManufacturersQuery = @"
                        INSERT INTO Manufacturers (Name, Address, Phone)
                        VALUES ('Samsung', 'Hà Nội', '0123456789');
                        INSERT INTO Manufacturers (Name, Address, Phone)
                        VALUES ('Nike', 'TP. Hồ Chí Minh', '0987654321');
                    ";
                    using (var command = new SQLiteCommand(insertManufacturersQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
                // Kiểm tra và tạo bảng Fund
                string checkFundTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='Fund'";
                bool FundTableExists = false;
                using (var command = new SQLiteCommand(checkFundTableQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            FundTableExists = true;
                        }
                    }
                }

                if (!FundTableExists)
                {
                    string createFundTableQuery = @"
                                        CREATE TABLE Fund (
                                        Id INTEGER PRIMARY KEY AUTOINCREMENT,
                                        Bank INTEGER NOT NULL,
                                        Name TEXT NOT NULL,
                                        Type TEXT,
                                        Balance INTEGER NOT NULL,
                                        TotalTransaction INTEGER NOT NULL,
                                        Status TEXT NOT NULL
            
                                    )";
                    using (var command = new SQLiteCommand(createFundTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }

                    string insertFundQuery = @"
                                        INSERT INTO Fund (Bank, Name, Type, Balance, TotalTransaction, Status)
                                        VALUES (123456789,'Nguyễn Văn A', 'Agribank', 10000, 50, 'Active');
                                        
                                    ";
                    using (var command = new SQLiteCommand(insertFundQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }

        }
    }
}