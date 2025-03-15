using System.Data.SQLite;

namespace FootballTeams
{
    internal partial class DBOps
    {
        private readonly string connectionString;

        public DBOps(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void AddPlayer(Player player)
        {
            using SQLiteConnection connection = new(connectionString);
            connection.Open();
            using SQLiteTransaction transaction = connection.BeginTransaction();

            using SQLiteCommand command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = "INSERT INTO Address (Country, City, Street, Building, Apartment) " +
                                      "VALUES (@Country, @City, @Street, @Building, @Apartment); " +
                                      "SELECT last_insert_rowid();";

                command.Parameters.AddWithValue("@Country", player.AddressPlayer.Country);
                command.Parameters.AddWithValue("@City", player.AddressPlayer.City);
                command.Parameters.AddWithValue("@Street", player.AddressPlayer.Street);
                command.Parameters.AddWithValue("@Building", player.AddressPlayer.Building);
                command.Parameters.AddWithValue("@Apartment", player.AddressPlayer.Apartment);

                long addressID = (long)command.ExecuteScalar();
                command.Parameters.Clear();

                long teamID = GetTeamId(player.Team);

                command.CommandText = "INSERT INTO Player (FirstName, SecondName, AddressID, TeamID, NumberOfGoals) " +
                                      "VALUES (@FirstName, @SecondName, @AddressID, @TeamID, @NumberOfGoals);";

                command.Parameters.AddWithValue("@FirstName", player.FirstName);
                command.Parameters.AddWithValue("@SecondName", player.SecondName);
                command.Parameters.AddWithValue("@AddressID", addressID);
                command.Parameters.AddWithValue("@TeamID", teamID);
                command.Parameters.AddWithValue("@NumberOfGoals", player.NumberOfGoals);
                command.ExecuteNonQuery();
                command.Parameters.Clear();

                command.CommandText = "UPDATE Stats SET " +
                                      "AvgGoals = (AvgGoals * NumberOfPlayers + @Goals) / (NumberOfPlayers + 1), " +
                                      "NumberOfPlayers = NumberOfPlayers + 1 " +
                                      "WHERE TeamID = @TeamID;";

                command.Parameters.AddWithValue("@Goals", player.NumberOfGoals);
                command.Parameters.AddWithValue("@TeamID", teamID);
                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
            }
        }

        public void AddTeam(Team team)
        {
            using SQLiteConnection connection = new(connectionString);
            connection.Open();
            using SQLiteTransaction transaction = connection.BeginTransaction();

            using SQLiteCommand command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = "INSERT INTO Team (TeamName, Year) VALUES (@TeamName, @Year); " +
                                      "SELECT last_insert_rowid();";

                command.Parameters.AddWithValue("@TeamName", team.Name);
                command.Parameters.AddWithValue("@Year", team.Year);

                long teamID = (long)command.ExecuteScalar();
                command.Parameters.Clear();

                command.CommandText = "INSERT INTO Stats (TeamID, NumberOfPlayers, NumberOfStadiums, AvgGoals) " +
                                      "VALUES (@TeamID, 0, 0, 0);";

                command.Parameters.AddWithValue("@TeamID", teamID);
                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
            }
        }

        public void AddStadium(Stadium stadium)
        {
            using SQLiteConnection connection = new(connectionString);
            connection.Open();

            string sqlExpression = "INSERT INTO Stadium (StadiumName) VALUES (@StadiumName);";
            using SQLiteCommand command = new(sqlExpression, connection);
            command.Parameters.AddWithValue("@StadiumName", stadium.Name);
            command.ExecuteNonQuery();
        }

        public long GetTeamId(Team team)
        {
            using SQLiteConnection connection = new(connectionString);
            connection.Open();
            using SQLiteCommand command = new()
            {
                Connection = connection,
                CommandText = "SELECT TeamID FROM Team WHERE TeamName = @TeamName;"
            };
            command.Parameters.AddWithValue("@TeamName", team.Name);
            object result = command.ExecuteScalar();
            return result != null ? Convert.ToInt64(result) : -1;
        }
    }
}
