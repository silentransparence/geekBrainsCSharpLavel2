using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace asteroid
{
    class Asteroid : BaseObject
    {

        Image img = Image.FromFile(@"Image\asteroid.png");

        public Asteroid(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            //Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
            Game.Buffer.Graphics.DrawImage(img, Pos);
        }

        public override void Update()
        {
            Pos.X = Pos.X + Dir.X;
            if (Pos.X < 0) 
            {
                Pos.X = 1000;
                Pos.Y = Game.rnd.Next(0, Game.Height);
                Dir.X = -Game.rnd.Next(1, 10);
            }
        }
    }
}

