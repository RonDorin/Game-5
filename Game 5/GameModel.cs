using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Game4
{
    class GameModel
    {
        public static int Width = 800;
        public static int Height = 800;
        public static Cosmoplane Cosmoplane = new Cosmoplane(new Point(GameModel.Width / 2 - 30, GameModel.Height / 2 - 60), 7);

        public static List<Enemy> CollideObjects(List<Enemy> objectsCollection, IMovingObject controllableObject)
        {
            return objectsCollection
                .Where(x => (x.GetLocation().X + 50 >= controllableObject.GetLocation().X) && 
                            (x.GetLocation().X - 40 <= controllableObject.GetLocation().X) &&
                            (x.GetLocation().Y + 40 >= controllableObject.GetLocation().Y) && 
                            (x.GetLocation().Y - 50 <= controllableObject.GetLocation().Y))
                .ToList();
        }
    }
}



