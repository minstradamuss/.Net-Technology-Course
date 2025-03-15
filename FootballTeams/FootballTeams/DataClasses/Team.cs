namespace FootballTeams
{
    internal class Team
    {
        public string? Name { get; set; }
        public int Year { get; set; }
    }

    static class ExampleTeams
    {
        public static Team Team1 = new()
        {
            Name = "Зенит",
            Year = 2024,
        };

        public static Team Team2 = new()
        {
            Name = "Спартак",
            Year = 2024,
        };

        public static Team Team3 = new()
        {
            Name = "ЦСКА",
            Year = 2024,
        };

        public static Team Team4 = new()
        {
            Name = "Краснодар",
            Year = 2024,
        };
    }
}
