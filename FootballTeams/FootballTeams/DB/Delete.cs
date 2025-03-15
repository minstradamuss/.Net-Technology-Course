using System.Data.SQLite;

namespace FootballTeams
{
    internal partial class DBOps
    {

        public void DeletePlayer(Player player)
        {
            using SQLiteConnection connection = new(connectionString);
            connection.Open();
            using SQLiteTransaction transaction = connection.BeginTransaction();

            using SQLiteCommand command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = "SELECT PlayerID, TeamID, AddressID FROM Player WHERE FirstName = @FirstName AND SecondName = @SecondName;";
                command.Parameters.AddWithValue("@FirstName", player.FirstName);
                command.Parameters.AddWithValue("@SecondName", player.SecondName);

                using var reader = command.ExecuteReader();
                if (!reader.Read())
                {
                    Console.WriteLine($"Игрок {player.FirstName} {player.SecondName} не найден.");
                    return;
                }

                int playerID = reader.GetInt32(0);
                int teamID = reader.GetInt32(1);
                int addressID = reader.GetInt32(2);
                reader.Close();
                command.Parameters.Clear();

                command.CommandText = "DELETE FROM Player WHERE PlayerID = @PlayerID;";
                command.Parameters.AddWithValue("@PlayerID", playerID);
                int rowsAffected = command.ExecuteNonQuery();
                command.Parameters.Clear();

                if (rowsAffected == 0)
                {
                    Console.WriteLine($"Игрок {player.FirstName} {player.SecondName} не найден для удаления.");
                    return;
                }

                command.CommandText = "DELETE FROM Address WHERE AddressID = @AddressID;";
                command.Parameters.AddWithValue("@AddressID", addressID);
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                command.CommandText = "UPDATE Stats SET " +
                                      "AvgGoals = (AvgGoals * NumberOfPlayers - @Goals) / NULLIF((NumberOfPlayers - 1), 0), " +
                                      "NumberOfPlayers = NumberOfPlayers - 1 " +
                                      "WHERE TeamID = @TeamID;";
                command.Parameters.AddWithValue("@Goals", player.NumberOfGoals);
                command.Parameters.AddWithValue("@TeamID", teamID);
                command.ExecuteNonQuery();

                transaction.Commit();
                Console.WriteLine($"Игрок {player.FirstName} {player.SecondName} удалён.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при удалении игрока: {ex.Message}");
                transaction.Rollback();
            }
        }


        public void ClearAll()
        {
            using SQLiteConnection connection = new(connectionString);
            connection.Open();
            using SQLiteTransaction transaction = connection.BeginTransaction();

            using SQLiteCommand command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = "DELETE FROM Player;";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM Address;";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM Stats;";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM Team_Stadium;";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM Team;";
                command.ExecuteNonQuery();

                command.CommandText = "DELETE FROM Stadium;";
                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка при очистке базы данных: {ex.Message}");
                transaction.Rollback();
            }
        }
     
    }
}
