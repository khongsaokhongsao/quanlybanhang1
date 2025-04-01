using System;
using System.Collections.Generic;
using System.Data.SQLite;
using QuanLyBanHang.Models;
using QuanLyBanHang.Helpers;
using QuanLyBanHang.DataAccess;

namespace QuanLyBanHang.Repositories
{
    public class CategoryRepository
    {
        public List<Category> GetAllCategories()
        {
            var categories = new List<Category>();
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT * FROM Categories";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        using (var reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                categories.Add(new Category
                                {
                                    Id = Convert.ToInt32(reader["Id"]),
                                    Name = reader["Name"].ToString()
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy danh sách danh mục: {ex.Message}", ex);
            }
            return categories;
        }

        public void AddCategory(Category category)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "INSERT INTO Categories (Name) VALUES (@Name); SELECT last_insert_rowid();";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", category.Name);
                        category.Id = Convert.ToInt32(command.ExecuteScalar());
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi thêm danh mục: {ex.Message}", ex);
            }
        }

        public void UpdateCategory(Category category)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "UPDATE Categories SET Name = @Name WHERE Id = @Id";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Id", category.Id);
                        command.Parameters.AddWithValue("@Name", category.Name);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật danh mục: {ex.Message}", ex);
            }
        }

        public void DeleteCategory(int categoryId)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    // Kiểm tra xem danh mục có sản phẩm liên quan không
                    string checkQuery = "SELECT COUNT(*) FROM Products WHERE CategoryId = @CategoryId";
                    using (var checkCommand = new SQLiteCommand(checkQuery, connection))
                    {
                        checkCommand.Parameters.AddWithValue("@CategoryId", categoryId);
                        long productCount = (long)checkCommand.ExecuteScalar();
                        if (productCount > 0)
                        {
                            throw new InvalidOperationException("Không thể xóa danh mục vì vẫn còn sản phẩm thuộc danh mục này!");
                        }
                    }

                    // Xóa danh mục
                    string deleteQuery = "DELETE FROM Categories WHERE Id = @Id";
                    using (var command = new SQLiteCommand(deleteQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Id", categoryId);
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa danh mục: {ex.Message}", ex);
            }
        }

        public bool IsCategoryNameExists(string name, int excludeId = 0)
        {
            try
            {
                using (var connection = DatabaseHelper.GetConnection())
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM Categories WHERE Name = @Name AND Id != @ExcludeId";
                    using (var command = new SQLiteCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", name);
                        command.Parameters.AddWithValue("@ExcludeId", excludeId);
                        long count = (long)command.ExecuteScalar();
                        return count > 0;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi kiểm tra tên danh mục: {ex.Message}", ex);
            }
        }
    }
}