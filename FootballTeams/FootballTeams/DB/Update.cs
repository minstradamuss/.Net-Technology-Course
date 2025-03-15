using System.Data.SqlClient;

namespace FootballTeams
{
    internal partial class DBOps
    {
        public void ScoreGoal(Player player)
        {
            using SqlConnection connection = new(connectionString);
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            SqlCommand command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                command.CommandText = $"SELECT TeamID FROM Player WHERE FirstName = '{player.FirstName}'";
                int teamID = int.Parse(command.ExecuteScalar().ToString());

                command.CommandText = $"UPDATE Player SET NumberOfGoals = NumberOfGoals + 1 WHERE FirstName = '{player.FirstName}'";
                command.ExecuteNonQuery();

                command.CommandText = "UPDATE Stats SET " + "AvgGoals = AvgGoals + (1 / NumberOfPlayers)" + $"WHERE TeamID = {teamID}";
                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                transaction.Rollback();
            }
        }
    }
}