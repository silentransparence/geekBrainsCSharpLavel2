using System;
using System.Windows.Forms;
using System.Drawing;

namespace asteroid
{
    class Game
    {
        static BaseObject[] objs;
        static Bullet bullet;
        static Asteroid[] asteroids;
        static Ship ship = new Ship(new Point(10, 400), new Point(5, 5), new Size(10, 10));

        private static BufferedGraphicsContext context;
        public static BufferedGraphics Buffer;
        static public Random rnd = new Random();

        public static int Width { get; set; }
        public static int Height { get; set; }
        static Timer timer = new Timer();

        static Game()
        {
        }

        public static void Init(Form form)
        {
            Graphics g;            
            context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics(); 
            Width = form.Width;
            Height = form.Height;
            Buffer = context.Allocate(g, new Rectangle(0, 0, Width, Height));
            Load();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
            form.KeyDown += Form_KeyDown;
            Ship.MessageDie += Finish;
        }

        private static void Form_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ControlKey) bullet = new Bullet(new Point(ship.Rect.X + 10, ship.Rect.Y + 4), new Point(4, 0), new Size(4, 1));
            if (e.KeyCode == Keys.Up) ship.Up();
            if (e.KeyCode == Keys.Down) ship.Down();
        }

        public static void Draw()
        {
            Buffer.Graphics.Clear(Color.Black);

            foreach (BaseObject obj in objs)
                obj.Draw();

            foreach (Asteroid a in asteroids)
                if (a != null) a.Draw();
            if (bullet != null) bullet.Draw();

            ship.Draw();
            Buffer.Graphics.DrawString("Energy:" + ship.Energy, SystemFonts.DefaultFont, Brushes.White, 0, 0);

            Buffer.Render();
        }

        static public void Update()
        {
            foreach (BaseObject obj in objs) obj.Update();

            if (bullet != null) bullet.Update();
            for (int i = 0; i < asteroids.Length; i++)
            {
                if (asteroids[i] != null)
                {
                    asteroids[i].Update();
                    if (bullet != null && bullet.Collision(asteroids[i]))
                    {
                        System.Media.SystemSounds.Hand.Play();
                        asteroids[i] = null;
                        bullet = null;
                        continue;
                    }
                    if (ship.Collision(asteroids[i]))
                    {
                        ship.EnergyLow(rnd.Next(1, 10));
                        System.Media.SystemSounds.Asterisk.Play();
                        if (ship.Energy <= 0) ship.Die();
                    }
                }
            }
        }

        public static void Load()
        {
            objs = new BaseObject[30];
            bullet = new Bullet(new Point(0, 255), new Point(5, 0), new Size(4, 1));
            asteroids = new Asteroid[3];

            for (int i = 0; i < objs.Length; i++)
            {
                int r = rnd.Next(5, 50);
                objs[i] = new Star(new Point(500, Game.rnd.Next(0, Game.Height)), new Point(-r, 0), new Size(3, 3));
            }
            for (int i = 0; i < asteroids.Length; i++)
            {
                int r = rnd.Next(5, 50);
                asteroids[i] = new Asteroid(new Point(800, Game.rnd.Next(0, Game.Height)), new Point(-5, 0), new Size(r, r));
            }
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }

        static public void Finish()
        {
            timer.Stop();
            Buffer.Graphics.DrawString("Game over", new Font(FontFamily.GenericSansSerif, 60, FontStyle.Underline), Brushes.White, 200, 100);
            Buffer.Render();
        }
    }
}
