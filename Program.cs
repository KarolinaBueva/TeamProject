using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Text;
using System.Xml.Linq;

namespace TeamProject
{
    enum Frequency { Weekly, Monthly, Yearly }

    interface IRateAndCopy
    {
        double Rating { get; }
        object DeepCopy();
    }


    public class Edition
    {
        protected string Name;
        protected DateTime Data;
        protected int Copies;

        public Edition(string name, DateTime data, int copies)
        {
            Name = name;
            Data = data;
            Copies = copies;
        }

        public Edition() : this("Название", DateTime.Now, 1000) { }

        public string EditName
        {
            get => Name;
            set => Name = value;
        }

        public DateTime Date
        {
            get => Data;
            set => Data = value;
        }

        public int Tirazh
        {
            get => Copies;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Номер тиража не может быть отрицательным");
                Copies = value;
            }
        }

        public virtual object DeepCopy()
        {
            return MemberwiseClone();
        }

        public override bool Equals(object obj)
        {
            if (obj is Edition other)
            {
                return Name == other.Name && Data == other.Data && Copies == other.Copies;
            }
            return false;
        }

        public static bool operator ==(Edition e1, Edition e2)
        {
            if (ReferenceEquals(e1, null)) return ReferenceEquals(e2, null);
            return e1.Equals(e2);
        }

        public static bool operator !=(Edition e1, Edition e2) => !(e1 == e2);

        public override int GetHashCode()
        {
            return HashCode.Combine(Name, Data, Copies);
        }

        public override string ToString()
        {
            return $"Название: {Name}, дата выхода: {Data}, тираж: {Copies}";
        }
    }
    internal class Magazine: Edition, IRateAndCopy
    {
        private Frequency frequency;
        public double Rating { get; set; }
        private ArrayList editors = new ArrayList();
        private ArrayList articles = new ArrayList();

        public Magazine(string name, Frequency frequency, DateTime date, int TirazhNum)
            :base(name, date, TirazhNum)
        {
            this.frequency = frequency;
        }

        public Magazine() : base() { }

        public Frequency Frequency
        {
            get => frequency;
            set
            {
                frequency = value;
            }
        }

        public ArrayList Editors => editors;
        public ArrayList Articles => articles;

        public double AverageRating
        {
            get => articles.Count == 0 ? 0.0 : articles.Cast<Article>().Average(a => a.Rating);
        }

        public bool this[Frequency frequency]
        {
            get => this.frequency == frequency;
        }

        public void AddArticles(params Article[] art)
        {
            foreach (var article in art)
            {
                articles.Add(article);
            }
        }

        public void AddEditors(params Person[] eds)
        {
            foreach (var editor in eds)
            {
                editors.Add(editor);
            }
        }

        private string Out()
        {
            StringBuilder sb= new StringBuilder();  
            foreach (Article i in articles)
            {
                sb.AppendLine(i.ToString());
            }
            return sb.ToString();
        }

        public override string ToString()
        {
            return $"Название-{Name}, периодичность выхода журнала-{frequency}, дата выхода-{Data}, номер тиража-{Tirazh}, список статей-{Out()}";
        }

        public virtual string ToShortString()
        {
            return $"Название-{Name}, периодичность выхода журнала-{frequency}, дата выхода-{Data}, номер тиража-{Tirazh}, значение среднего рейтинга статей-{AverageRating}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Magazine other)
            {
                return Name == other.Name && frequency == other.frequency && Data == other.Data && Tirazh == other.Tirazh && articles.SequenceEqual(other.articles);
            }
            return false;
        }

        public override int GetHashCode()
        {
            
        }

        public static bool operator ==(Magazine m1, Magazine m2)
        {
            if (ReferenceEquals(m1, null)) return ReferenceEquals(m2, null);
            return m1.Equals(m2);
        }

        public static bool operator !=(Magazine m1, Magazine m2) => !(m1 == m2);

        public override object DeepCopy()
        {
            var copy = new Magazine(Name, frequency, Data, Copies)
            {
                editors = new ArrayList(editors.Cast<Person>().Select(e => e.DeepCopy()).ToArray()),
                articles = new ArrayList(articles.Cast<Article>().Select(a => a.DeepCopy()).ToArray())
            };
            return copy;
        }
    }
    public class Person
    {
        private string firstName;
        private string lastName;
        private DateTime birthDate;

        public Person(string firstName, string lastName, DateTime birthDate)
        {
            FirstName = firstName;
            LastName = lastName;
            BirthDate = birthDate;
        }

        public Person() : this("Имя ", "Фамилия ", new DateTime(2001,1,1))
        {
        }

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        public DateTime BirthDate
        {
            get { return birthDate; }
            set { birthDate = value; }
        }

        public int BirthYear
        {
            get { return birthDate.Year; }
            set
            {
                if (value < 1900 || value > DateTime.Now.Year)
                    throw new ArgumentException("Год рождения должен быть в пределах от 1900 до текущего года.");
                birthDate = new DateTime(value, birthDate.Month, birthDate.Day);
            }
        }

        public override string ToString()
        {
            return $"Имя: {FirstName}, Фамилия: {LastName}, Дата рождения: {BirthDate.ToShortDateString()}";
        }

        public virtual string ToShortString()
        {
            return $"{FirstName} {LastName}";
        }
    }
    public class Article: IRateAndCopy
    {
        public Person Author { get; set; }
        public string Title { get; set; }
        public double Rating { get; set; }

        public Article(Person author, string title, double rating)
        {
            Author = author;
            Title = title;
            Rating = rating;
        }

        public Article() : this(new Person(), "Название ", 0.0)
        {
        }

        public override string ToString()
        {
            return $"Автор: {Author.ToShortString()}, Название: {Title}, Рейтинг: {Rating}";
        }

        public override bool Equals(object obj)
        {
            if (obj is Article other)
            {
                return Author == other.Author && Title == other.Title && Rating == other.Rating;
            }
            return false;
        }

        public static bool operator ==(Article a1, Article a2)
        {
            if (ReferenceEquals(a1, null)) return ReferenceEquals(a2, null);
            return a1.Equals(a2);
        }

        public static bool operator !=(Article a1, Article a2) 
        {
            return !(a1 == a2);
        }

        public virtual object DeepCopy()
        {
            return new Article(Author.DeepCopy() as Person, string.Copy(Title), Rating);
        }
    }
    class Program { 
        static void Main()
        {  
            Person p1 = new Person("ivan", "ivanov", new DateTime(1990, 1, 1)); 
            Person p2 = new Person();

            Article article1 = new Article(p1, "Статья 1", 4.5);
            Article article2 = new Article(p2, "Статья 2", 4.8);
            Article article3 = new Article(p1, "Статья 3", 4.2);

            Magazine magazine1 = new Magazine("magaz", Frequency.Weekly, DateTime.Now, 5000);
            Magazine magazine2 = new Magazine();

            magazine1.AddArticles(article1, article2);

            Console.WriteLine(magazine1);
            Console.WriteLine();
            Console.WriteLine(magazine2.ToShortString());


            magazine2.Articles = new[] { article3 };


            Console.WriteLine($"Средний рейтинг статей в magazine1: {magazine1.AverageRating}");
            Console.WriteLine($"Средний рейтинг статей в magazine2: {magazine2.AverageRating}");

            Console.WriteLine($"информация об авторе 1: {p1}");
            Console.WriteLine($"информация об авторе 2: {p2.ToShortString()}");

            try
            {
                p1.BirthYear = 1980;
                Console.WriteLine($"Измененный год рождения автора 1: {p1.BirthDate.Year}");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine(e.Message);
            }

        const int n = 5000;

            Person[] arr1 = new Person[n*n];
            for (int i = 0; i < n; i++)
            {
                arr1[i] = new Person("ivan", "ivanov", new DateTime(2000,10,10));
            }


            Person[,] arr2 = new Person[n, n]; 
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    arr2[i, j] = new Person("petr", "petrov", new DateTime(2005,12,5));
                }
            }


            Person[][] arr3 = new Person[n][]; 
            for (int i = 0; i < n; i++)
            {
                arr3[i] = new Person[n];
                for (int j = 0; j < n; j++)
                {
                    arr3[i][j] = new Person("vasya", "vasiliev", new DateTime(2003,3,3));
                }
            }

            int start = Environment.TickCount;
            for (int i = 0; i < n; i++)
            {
                string name = arr1[i].FirstName;
            }
            int duration = Environment.TickCount - start; 
            Console.WriteLine($"Одномерный массив: {duration} мс");

            start = Environment.TickCount;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    string name = arr2[i, j].FirstName;
                }
            }
            duration = Environment.TickCount - start;
            Console.WriteLine($"Двумерный прямоугольный массив: {duration} мс");

            start = Environment.TickCount;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    string name = arr3[i][j].FirstName;
                }
            }
            duration = Environment.TickCount - start;
            Console.WriteLine($"Двумерный ступенчатый массив: {duration} мс");
    }
    }
}