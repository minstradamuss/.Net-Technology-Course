class DataOrg
{
    private string name;

    // замен€ем публичные пол€ на свойства с соответствующими 
    // геттерами и сеттерами
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

    // константы вместо магических чисел
    const int nameLengthCoefficient = 4;
    const int minNameLength = 3;
    const int minAge = 18ж
    const int maxAge = 65;

    public class Row
    {
        public string Name { get; set; }
        public string Age { get; set; }
        public string Time { get; set; }

        // константы вместо магических чисел
        const double coefficient = 0.83;

        public Row(string name, int age)
        {
            Name = name;
            Age = $"{age * coefficient}";
            Time = DateTime.Now.ToString();
        }
    }

    // ћетод получени€ строки данных
    public Row GetRow()
    {
        if (name == null)
            throw new ArgumentNullException("name");
        var row = new Row(name, age);
        return row;
    }

    // отдельные методы дл€ обработки данных
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

    public void CalculateNameLength()
    {
        if (name == null)
            throw new ArgumentNullException("name");
        if (IsCorrectData())
            nameLength = name.Length * nameLengthCoefficient;
    }

    // отдельный метод дл€ установки значений
    public void SetValue(string name, int value)
    {
        if (name.Equals("age"))
            SetAge(value);
        if (name.Equals("score"))
            SetScore(value);
    }

    // принцип инкапсул€ции
    private void SetAge(int value)
    {
        age = value;
    }

    private void SetScore(int value)
    {
        score = value;
    }
}
