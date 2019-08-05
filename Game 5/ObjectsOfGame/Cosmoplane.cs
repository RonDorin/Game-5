using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game4
{
    class Cosmoplane : ICanBeKilled
    {
        private Point _location;
        public int Speed { get; }
        public Size Size { get; set; }
        public bool IsKilled { get; set; }
        public Bitmap Picture { get; }

        public int QueueOfShots { get; set; }

        public Cosmoplane(Point cosmoplaneLocation, int speed)
        {
            _location = cosmoplaneLocation;
            IsKilled = false;
            Size = new Size(80, 80);
            QueueOfShots = 0;
            Picture = new Bitmap(new Bitmap(@"C:\Users\Игорь\source\repos\Game4\Game4\Images\Cosmoplane.png"), Size);
            Speed = speed;
        }

        public Point GetLocation()
        {
            return _location;
        }

        public Shot Shoot()
        {
            return new Shot(new Point(_location.X + 29, _location.Y - 15), 10);
        }

        public void Overcharge()
        {
            QueueOfShots++;
            if(QueueOfShots >= 10)
                QueueOfShots = 0;
        }

        public void MoveRight()
        {
            if (_location.X + Speed <= GameModel.Width - Size.Width - 10)
                _location.X += Speed;
        }

        public void MoveLeft()
        {
            if (_location.X - Speed >= -10)
                _location.X -= Speed;
        }

        public void MoveUp()
        {
            if (_location.Y - Speed >= 0)
                _location.Y -= Speed;
        }

        public void MoveDown()
        {
            if (_location.Y + Speed <= GameModel.Height - Size.Height - 30)
                _location.Y += Speed;
        }

        public void CollisionWithEnemy()
        {
            var killedEnemys = GameModel.CollideObjects(Enemy.VisibleEnemies, this);
            if (killedEnemys.Count > 0)
                IsKilled = true;
        }
    }
}
