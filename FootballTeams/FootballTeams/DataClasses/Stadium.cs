namespace FootballTeams
{
    internal class Stadium
    {
        public string? Name { get; set; }
    }

    static class ExampleStadiums
    {
        public static Stadium Stadium1 = new()
        {
            Name = "Газпром Арена"
        };

        public static Stadium Stadium2 = new()
        {
            Name = "ВЭБ Арена"
        };

        public static Stadium Stadium3 = new()
        {
            Name = "Краснодар"
        };

        public static Stadium Stadium4 = new()
        {
            Name = "Лукойл Арена"
        };

        public static Stadium Stadium5 = new()
        {
            Name = "ВТБ Арена"
        };
    }
}
