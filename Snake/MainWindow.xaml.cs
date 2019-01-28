using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Snake
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private SnakeClass _snake;

        public int Score
        {
            get => _snake.Lenght;

            set
            {
                _snake.Lenght = value;
                lbScore.Content = $"Score:\n {_snake.Lenght}";
            }
        } 

        public MainWindow()
        {
            InitializeComponent();
            field.Source = new BitmapImage(new Uri(@"Your Patth"));
            field.Stretch = Stretch.Fill;

            _snake = new SnakeClass(PlayField);
            _snake.GenerateSnake();
        }

        #region Methods
        private void IsKeyDown(object sender, KeyEventArgs e)
        {
            if (IsDrirectionKey(e.Key))
            {
                _snake.ChangeDriektion(e.Key);
            }
            else if(e.Key == Key.Space)
            {
                StartGame();
            }
        }

        private bool IsDrirectionKey(Key e)
        {
            bool check = false;

            if (e.ToString() == "W" || e.ToString() == "A" || e.ToString() == "s" || e.ToString() == "D")
            {
                check = true;
            }
            else if (e.ToString() == "Up" || e.ToString() == "Left" || e.ToString() == "Down" || e.ToString() == "Right")
            {
                check = true;
            }

            return check;
        }

        private void StartGame()
        {
            lbStart.Visibility = Visibility.Hidden;

            _snake.SetUpTimer();
        }
        #endregion
    }
}
