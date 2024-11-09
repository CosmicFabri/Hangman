using System.ComponentModel;
using System.Threading.Tasks;

namespace Hangman
{
    public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        // Words list
        List<string> words = new List<string>
        {"PERU", "SLIPKNOT", "JAPAN", "GUITAR", "STRATOCASTER",
        "MANNY", "NUBE", "CASSANDRA", "TONGO", "TELECASTER",
        "SKATING", "EXODUS", "CHORRILLOS", "XAYAH", "FREEDOM"};

        // Correct answer
        private string answer;

        // Incorrect characters list
        List<char> chosenLetters = new List<char>();

        // Pressed buttons list
        List<Button> pressedButtons = new List<Button>();

        // Message property
        private string message = "";
        public string Message
        {
            get => message;
            set
            {
                message = value;
                OnPropertyChanged(nameof(Message));
            }
        }

        // Status property
        private string status = "Errors: 0 out of 6";
        public string Status
        {
            get => status;
            set
            {
                if (status != value)
                {
                    status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        // Image property
        private string myImage = "img0.jpg";
        public string MyImage
        {
            get => myImage;
            set
            {
                if (myImage != value)
                {
                    myImage = value;
                    OnPropertyChanged(nameof(MyImage));
                }
            }
        }

        // Errors variable
        private string errors = "0";

        // Spotlight property
        private string spotlight = "";
        public string Spotlight
        {
            get => spotlight;
            set
            {
                if (spotlight != value)
                {
                    spotlight = value;
                    OnPropertyChanged(nameof(Spotlight));
                }
            }
        }

        private List<char> letters = new List<char>();
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
            // Our XAML will be able to
            // bind data from this class
            BindingContext = this;
            Letters.AddRange("QWERTYUIOPASDFGHJKLZXCVBNM".ToCharArray());
            answer = RandomWord();
            MaskWord(answer, chosenLetters);
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

        private void MaskWord(string word, List<char> letters)
        {
            var mask = word
                .Select(x => letters.IndexOf(x) >= 0 ? x : '_')
                .ToArray();

            Spotlight = string.Join(" ", mask);
        }

        private void RestartWord(string word)
        {
            var mask = word
                .Select(c => char.IsLetter(c) ? '_' : c)
                .ToArray();

            Spotlight = string.Join(" ", mask);
        }

        // Button click handler
        private void OnButtonClicked(object sender, EventArgs args)
        {
            // Casting the sender to a button
            Button btn = (Button)sender;

            // Obtaining the text inside the button
            char btnText = (btn.Text.ToCharArray())[0];

            // Deactivating the button
            btn.IsEnabled = false;

            // Sending it to the pressed buttons list
            pressedButtons.Add(btn);

            HandleLetter(btnText);
            UpdateStatus();
        }

        // Restart button handler
        private void OnRestartButtonClicked(object sender, EventArgs args)
        {
            // Resetting error count
            errors = "0";
            UpdateStatus();

            // Resseting message
            Message = "";

            // Choosing a new word
            answer = RandomWord();

            // Clearing the list of selected letters
            chosenLetters.Clear();

            // Masking the new word
            MaskWord(answer, chosenLetters);

            // Enabling all buttons again
            EnableAllButtons();
        }
        private void HandleLetter(char letter)
        {
            int numErrors = Int32.Parse(errors);

            if (chosenLetters.IndexOf(letter) == -1)
            {
                chosenLetters.Add(letter);
            }

            if (answer.IndexOf(letter) >= 0)
            {
                MaskWord(answer, chosenLetters);
                CheckToWin();
            } else
            {
                numErrors++;
            }

            errors = numErrors.ToString();
        }

        private void CheckToWin()
        {
            if (Spotlight.Replace(" ", "") == answer)
            {
                Message = "You win!";
                DisableAllExceptRestart();
            }
        }

        private void UpdateStatus()
        {
            Status = $"Errors: {errors} out of 6";
            MyImage = $"img{errors}.jpg";

            if (errors == "6")
            {
                Message = "You lose!";
                DisableAllExceptRestart();
            }
        }

        private void DisableAllExceptRestart()
        {
            foreach (var view in myFlexLayout.Children)
            {
                if (view is Button button && button.Text != "Restart")
                {
                    button.IsEnabled = false;
                }
            }
        }

        private void EnableAllButtons()
        {
            foreach (var view in myFlexLayout.Children)
            {
                if (view is Button button)
                {
                    button.IsEnabled = true;
                }
            }
        }

    }

}
