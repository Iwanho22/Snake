using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Threading;

namespace Snake
{
    class SnakeClass
    {
        #region Fields
        private Canvas _playField;

        private int _height = 20;

        private int _posX = 0;

        private int _posY = 0;

        private FoodGenerator _foodGenerator;

        private string _colorPath;
        #endregion

        #region Properties
        public List<Image> SnakeParts { get; set; }

        public Direction SnakeDriection { get; set; }

        public DispatcherTimer GameTimer { get; set; }

        public int Lenght { get; set; } = 0;

        public bool CheckDirection { get; set; } = true;
        #endregion

        #region Constructor
        public SnakeClass(Canvas playfield)
        {
            this._playField = playfield;
            this.SnakeParts = new List<Image>();
            this.SnakeDriection = Direction.Right;
            this._colorPath = @"Your Path";

            _foodGenerator = new FoodGenerator(_playField);

        }
        #endregion

        #region Methods
        public void GenerateSnake()
        {
            Image snakeHead = new Image
            {
                Source = new BitmapImage(new Uri(_colorPath))
            };

            snakeHead.Height = _height;
            snakeHead.Width = _height;

            SnakeParts.Add(snakeHead);
            _playField.Children.Add(snakeHead);
        }

        public void MoveSnake(object sender, EventArgs e)
        {
            switch (SnakeDriection)
            {
                case Direction.Right:
                    {
                        _posX += _height;
                        break;
                    }
                case Direction.Left:
                    {
                        _posX -= _height;
                        break;
                    }
                case Direction.Up:
                    {
                        _posY -= _height;
                        break;
                    }
                case Direction.Down:
                    {
                        _posY += _height;
                        break;
                    }
            }
            MoveSnakeBody();

            Canvas.SetLeft(SnakeParts[0], _posX);
            Canvas.SetTop(SnakeParts[0], _posY);

            CheckPlayField();
            CheckSnake();
            CheckForFood();

            CheckDirection = true;
        }

        public void ChangeDriektion(Key e)
        {
            if (CheckDirection == true)
            {
                if ((e == Key.W || e == Key.Up) && SnakeDriection != Direction.Down)
                {
                    SnakeDriection = Direction.Up;
                }
                else if ((e == Key.A || e == Key.Left) && SnakeDriection != Direction.Right)
                {
                    SnakeDriection = Direction.Left;
                }
                else if ((e == Key.S || e == Key.Down) && SnakeDriection != Direction.Up)
                {
                    SnakeDriection = Direction.Down;
                }
                else if ((e == Key.D || e == Key.Right) && SnakeDriection != Direction.Left)
                {
                    SnakeDriection = Direction.Right;
                }

                CheckDirection = false;
            }
        }

        private void CheckForFood()
        {
            Point head = new Point(Canvas.GetLeft(SnakeParts[0]), Canvas.GetTop(SnakeParts[0]));
            Point food = new Point(Canvas.GetLeft(_foodGenerator.Food), Canvas.GetTop(_foodGenerator.Food));

            if(head == food)
            {
                _playField.Children.Remove(_foodGenerator.Food);

                _foodGenerator.GenerateFood();
                GenerateSnakePart();
                SetScore();
            }
        }

        private void GenerateSnakePart()
        {
            Point last = new Point(Canvas.GetLeft(SnakeParts.Last()), Canvas.GetTop(SnakeParts.Last()));
            Image snakePart = new Image
            {
                Source = new BitmapImage(new Uri(_colorPath)),
                Height = _height,
                Width = _height
            };

            SnakeParts.Add(snakePart);

            switch (SnakeDriection)
            {
                case Direction.Right:
                    last.X -= 20;
                    break;
                case Direction.Left:
                    last.X += 20;
                    break;
                case Direction.Up:
                    last.Y += 20;
                    break;
                case Direction.Down:
                    last.Y -= 20;
                    break;
            }
            Canvas.SetTop(snakePart, last.Y);
            Canvas.SetLeft(snakePart, last.X);

            _playField.Children.Add(snakePart);
        }

        private void MoveSnakeBody()
        {
            Point part = new Point();

            if (SnakeParts.Count() > 1)
            {
                foreach (Image image in SnakeParts)
                {
                    if(image == SnakeParts.First())
                    {
                    }
                    else if(image == SnakeParts[1])
                    {
                        part = new Point(Canvas.GetLeft(SnakeParts[1]), Canvas.GetTop(SnakeParts[1]));

                        Canvas.SetLeft(SnakeParts[1], Canvas.GetLeft(SnakeParts.First()));
                        Canvas.SetTop(SnakeParts[1], Canvas.GetTop(SnakeParts.First()));
                    }
                    else
                    {
                        Point tempPos = new Point(Canvas.GetLeft(image), Canvas.GetTop(image));

                        Canvas.SetLeft(image, part.X);
                        Canvas.SetTop(image, part.Y);

                        part = tempPos;
                    }
                }
            }
        }

        private void CheckPlayField()
        {
            if(_posX < 0 || _posX +20 > 500)
            {
                GameTimer.Stop();
            }
            else if(_posY < 0 || _posY + 20 > 500)
            {
                StopGame();
            }
        }

        public void SetUpTimer()
        {
            GameTimer = new DispatcherTimer();
            GameTimer.Interval = new TimeSpan(0, 0, 0, 0, 100);
            GameTimer.Tick += new EventHandler(MoveSnake);
            GameTimer.Start();
        }

        private void CheckSnake()
        {
            foreach (Image part in SnakeParts)
            {
                if(part != SnakeParts.First())
                {
                    if(Canvas.GetTop(part) == _posY && Canvas.GetLeft(part) == _posX)
                    {
                        StopGame();
                    }
                }
            }
        }

        private void StopGame()
        {
            GameTimer.Stop();
        }

        private void SetScore()
        {
            var main = App.Current.MainWindow as MainWindow; // If not a static method, this.MainWindow would work
            main.Score ++;
        }
        #endregion
    }
}
