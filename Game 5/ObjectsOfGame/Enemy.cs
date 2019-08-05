using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game4
{
    class Enemy : ICanBeKilled
    {
        private Point startPoint;
        private Point _location;
        public int Speed { get; }
        public Bitmap Picture { get; }
        public bool IsKilled { get; set; }

        public static int FrequencyOfAppearing { get; set; }
        public static int AppearingLimit => 100;

        private readonly Random _rnd = new Random();

        public static List<Enemy> VisibleEnemies = new List<Enemy>();

        public Enemy()
        {
            startPoint = new Point(_rnd.Next(50, 750), 0);
            _location = startPoint;
            Speed = 10;
            Picture = new Bitmap(new Bitmap(@"C:\Users\Игорь\source\repos\Game4\Game4\Images\Enemy.png"), 60, 60);
            FrequencyOfAppearing = 0;
            IsKilled = false;
            VisibleEnemies.Add(this);
        }

        public Point GetLocation()
        {
            return _location;
        }

        public void EnemysMoving()
        {
            var delta = _rnd.Next(-20, 20);
            if (_location.X + delta > startPoint.X + 100)
                _location = new Point(startPoint.X + 100, _location.Y + Speed);
            else if (_location.X + delta < startPoint.X - 100)
                _location = new Point(startPoint.X - 100, _location.Y + Speed);
            else
                _location = new Point(_location.X + delta, _location.Y + Speed);
        }

        public void RemoveFromListOfEnemys()
        {
            VisibleEnemies.Remove(this);
            _location = new Point(1000, 1000);
        }

        public bool IsVisible()
        {
            return !IsKilled && _location.Y < GameModel.Height;
        }
    }
}
