using System;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Snake
{
    class FoodGenerator
    {
        #region Fields
        private int _height = 20;

        private Canvas _playField;
        
        private string _colorpath = @"C:\Users\Ivo\Desktop\Ivo\Beruf\1. Lehrjahr\Projekte\Snake\Snake\Snake\Images\red.png";

        private int _posX;

        private int _posY;

        private Image _food;
        #endregion

        #region Properties
        public Image Food
        {
            get => _food;
        }

        public int PosX
        {
            get => _posX;
        }

        public int PosY
        {
            get => _posY;
        }
        #endregion

        #region Methods
        public FoodGenerator(Canvas playField)
        {
            this._playField = playField;
            GenerateFood();
        }

        public void GenerateFood()
        {
            Random random = new Random();
            _posX = random.Next(0, 25) * _height;
            _posY = random.Next(0, 25) * _height;

            _food = new Image();
            _food.Source = new BitmapImage(new Uri(_colorpath));
            _food.Stretch = System.Windows.Media.Stretch.Fill;
            _food.Height = _height;
            _food.Width = _height;

            Canvas.SetTop(_food, _posY);
            Canvas.SetLeft(_food, _posX);

            _playField.Children.Add(_food);
        }
        #endregion
    }
}
