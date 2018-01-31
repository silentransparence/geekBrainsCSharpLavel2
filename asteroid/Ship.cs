using System.Drawing;

namespace asteroid
{
    delegate void Message();

    class Ship : BaseObject
    {

        public static event Message MessageDie;

        private int energy = 100;

        public int Energy => energy;


        public void EnergyLow(int n)
        {
            energy -= n; 
        }

        public Ship(Point pos, Point dir, Size size) : base(pos, dir, size)
        {
        }

        public override void Draw()
        {
            Game.Buffer.Graphics.FillEllipse(Brushes.Wheat, Pos.X, Pos.Y, Size.Width, Size.Height);
        }

        public override void Update()
        {
        }

        public void Up()
        {
            if (Pos.Y > 0) Pos.Y = Pos.Y - Dir.Y;
        }

        public void Down()
        {
            if (Pos.Y < Game.Height) Pos.Y = Pos.Y + Dir.Y;
        }

        public void Die()
        {
            if (MessageDie != null) MessageDie();
        }

    }
}
