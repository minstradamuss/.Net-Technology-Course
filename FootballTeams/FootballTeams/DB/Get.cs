using System.Data.SQLite;

namespace FootballTeams
{
    internal partial class DBOps
    {

        public void PrintTeamPlayers(Team team)
        {
            using SQLiteConnection connection = new(connectionString);
            connection.Open();

            string sqlExpression = "SELECT FirstName, SecondName FROM Player WHERE TeamID = @TeamID;";

            using SQLiteCommand command = new(sqlExpression, connection);
            command.Parameters.AddWithValue("@TeamID", GetTeamId(team));

            using SQLiteDataReader reader = command.ExecuteReader();

            if (!reader.HasRows)
            {
                Console.WriteLine($"У команды {team.Name} нет игроков.");
                return;
            }

            Console.WriteLine($"Игроки команды {team.Name}:");
            while (reader.Read())
            {
                string firstName = reader.GetString(0);
                string lastName = reader.GetString(1);
                Console.WriteLine($"{firstName} {lastName}");
            }
        }

    }
}
