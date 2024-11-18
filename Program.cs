using System.Diagnostics;
using System.Drawing;

namespace TeamProject
{
    enum Frequency { Weekly, Monthly, Yearly }
    internal class Magazine
    {
        private string name;
        private Frequency frequency;
        private DateTime date;
        private int TirazhNum;
        private Article[] articles;

        public Magazine(string name, Frequency frequency, DateTime date, int TirazhNum)
        {
            this.name = name;
            this.frequency = frequency;
            this.date = date;
            this.TirazhNum = TirazhNum;
            articles = new Article[0];
        }

        public Magazine()
        {
            name = "Magazine";
            frequency = Frequency.Monthly;
            date = DateTime.Now;
            TirazhNum = 1000;
            articles = new Article[0];
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }

        public Frequency Frequency
        {
            get => frequency;
            set
            {
                frequency = value;
            }
        }

        public DateTime Date
        {
            get => date;
            set
            {
                date = value;
            }
        }

        public int Num
        {
            get => TirazhNum;
            set
            {
                TirazhNum = value;
            }
        }

        public Article[] Articles
        {
            get => articles;
            set
            {
                articles = value;
            }
        }

        public double AverageRating
        {
            get => articles.Length == 0 ? 0.0 : articles.Average(article => article.Rating);
        }

        public bool this[Frequency frequency]
        {
            get => frequency == frequency;
        }

        public void AddArticles(params Article[] articles)
        {
            articles = articles.Concat(articles).ToArray();
        }

        public override string ToString()
        {
            return $"Название-{name}, периодичность выхода журнала-{frequency}, дата выхода-{date}, номер тиража-{TirazhNum}, список статей-{articles}";
        }

        public virtual string ToShortString()
        {
            return $"Название-{name}, периодичность выхода журнала-{frequency}, дата выхода-{date}, номер тиража-{TirazhNum}, значение среднего рейтинга статей-{AverageRating}";
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
    public class Article
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
        static void Main()
        {  
            Person p1 = new Person("ivan", "ivanov", new DateTime(1990, 1, 1)); 
            Person p2 = new Person();

            Console.WriteLine(p1.ToString()); Console.WriteLine(p1.ToShortString());
            Console.WriteLine(p2.ToString()); Console.WriteLine(p2.ToShortString());
            try
            {
                p1.BirthYear = 1985; 
                Console.WriteLine($"новый год рождения: {p1.BirthYear}");
                Console.WriteLine(p1.ToString());
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }


            const int n = 1000;

            Person[] arr1 = new Person[n];
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


            // Измерение времени доступа к элементам одномерного массива
            int start = Environment.TickCount;
            for (int i = 0; i < n; i++)
            {
                string name = arr1[i].FirstName;
            }
            int duration = Environment.TickCount - start; Console.WriteLine($"Одномерный массив: {duration} мс");


            // Измерение времени доступа к элементам двумерного прямоугольного массива
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


            // Измерение времени доступа к элементам двумерного ступенчатого массива
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