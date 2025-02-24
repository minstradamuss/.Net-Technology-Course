class DataOrg
{
    private string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    private int age, score;
    public int Age
    {
        get { return age; }
        set { age = value; }
    }
    public int Score
    {
        get { return score; }
        set { score = value; }
    }

    int nameLength;

    //убрали магические константы 
    const int nameLenghtCoefficient = 4, minNameLength = 3;
    const int minAge = 18, maxAge = 65;

    //сделали отдельные новые классы, каждый со своей функциональностью
    public class Row
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Time { get; set; }

        //убрали магические константы 
        const double koeff = 0.83;

        public Row(string name, int age)
        {
            Name = name;
            Age = $"{age * koeff}";
            Time = DateTime.Now.ToString();
        }
    }

    public Row? GetRow()
    {
        if (name == null)
            throw new ArgumentNullException("name");
        var row = new Row(name, age);
        return row;
    }

    //выносим проверки специальных случаев в отдельное условие 
    private bool IsCorrectData()
    {
        if (name.Length < minNameLength)
            return false;
        if (age < minAge || age > maxAge)
            return false;
        if (score == -1)
            return false;
        return true;
    }

    //теперь название метода раскрывает то, что он делает
    public void CalculateNameLength()
    {
        if (name == null)
            throw new ArgumentNullException("name");
        if (IsCorrectData())
            nameLength = name.Length * nameLenghtCoefficient;
    }

    public void SetValue(string name, int value)
    {
        if (name.Equals("age"))
            SetAge(value);
        if (name.Equals("score"))
            SetScore(value);
    }

    //для полей создаем геттер и сеттер и пользуемся далее только ими
    private void SetAge(int value)
    {
        age = value;
    }

    private void SetScore(int value)
    {
        score = value;
    }
}