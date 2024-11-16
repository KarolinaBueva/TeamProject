using System;
using System.Diagnostics;

public class Person
{
    private string name;
    private string surname;
    private DateTime birthday;
    public Person(string name, string surname, DateTime birthday)
    {
        Name = name;
        Surname = surname;
        Birthday = birthday;
    }

    public Person() : this("Имя", "Фамилия", DateTime.Now)
    {
    }
    public string Name
    {
        get { return name; }
        set { name = value; }
    }

    public string Surname
    {
        get { return surname; }
        set { surname = value; }
    }

    public DateTime Birthday
    {
        get { return birthday; }
        set { birthday = value; }
    }
    public int BirthYear
    {
        get { return birthday.Year; }
        set
        {
            if (value < 1900 || value > DateTime.Now.Year)
                throw new ArgumentException("Год рождения должен быть в пределах от 1900 до текущего года.");
            birthday = new DateTime(value, birthday.Month, birthday.Day);
        }
    }
    public override string ToString()
    {
        return $"Имя: {Name}, Фамилия: {Surname}, Дата рождения: {Birthday.ToShortDateString()}";
    }
    public virtual string ToShortString()
    {
        return $"{Name} {Surname}";
    }
}


