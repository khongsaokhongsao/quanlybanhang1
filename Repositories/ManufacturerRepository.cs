using System;
using System.Collections.Generic;
using System.Data.SQLite;
using QuanLyBanHang.DataAccess;
using QuanLyBanHang.Models;

namespace QuanLyBanHang.Repositories
{
    public class ManufacturerRepository
    {
        public List<ManufacturerModel> GetAllManufacturers()
        {
            var manufacturers = new List<ManufacturerModel>();

            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "SELECT * FROM Manufacturers";
                using (var command = new SQLiteCommand(query, connection))
                {
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var manufacturer = new ManufacturerModel
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Address = reader.IsDBNull(2) ? null : reader.GetString(2),
                                Phone = reader.IsDBNull(3) ? null : reader.GetString(3)
                            };
                            manufacturers.Add(manufacturer);
                        }
                    }
                }
            }

            return manufacturers;
        }

        public void AddManufacturer(ManufacturerModel manufacturer)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = @"
                    INSERT INTO Manufacturers (Name, Address, Phone)
                    VALUES (@Name, @Address, @Phone);
                    SELECT last_insert_rowid();";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", manufacturer.Name);
                    command.Parameters.AddWithValue("@Address", (object)manufacturer.Address ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", (object)manufacturer.Phone ?? DBNull.Value);

                    manufacturer.Id = Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        public void UpdateManufacturer(ManufacturerModel manufacturer)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = @"
                    UPDATE Manufacturers
                    SET Name = @Name, Address = @Address, Phone = @Phone
                    WHERE Id = @Id";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", manufacturer.Id);
                    command.Parameters.AddWithValue("@Name", manufacturer.Name);
                    command.Parameters.AddWithValue("@Address", (object)manufacturer.Address ?? DBNull.Value);
                    command.Parameters.AddWithValue("@Phone", (object)manufacturer.Phone ?? DBNull.Value);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void DeleteManufacturer(int manufacturerId)
        {
            using (var connection = DatabaseHelper.GetConnection())
            {
                connection.Open();
                string query = "DELETE FROM Manufacturers WHERE Id = @Id";
                using (var command = new SQLiteCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", manufacturerId);
                    command.ExecuteNonQuery();
                }
            }
        }
    }
}