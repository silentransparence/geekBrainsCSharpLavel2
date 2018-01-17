﻿using System;
using System.Windows.Forms;
using System.Drawing;

namespace asteroid
{
    class Game
    {
        private static BufferedGraphicsContext _context;
        public static BufferedGraphics Buffer;
        // Свойства 
        // Ширина и высота игрового поля
        public static int Width { get; set; }
        public static int Height { get; set; }
        static Game()
        {
        }
        public static void Init(Form form)
        {
            // Графическое устройство для вывода графики
            Graphics g;
            // Предоставляет доступ к главному буферу графического контекста для текущего прилогжения 
            _context = BufferedGraphicsManager.Current;
            g = form.CreateGraphics(); // Создаем объект - поверхность рисования и связываем его с формой 
            // Запоминаем размеры формы
            Width = form.Width;
            Height = form.Height;
            // Связываем буферп в памяти с графичеким объектом.
            // для того, чтобы рисовать в буфере 
            Buffer = _context.Allocate(g, new Rectangle(0, 0, Width, Height));

            Load();

            Timer timer = new Timer {Interval = 100};
            timer.Start();

            timer.Tick += Timer_Tick;
        }
        public static void Draw()
        {
            // Проверяем вывод графики
            Buffer.Graphics.Clear(Color.Black);
            Buffer.Graphics.DrawRectangle(Pens.White, new Rectangle(100, 100, 200, 200));
            Buffer.Graphics.FillEllipse(Brushes.Wheat, new Rectangle(100, 100, 200, 200));
            Buffer.Render();

            Buffer.Graphics.Clear(Color.Black);
            foreach (BaseObject obj in _objs)
                obj.Draw();
            Buffer.Render();
        }

        public static void Update()
        {
            foreach (BaseObject obj in _objs)
                obj.Update();
        }

        public static BaseObject[] _objs;
        public static void Load()
        {
            _objs = new BaseObject[30];
            for (int i = 0; i < _objs.Length/2; i++)
                _objs[i] = new BaseObject(new Point(600, i * 20), new Point(i, 0), new Size(20, 20));

            for (int i = _objs.Length / 2; i < _objs.Length; i++)
                _objs[i] = new Star(new Point(600, i * 20), new Point(-i, 0), new Size(5, 5));
        }

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Draw();
            Update();
        }
    }
}
