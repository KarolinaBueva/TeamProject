namespace TeamProject
{
    enum Frequency { Weekly, Monthly, Yearly }
    internal class Magazine
    {
        private string name;
        private Frequency frequency;
        private DateTime data;
        private int TirazhNum;
        //private Article[];

        public Magazine(string name, Frequency frequency, DateTime data, int TirazhNum)
        {
            this.name = name;
            this.frequency = frequency;
            this.data = data;
            this.TirazhNum = TirazhNum;
        }
        public Magazine() { }

        public string Name
        {
            get => name;
            set
            {
                name= value;
            }
        }

        public Frequency Freq
        {
            get => frequency;
            set
            {
                frequency = value;
            }
        }

        public string Name
        {
            get => name;
            set
            {
                name = value;
            }
        }

    }
}