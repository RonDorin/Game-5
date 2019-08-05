using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading.Tasks;

using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;


namespace Game4
{
    class MyForm : Form
    {
        private readonly Timer _timer;
        private readonly Timer _timerForEnemys;
        private readonly Dictionary<string, bool> _keysAreDown = new Dictionary<string, bool>()
            {{"Up" , false}, {"Down" , false}, {"Right" , false}, {"Left" , false}, {"Space" , false}};

        public Cosmoplane Cosmoplane = GameModel.Cosmoplane;

        public static PictureBox restart;

        public MyForm()
        {
            Size = new Size(GameModel.Width, GameModel.Height);
            InitializeComponent();
            SetStyle(ControlStyles.OptimizedDoubleBuffer
                     | ControlStyles.AllPaintingInWmPaint
                     | ControlStyles.UserPaint, true);
            KeyDown += KeyIsDown;
            _timer = new Timer() { Interval = 40 };
            _timer.Tick += Upgrade;
            KeyUp += KeyIsUp;
            _timer.Start();
            _timerForEnemys = new Timer() { Interval = 300 };
            _timerForEnemys.Start();
        }

        private void InitializeComponent()
        {
            PaintCosmoplane();
            CreateMyForm();
        }

        private void PaintCosmoplane()
        {
            Paint += (sender, e) => e.Graphics.DrawImage(Cosmoplane.Picture, Cosmoplane.GetLocation());
        }

        private void CreateMyForm()
        {
            Name = "MyForm";
            ResumeLayout(false);
            BackgroundImage = Image.FromFile(@"C:\Users\Игорь\source\repos\Game4\Game4\Images\Space.png");
        }

        private void Upgrade(object sender, EventArgs e)
        {
            DoWhenKeysIsDown();
            PaintAndLaunchEnemy();
            IsGameOver();
            Refresh();
        }

        private void IsGameOver()
        {
            Cosmoplane.CollisionWithEnemy();
            if (Cosmoplane.IsKilled)
            {
                _timer.Stop();
                _timerForEnemys.Stop();
                PaintLableGameOver();
                CreateButtonForRestart();
                ClickOnRestart();
            }
        }

        private void DoWhenKeysIsDown()
        {
            if (_keysAreDown["Up"]) Cosmoplane.MoveUp();
            if (_keysAreDown["Down"]) Cosmoplane.MoveDown();
            if (_keysAreDown["Right"]) Cosmoplane.MoveRight();
            if (_keysAreDown["Left"]) Cosmoplane.MoveLeft();
            if (_keysAreDown["Space"]) PaintAndLaunchShot(Cosmoplane.Shoot());
        }

        private void PaintAndLaunchEnemy()
        {
            Enemy.FrequencyOfAppearing++;
            if (Enemy.FrequencyOfAppearing >= Enemy.AppearingLimit)
            {
                var enemy = new Enemy();
                PaintEnemy(enemy);
                _timerForEnemys.Tick += (sender, eventArgs) =>
                {
                    if (enemy.IsVisible())
                        enemy.EnemysMoving();
                    else
                        enemy.RemoveFromListOfEnemys();
                };
            }
        }

        private void PaintEnemy(Enemy enemy)
        {
            Paint += (sender, e) =>
            {
                if (enemy.IsVisible())
                    e.Graphics.DrawImage(enemy.Picture, enemy.GetLocation());
            };
        }

        private void PaintAndLaunchShot(Shot shot)
        {
            Cosmoplane.Overcharge();
            if (Cosmoplane.QueueOfShots < 1)
            {
                PaintShot(shot);
                _timer.Tick += (sender, eventArgs) => shot.FlyOrStop();
            }
        }

        private void PaintShot(Shot shot)
        {
            Paint += (sender, e) =>
            {
                if ((shot.GetLocation().Y > 0) && (shot.IsVisible))
                    e.Graphics.DrawImage(shot.Picture, shot.GetLocation());
            };
        }

        private void PaintLableGameOver()
        {
            var bitmap = new Bitmap(@"C:\Users\Игорь\source\repos\Game 5\Game 5\Images\GameOver.png");
            var reducedBitmap = new Bitmap(bitmap, 500, 300);
            Paint += (sender, e) =>
            {
                e.Graphics.DrawImage(reducedBitmap, 150, 150);
            };
        }

        private void CreateButtonForRestart()
        {
            restart = new PictureBox();
            restart.Size = new Size(90, 90);
            restart.Location = new Point(355, 500);
            var bitmap = new Bitmap(@"C:\Users\Игорь\source\repos\Game 5\Game 5\Images\Retry.png");
            var reducedBitmap = new Bitmap(bitmap, 100, 100);
            restart.Image = reducedBitmap;
            restart.SizeMode = PictureBoxSizeMode.Zoom;
            restart.BackColor = Color.Black;
            Controls.Add(restart);
        }

        public void ClickOnRestart()
        {
            restart.Click += (sender, args) => { Application.Restart();};
            restart = null;
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    _keysAreDown["Up"] = true;
                    break;
                case Keys.Down:
                    _keysAreDown["Down"] = true;
                    break;
                case Keys.Right:
                    _keysAreDown["Right"] = true;
                    break;
                case Keys.Left:
                    _keysAreDown["Left"] = true;
                    break;
                case Keys.Space:
                    _keysAreDown["Space"] = true;
                    break;
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Up:
                    _keysAreDown["Up"] = false;
                    break;
                case Keys.Down:
                    _keysAreDown["Down"] = false;
                    break;
                case Keys.Right:
                    _keysAreDown["Right"] = false;
                    break;
                case Keys.Left:
                    _keysAreDown["Left"] = false;
                    break;
                case Keys.Space:
                    _keysAreDown["Space"] = false;
                    break;
            }
        }
    }
}