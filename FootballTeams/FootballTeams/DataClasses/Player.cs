namespace FootballTeams
{
    internal class Player
    {
        public string? FirstName { get; set; }
        public string? SecondName { get; set; }

        public class Address
        {
            public string? Country { get; set; }
            public string? City { get; set; }
            public string? Street { get; set; }
            public int Building { get; set; }
            public int Apartment { get; set; }
        }

        public Address? AddressPlayer { get; set; }
        public Team? Team { get; set; }
        public int NumberOfGoals { get; set; } = 0;
    }

    static class ExamplePlayers
    {
        public static Player Player1 = new()
        {
            FirstName = "Евгений",
            SecondName = "Латышонок",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Санкт-Петербург",
                Street = "Футбольная аллея",
                Building = 1,
                Apartment = 1,
            },
            Team = ExampleTeams.Team1,
            NumberOfGoals = 0
        };

        public static Player Player2 = new()
        {
            FirstName = "Сергей",
            SecondName = "Волков",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Санкт-Петербург",
                Street = "Футбольная аллея",
                Building = 1,
                Apartment = 1,
            },
            Team = ExampleTeams.Team1,
            NumberOfGoals = 0
        };

        public static Player Player3 = new()
        {
            FirstName = "Вильмар Энрике Барриос",
            SecondName = "Теран",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Санкт-Петербург",
                Street = "Футбольная аллея",
                Building = 1,
                Apartment = 1,
            },
            Team = ExampleTeams.Team1,
            NumberOfGoals = 3
        };

        public static Player Player4 = new()
        {
            FirstName = "Дмитрий",
            SecondName = "Васильев",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Санкт-Петербург",
                Street = "Футбольная аллея",
                Building = 1,
                Apartment = 1,
            },
            Team = ExampleTeams.Team1,
            NumberOfGoals = 2
        };

        public static Player Player5 = new()
        {
            FirstName = "Матео",
            SecondName = "Кассьерра",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Санкт-Петербург",
                Street = "Футбольная аллея",
                Building = 1,
                Apartment = 1,
            },
            Team = ExampleTeams.Team1,
            NumberOfGoals = 41
        };

        public static Player Player6 = new()
        {
            FirstName = "Илья",
            SecondName = "Помазун",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Москва",
                Street = "Волоколамское шоссе",
                Building = 69,
                Apartment = 2,
            },
            Team = ExampleTeams.Team2,
            NumberOfGoals = 2
        };

        public static Player Player7 = new()
        {
            FirstName = "Алексис",
            SecondName = "Дуарте",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Москва",
                Street = "Волоколамское шоссе",
                Building = 69,
                Apartment = 2,
            },
            Team = ExampleTeams.Team2,
            NumberOfGoals = 1
        };

        public static Player Player8 = new()
        {
            FirstName = "Олег",
            SecondName = "Рябчук",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Москва",
                Street = "Волоколамское шоссе",
                Building = 69,
                Apartment = 0,
            },
            Team = ExampleTeams.Team2,
            NumberOfGoals = 2
        };

        public static Player Player9 = new()
        {
            FirstName = "Пабло",
            SecondName = "Солари",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Москва",
                Street = "Волоколамское шоссе",
                Building = 69,
                Apartment = 2,
            },
            Team = ExampleTeams.Team2,
            NumberOfGoals = 2
        };

        public static Player Player10 = new()
        {
            FirstName = "Наиль",
            SecondName = "Умяров",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Москва",
                Street = "Волоколамское шоссе",
                Building = 69,
                Apartment = 2,
            },
            Team = ExampleTeams.Team2,
            NumberOfGoals = 4
        };

        public static Player Player11 = new()
        {
            FirstName = "Игорь",
            SecondName = "Акинфеев",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Москва",
                Street = "3-я Песчаная",
                Building = 2,
                Apartment = 1,
            },
            Team = ExampleTeams.Team3,
            NumberOfGoals = 0
        };

        public static Player Player12 = new()
        {
            FirstName = "Данил",
            SecondName = "Круговой",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Москва",
                Street = "3-я Песчаная",
                Building = 2,
                Apartment = 1,
            },
            Team = ExampleTeams.Team3,
            NumberOfGoals = 1
        };

        public static Player Player13 = new()
        {
            FirstName = "Милан",
            SecondName = "Гайич",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Москва",
                Street = "3-я Песчаная",
                Building = 2,
                Apartment = 1,
            },
            Team = ExampleTeams.Team3,
            NumberOfGoals = 0
        };

        public static Player Player14 = new()
        {
            FirstName = "Тамерлан",
            SecondName = "Мусаев",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Москва",
                Street = "3-я Песчаная",
                Building = 2,
                Apartment = 1,
            },
            Team = ExampleTeams.Team3,
            NumberOfGoals = 7
        };

        public static Player Player15 = new()
        {
            FirstName = "Игорь",
            SecondName = "Дивеев",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Москва",
                Street = "3-я Песчаная",
                Building = 2,
                Apartment = 1,
            },
            Team = ExampleTeams.Team3,
            NumberOfGoals = 4
        };

        public static Player Player16 = new()
        {
            FirstName = "Юрий",
            SecondName = "Дюпин",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Краснодар",
                Street = "Разведчика Леонова",
                Building = 1,
                Apartment = 1,
            },
            Team = ExampleTeams.Team4,
            NumberOfGoals = 0
        };

        public static Player Player17 = new()
        {
            FirstName = "Диего",
            SecondName = "Коста",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Краснодар",
                Street = "Разведчика Леонова",
                Building = 1,
                Apartment = 1,
            },
            Team = ExampleTeams.Team4,
            NumberOfGoals = 1
        };

        public static Player Player18 = new()
        {
            FirstName = "Сергей",
            SecondName = "Петров",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Краснодар",
                Street = "Разведчика Леонова",
                Building = 1,
                Apartment = 1,
            },
            Team = ExampleTeams.Team4,
            NumberOfGoals = 17
        };

        public static Player Player19 = new()
        {
            FirstName = "Данила",
            SecondName = "Козлов",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Краснодар",
                Street = "Разведчика Леонова",
                Building = 1,
                Apartment = 1,
            },
            Team = ExampleTeams.Team4,
            NumberOfGoals = 3
        };

        public static Player Player20 = new()
        {
            FirstName = "Фёдор",
            SecondName = "Смолов",
            AddressPlayer = new()
            {
                Country = "Россия",
                City = "Краснодар",
                Street = "Разведчика Леонова",
                Building = 1,
                Apartment = 1,
            },
            Team = ExampleTeams.Team4,
            NumberOfGoals = 82
        };
    }
}
