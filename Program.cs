using System.Diagnostics;
using System.Drawing;
using System.Text;

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
            set =>  date = value;
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
            get => this.frequency == frequency;
        }

        public Article[] Add(params Article[] art)
        {
            Article[] arr = new Article[articles.Length + art.Length];
            for (int i = 0; i < articles.Length; i++)
            {
                arr[i] = articles[i];
            }
            for (int i = articles.Length; i < articles.Length+art.Length; i++)
            {
                arr[i] = art[i];
            }
            articles = arr;
            return articles;
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
            return $"Название-{name}, периодичность выхода журнала-{frequency}, дата выхода-{date}, номер тиража-{TirazhNum}, список статей-{Out()}";//
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

            magazine1.Add(article1, article2);

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


            // Измерение времени доступа к элементам одномерного массива
            int start = Environment.TickCount;
            for (int i = 0; i < n; i++)
            {
                string name = arr1[i].FirstName;
            }
            int duration = Environment.TickCount - start; 
            Console.WriteLine($"Одномерный массив: {duration} мс");


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