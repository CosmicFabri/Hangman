using System.ComponentModel;

namespace Hangman
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        // Words list
        List<string> words = new List<string>
        {"Peru", "Slipknot", "Japan", "Guitar",
        "Manny", "Nube", "Cassandra", "Tongo"};

        // Characters list
        List<char> characters = new List<char>();
        //{'q', 'w', 'e', 'r', 't', 'y', 'u', 'i', 'o', 'p',
        //'a', 's', 'd', 'f', 'g', 'h', 'j', 'k', 'l', 'z',
        //'x', 'c', 'v', 'b', 'n', 'm'};

        private string spotlight = "";
        public string Spotlight
        {
            get => spotlight;
            set
            {
                spotlight = value;
                OnPropertyChanged();
            }
        }

        private List<char> letras = new List<char>();
        public List<char> Letras
        {
            get => Letras;
            set
            {
                letras = value;
                OnPropertyChanged();
            }
        }

        public MainPage()
        {
            InitializeComponent();
            BindingContext = this;
            string respuesta = RandomWord();
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
    }

}
