using System.ComponentModel;

namespace Hangman
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        // Words list
        List<string> words = new List<string>
        {"Peru", "Slipknot", "Japan", "Guitar", "Stratocaster",
        "Manny", "Nube", "Cassandra", "Tongo", "Telcaster",
        "Skating", "Exodus", "Chorrillos", "Xayah", "Freedom"};

        // Correct answer
        string answer;

        // Characters list
        List<char> characters = new List<char>();

        public string spotlight = "";
        public string Spotlight
        {
            get => spotlight;
            set
            {
                spotlight = value;
                OnPropertyChanged();
            }
        }

        public List<char> letters = new List<char>();
        public List<char> Letters
        {
            get => letters;
            set
            {
                letters = value;
                OnPropertyChanged();
            }
        }

        public MainPage()
        {
            BindingContext = this;
            Letters.AddRange("qwertyuiopasdfghjklzxcvbnm".ToCharArray());
            answer = RandomWord();
            MaskWord(answer, characters);
            InitializeComponent();
        }

        // Select a random word from the list
        private string RandomWord()
        {
            Random rnd = new Random();

            // Index for a word in the list
            int index = rnd.Next() % words.Count;

            // Return the random word
            return words[index];
        }

        private void MaskWord(String word, List<char> letters)
        {
            var mask = word
                .Select(x => letters.IndexOf(x) >= 0 ? x : '_')
                .ToArray();

            Spotlight = string.Join(" ", mask);
        }
    }

}
