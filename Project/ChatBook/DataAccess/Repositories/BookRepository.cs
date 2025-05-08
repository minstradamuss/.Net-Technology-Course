//using System;
//using System.Linq;
//using ChatBook.Entities;
//using System.Data.SQLite;
//using ChatBook.Entities;

//public class BookRepository
//{
//    private string _connectionString = "Data Source=C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\Project\\ChatBook\\ChatBook.db;";

//    public void SaveBook(Book book)
//    {
//        using (var connection = new SQLiteConnection(_connectionString))
//        {
//            connection.Open();

//            using (var command = new SQLiteCommand(connection))
//            {
//                command.CommandText = @"
//                    INSERT INTO Books (Title, Status, Rating, Review, CoverImage) 
//                    VALUES (@Title, @Status, @Rating, @Review, @CoverImage)";
//                command.Parameters.AddWithValue("@Title", book.Title);
//                command.Parameters.AddWithValue("@Status", book.Status);
//                command.Parameters.AddWithValue("@Rating", book.Rating);
//                command.Parameters.AddWithValue("@Review", book.Review);
//                command.Parameters.AddWithValue("@CoverImage", book.CoverImage ?? new byte[0]);

//                command.ExecuteNonQuery();
//            }
//        }
//    }

//    public Book GetBookById(int id)
//    {
//        using (var connection = new SQLiteConnection(_connectionString))
//        {
//            connection.Open();

//            using (var command = new SQLiteCommand("SELECT * FROM Books WHERE Id = @Id", connection))
//            {
//                command.Parameters.AddWithValue("@Id", id);

//                using (var reader = command.ExecuteReader())
//                {
//                    if (reader.Read())
//                    {
//                        return new Book
//                        {
//                            Id = Convert.ToInt32(reader["Id"]),
//                            Title = reader["Title"].ToString(),
//                            Status = reader["Status"].ToString(),
//                            Rating = Convert.ToInt32(reader["Rating"]),
//                            Review = reader["Review"].ToString(),
//                            CoverImage = reader["CoverImage"] as byte[]
//                        };
//                    }
//                }
//            }
//        }
//        return null;
//    }
//}
