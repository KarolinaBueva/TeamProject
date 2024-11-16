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

}