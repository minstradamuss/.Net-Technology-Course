// See https://aka.ms/new-console-template for more information
using FootballTeams;

string connectionString = "Data Source=C:\\Users\\User\\source\\repos\\.Net-Technology-Course\\FootballTeams\\FootballTeams\\Football.db;";
DBOps db = new DBOps(connectionString);

db.ClearAll();

db.AddTeam(ExampleTeams.Team1);
db.AddTeam(ExampleTeams.Team2);
db.AddTeam(ExampleTeams.Team3);
db.AddTeam(ExampleTeams.Team4);

db.AddStadium(ExampleStadiums.Stadium1);
db.AddStadium(ExampleStadiums.Stadium2);
db.AddStadium(ExampleStadiums.Stadium3);
db.AddStadium(ExampleStadiums.Stadium4);
db.AddStadium(ExampleStadiums.Stadium5);

db.AddPlayer(ExamplePlayers.Player1);
db.AddPlayer(ExamplePlayers.Player2);
db.AddPlayer(ExamplePlayers.Player3);
db.AddPlayer(ExamplePlayers.Player4);
db.AddPlayer(ExamplePlayers.Player5);
db.AddPlayer(ExamplePlayers.Player6);
db.AddPlayer(ExamplePlayers.Player7);
db.AddPlayer(ExamplePlayers.Player8);
db.AddPlayer(ExamplePlayers.Player9);
db.AddPlayer(ExamplePlayers.Player10);
db.AddPlayer(ExamplePlayers.Player11);
db.AddPlayer(ExamplePlayers.Player12);
db.AddPlayer(ExamplePlayers.Player13);
db.AddPlayer(ExamplePlayers.Player14);
db.AddPlayer(ExamplePlayers.Player15);
db.AddPlayer(ExamplePlayers.Player16);
db.AddPlayer(ExamplePlayers.Player17);
db.AddPlayer(ExamplePlayers.Player18);
db.AddPlayer(ExamplePlayers.Player19);
db.AddPlayer(ExamplePlayers.Player20);

Console.WriteLine("ID team1: " + db.GetTeamId(ExampleTeams.Team1));

db.PrintTeamPlayers(ExampleTeams.Team1);
db.PrintTeamPlayers(ExampleTeams.Team2);

db.DeletePlayer(ExamplePlayers.Player20);
