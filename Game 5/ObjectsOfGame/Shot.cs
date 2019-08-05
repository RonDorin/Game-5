using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game4
{
    class Shot : IMovingObject
    {
        private Point _location;
        public int Speed { get; }
        public Bitmap Picture { get; }
        public bool IsVisible { get; set; }

        public static List<Shot> VisibleShots = new List<Shot>();

        public Shot(Point startLocation, int speed)
        {
            _location = startLocation;
            Speed = speed;
            Picture = new Bitmap(new Bitmap(@"C:\Users\Игорь\source\repos\Game4\Game4\Images\Shot.png"), 20, 20);
            VisibleShots.Add(this);
            IsVisible = true;
        }

        public Point GetLocation()
        {
            return _location;
        }

        public void FlyOrStop()
        {
            if ((_location.Y >= 0) && (IsVisible))
                _location.Y -= Speed;
            else
            {
                _location = new Point(-1, -1);
                VisibleShots.Remove(this);
            }
            Kill();
        }

        public void Kill()
        {
            var killedEnemys = GameModel.CollideObjects(Enemy.VisibleEnemies, this);

            foreach (var killedEnemy in killedEnemys)
            {
                killedEnemy.IsKilled = true;
                IsVisible = false;
            }
        }
    }
}
