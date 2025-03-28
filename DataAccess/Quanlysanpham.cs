using System;
using System.Data.SQLite;

namespace QuanLyBanHang.DataAccess
{
    public static class QuanLySanPham
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

                // Kiểm tra xem bảng SanPham đã tồn tại chưa
                string checkTableQuery = "SELECT name FROM sqlite_master WHERE type='table' AND name='SanPham'";
                bool tableExists = false;

                using (var command = new SQLiteCommand(checkTableQuery, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            tableExists = true; // Nếu bảng đã tồn tại
                        }
                    }
                }

                // Nếu bảng chưa tồn tại, tạo bảng mới
                if (!tableExists)
                {
                    string createTableQuery = @"
                        CREATE TABLE SanPham (
                            Id INTEGER PRIMARY KEY AUTOINCREMENT,
                            TenSanPham TEXT NOT NULL,
                            DanhMucSanPham TEXT NOT NULL,
                            SoLuong INTEGER NOT NULL,
                            Gia REAL NOT NULL,
                            MoTa TEXT,
                            HinhAnh TEXT
                        )";

                    using (var command = new SQLiteCommand(createTableQuery, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                // Thêm sản phẩm nếu chưa tồn tại
                string checkProductQuery = "SELECT COUNT(*) FROM SanPham WHERE TenSanPham = @TenSanPham";

                using (var command = new SQLiteCommand(checkProductQuery, connection))
                {
                    command.Parameters.AddWithValue("@TenSanPham", "Cafe");

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count == 0) // Sản phẩm chưa tồn tại
                    {
                        string insertProductQuery = @"
                            INSERT INTO SanPham (TenSanPham, DanhMucSanPham, SoLuong, Gia, MoTa, HinhAnh)
                            VALUES (@TenSanPham, @DanhMucSanPham, @SoLuong, @Gia, @MoTa, @HinhAnh)";

                        using (var insertCommand = new SQLiteCommand(insertProductQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@TenSanPham", "Cafe");
                            insertCommand.Parameters.AddWithValue("@DanhMucSanPham", "Đồ uống");
                            insertCommand.Parameters.AddWithValue("@SoLuong", 50);
                            insertCommand.Parameters.AddWithValue("@Gia", 1500000);
                            insertCommand.Parameters.AddWithValue("@MoTa", "Dở");
                            insertCommand.Parameters.AddWithValue("@HinhAnh", "");

                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }

                using (var command = new SQLiteCommand(checkProductQuery, connection))
                {
                    command.Parameters.AddWithValue("@TenSanPham", "Trà");

                    int count = Convert.ToInt32(command.ExecuteScalar());

                    if (count == 0) // Sản phẩm chưa tồn tại
                    {
                        string insertProductQuery = @"
                            INSERT INTO SanPham (TenSanPham, DanhMucSanPham, SoLuong, Gia, MoTa, HinhAnh)
                            VALUES (@TenSanPham, @DanhMucSanPham, @SoLuong, @Gia, @MoTa, @HinhAnh)";

                        using (var insertCommand = new SQLiteCommand(insertProductQuery, connection))
                        {
                            insertCommand.Parameters.AddWithValue("@TenSanPham", "Trà");
                            insertCommand.Parameters.AddWithValue("@DanhMucSanPham", "Đồ uống");
                            insertCommand.Parameters.AddWithValue("@SoLuong", 50);
                            insertCommand.Parameters.AddWithValue("@Gia", 180000);
                            insertCommand.Parameters.AddWithValue("@MoTa", "Ngon");
                            insertCommand.Parameters.AddWithValue("@HinhAnh", "");

                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}
