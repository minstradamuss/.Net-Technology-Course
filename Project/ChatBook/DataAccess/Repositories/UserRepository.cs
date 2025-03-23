//using System;
//using System.Data.SQLite;
//using ChatBook.Entities;

//public class UserRepository
//{
//    private string _connectionString = "Data Source=C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\Project\\ChatBook\\ChatBook.db;";

//    public void UpdateUser(User user)
//    {
//        using (var connection = new SQLiteConnection(_connectionString))
//        {
//            connection.Open();

//            using (var command = new SQLiteCommand(connection))
//            {
//                command.CommandText = @"
//                    UPDATE Users 
//                    SET FirstName = @FirstName, 
//                        LastName = @LastName, 
//                        PhoneNumber = @PhoneNumber, 
//                        Avatar = @Avatar
//                    WHERE Id = @Id";

//                command.Parameters.AddWithValue("@FirstName", user.FirstName);
//                command.Parameters.AddWithValue("@LastName", user.LastName);
//                command.Parameters.AddWithValue("@PhoneNumber", user.PhoneNumber);
//                command.Parameters.AddWithValue("@Avatar", user.Avatar ?? new byte[0]);
//                command.Parameters.AddWithValue("@Id", user.Id);

//                command.ExecuteNonQuery();
//            }
//        }
//    }

//    public User GetUserById(int id)
//    {
//        using (var connection = new SQLiteConnection(_connectionString))
//        {
//            connection.Open();

//            using (var command = new SQLiteCommand("SELECT * FROM Users WHERE Id = @Id", connection))
//            {
//                command.Parameters.AddWithValue("@Id", id);

//                using (var reader = command.ExecuteReader())
//                {
//                    if (reader.Read())
//                    {
//                        return new User
//                        {
//                            Id = Convert.ToInt32(reader["Id"]),
//                            Nickname = reader["Nickname"].ToString(),
//                            FirstName = reader["FirstName"].ToString(),
//                            LastName = reader["LastName"].ToString(),
//                            PhoneNumber = reader["PhoneNumber"].ToString(),
//                            Avatar = reader["Avatar"] as byte[]
//                        };
//                    }
//                }
//            }
//        }
//        return null;
//    }
//}
