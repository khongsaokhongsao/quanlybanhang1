    using System;
    using System.Collections.Generic;
    using System.Data.SQLite;
    using QuanLyBanHang.DataAccess;
    using QuanLyBanHang.Models;

    namespace QuanLyBanHang.Repositories
    {
        public class ProductRepository
        {
            public List<Product> GetAllProducts()
            {
                try
                {   
                    var products = new List<Product>();

                    using (var connection = DatabaseHelper.GetConnection())
                    {
                        connection.Open();
                        string query = "SELECT * FROM Products";
                        using (var command = new SQLiteCommand(query, connection))
                        {
                            using (var reader = command.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var product = new Product
                                    {
                                        Id = reader.GetInt32(0),
                                        Name = reader.GetString(1),
                                        CategoryId = reader.GetInt32(2),
                                        Price = reader.GetDecimal(3),
                                        Stock = reader.GetInt32(4),
                                        IsActive = reader.GetBoolean(5)
                                    };
                                    products.Add(product);
                                }
                            }
                        }
                    }

                    return products;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lấy danh sách sản phẩm từ cơ sở dữ liệu: {ex.Message}", ex);
                }
            }

            public void AddProduct(Product product)
            {
                try
                {
                    if (string.IsNullOrEmpty(product.Name))
                    {
                        throw new ArgumentException("Tên sản phẩm không được để trống");
                    }

                    using (var connection = DatabaseHelper.GetConnection())
                    {
                        connection.Open();
                        string query = @"
                            INSERT INTO Products (Name, CategoryId, Price, Stock, IsActive)
                            VALUES (@Name, @CategoryId, @Price, @Stock, @IsActive);
                            SELECT last_insert_rowid();";
                        using (var command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Name", product.Name);
                            command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                            command.Parameters.AddWithValue("@Price", product.Price);
                            command.Parameters.AddWithValue("@Stock", product.Stock);
                            command.Parameters.AddWithValue("@IsActive", product.IsActive);
                            //command.ExecuteNonQuery();

                            // Lấy ID của sản phẩm vừa thêm
                            product.Id = Convert.ToInt32(command.ExecuteScalar());
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi thêm sản phẩm vào cơ sở dữ liệu: {ex.Message}", ex);
                }
            }

            public bool UpdateProduct(Product product)
            {
                try 
                {    
                    if (string.IsNullOrEmpty(product.Name))
                    {
                        throw new ArgumentException("Tên sản phẩm không được để trống");
                    }
                    using (var connection = DatabaseHelper.GetConnection())
                    {
                        connection.Open();
                        string query = @"
                            UPDATE Products
                            SET Name = @Name, 
                                CategoryId = @CategoryId, 
                                Price = @Price, 
                                Stock = @Stock, 
                                IsActive = @IsActive
                            WHERE Id = @Id";
                        using (var command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Id", product.Id);
                            command.Parameters.AddWithValue("@Name", product.Name);
                            command.Parameters.AddWithValue("@CategoryId", product.CategoryId);
                            command.Parameters.AddWithValue("@Price", product.Price);
                            command.Parameters.AddWithValue("@Stock", product.Stock);
                            command.Parameters.AddWithValue("@IsActive", product.IsActive);

                            int rowsAffected = command.ExecuteNonQuery();
                            return rowsAffected > 0;
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi cập nhật sản phẩm trong cơ sở dữ liệu: {ex.Message}", ex);
                }
            }

            public void DeleteProduct(int productId)
            {   
                try 
                {    
                    using (var connection = DatabaseHelper.GetConnection())
                    {
                        connection.Open();
                        string query = "DELETE FROM Products WHERE Id = @Id";
                        using (var command = new SQLiteCommand(query, connection))
                        {
                            command.Parameters.AddWithValue("@Id", productId);
                            command.ExecuteNonQuery();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi xóa sản phẩm từ cơ sở dữ liệu: {ex.Message}", ex);
                }
            }

            public List<Category> GetAllCategories()
            {
                try 
                {
                
                    var categories = new List<Category>();

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
                                    var category = new Category
                                    {
                                        Id = reader.GetInt32(0),
                                        Name = reader.GetString(1)
                                    };
                                    categories.Add(category);
                                }
                            }
                        }
                    }

                    return categories;
                }
                catch (Exception ex)
                {
                    throw new Exception($"Lỗi khi lấy danh sách danh muc từ cơ sở dữ liệu: {ex.Message}", ex);
                }
            }
        }
    }