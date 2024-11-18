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
            return $"Название-{name}, периодичность выхода журнала-{frequency}, дата выхода-{date}, номер тиража-{TirazhNum}, значение среднего рейтинга статей-{articles.Average()}";
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

        public Person() : this("Имя ", "Фамилия ", DateTime.Now)
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
}