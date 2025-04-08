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
                                    Bank = reader.GetInt32(1),
                                    Name = reader.GetString(2),
                                    Type = reader.GetString(3),
                                    Balance=reader.GetInt32(4),
                                    TotalTransaction = reader.GetInt32(5),
                                    Status = reader.GetString(6)
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

        public void AddQuy (Quy quy)
        {
            try
            {
                if (string.IsNullOrEmpty(quy.Name))
                {
                    throw new ArgumentException("Tên sản phẩm không được để trống");
                }

                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"
                            INSERT INTO Fund (Bank, Name, Type, Balance, TotalTransaction, Status )
                            VALUES (@Bank ,@Name, @Type, @Balance, @TotalTransaction, @Status);
                            SELECT last_insert_rowid();";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Bank", quy.Bank);
                        command.Parameters.AddWithValue("@Name", quy.Name);
                        command.Parameters.AddWithValue("@Type", quy.Type);
                        command.Parameters.AddWithValue("@Balance", quy.Balance);
                        command.Parameters.AddWithValue("@TotalTransaction", quy.TotalTransaction);
                        command.Parameters.AddWithValue("@Status", quy.Status);
                        //command.ExecuteNonQuery();

                        // Lấy ID của sản phẩm vừa thêm
                        quy.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm quỹ vào cơ sở dữ liệu: {ex.Message}", ex);
            }
        }
        public void UpdateQuy(Quy quy)
        {
            try
            {
                if (string.IsNullOrEmpty(quy.Name))
                {
                    throw new ArgumentException("Tên quỹ không được để trống");
                }

                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = @"
                        UPDATE Fund 
                        SET Bank = @Bank, Name = @Name, Type = @Type, Balance = @Balance, 
                            TotalTransaction = @TotalTransaction, Status = @Status
                        WHERE Id = @Id";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", quy.Id);
                        command.Parameters.AddWithValue("@Bank", quy.Bank);
                        command.Parameters.AddWithValue("@Name", quy.Name);
                        command.Parameters.AddWithValue("@Type", quy.Type);
                        command.Parameters.AddWithValue("@Balance", quy.Balance);
                        command.Parameters.AddWithValue("@TotalTransaction", quy.TotalTransaction);
                        command.Parameters.AddWithValue("@Status", quy.Status);

                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected == 0)
                        {
                            throw new Exception("Không tìm thấy quỹ để cập nhật.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật quỹ trong cơ sở dữ liệu: {ex.Message}", ex);
            }
        }
    }
}
