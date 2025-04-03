using QuanLyBanHang.DataAccess;
using QuanLyBanHang.Models;
using System;
using System.Collections.Generic;
using System.Data.SQLite;

namespace QuanLyBanHang.Repositories
{
    public class QuyRepository
    {
        public List<Quy> GetAllQuy()
        {
            try
            {
                var quy = new List<Quy>();

                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Fund";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                var quyItem = new Quy
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    Type = reader.GetString(2),
                                    Balance=reader.GetInt32(3),
                                    TotalTransaction = reader.GetInt32(4),
                                    Status = reader.GetString(5)
                                };
                                quy.Add(quyItem);
                            }
                        }
                    }
                }

                return quy;
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách quỹ từ cơ sở dữ liệu: {ex.Message}", ex);
            }
        }
    }
}
