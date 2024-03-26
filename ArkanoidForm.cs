using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Arkanoid
{
    public partial class ArkanoidForm : Form
    {
        private Graphics graphics;
        private BufferedGraphicsContext graphicsContext;
        private BufferedGraphics buffered;
        private List<Rectangle> rectangles;
        private List<Color> colorsRectangles;
        private Physics physics;
        private Bitmap ballImage;
        private Bitmap spase;
        private bool isEndGame;

        public ArkanoidForm()
        {
            InitializeComponent();
            graphics = CreateGraphics();
            graphicsContext = BufferedGraphicsManager.Current;
            buffered = graphicsContext.Allocate(graphics, DisplayRectangle);
        }


        private void ArkanoidForm_Load(object sender, EventArgs e)
        {
            isEndGame = false;
            ballImage = new Bitmap(Properties.Resources.sphere, 40, 40);
            spase = new Bitmap(Properties.Resources.space, Width, Height);
            FillingBlocks();
        }

        /// <summary>
        /// Пересоздание блоков, ракетки и мяча
        /// </summary>
        private void FillingBlocks()
        {
            physics = new Physics(new Rectangle(0, 0, Width, Height));
            rectangles = new List<Rectangle>();
            colorsRectangles = new List<Color>();
            var rnd = new Random();
            var column = 20;
            var itemColumn = column;
            var dx = 20;
            var itemWidth = (Width - dx * 2) / column;
            var itemHeight = 20;
            var indentation = 50;
            for (var row = 0; row < column / 2; row++)
            {
                for (var col = 0; col < itemColumn; col++)
                {
                    rectangles.Add(new Rectangle(dx + col * itemWidth, indentation + row * itemHeight, itemWidth, itemHeight));
                    colorsRectangles.Add(Color.FromArgb(rnd.Next(0, 256), rnd.Next(0, 256), rnd.Next(0, 256)));
                }
                dx += itemWidth;
                itemColumn -= 2;
            }
            graphics.DrawImage(ballImage, physics.getCenterRacket());
            Redraw();
            timerMoving.Start();
        }

        /// <summary>
        /// Перерисовка
        /// </summary>
        private void Redraw()
        {
            buffered.Graphics.DrawImage(spase, 0, 0);
            for (var i = 0; i < rectangles.Count; i++)
            {
                buffered.Graphics.FillRectangle(new SolidBrush(colorsRectangles[i]), rectangles[i]);
            }
            buffered.Graphics.FillRectangle(new SolidBrush(Color.Gray), physics.getRacket());
            buffered.Graphics.DrawImage(ballImage, physics.getPositionBall());
            buffered.Render();
        }

        /// <summary>
        /// Действия при тике таймера
        /// </summary>
        private void timerMoving_Tick(object sender, EventArgs e)
        {
            timerMoving.Stop();
            var item = physics.MoveBall(rectangles);
            if (!rectangles.Equals(item))
            {
                rectangles.Clear();
                rectangles.AddRange(item);
            }
            if (physics.IsEndGame())
            {
                isEndGame = true;
                MessageBox.Show("Вы проиграли(\nP.S. NucA лошара \nAbc",
                    "Игра окончена", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            Redraw();
            timerMoving.Start();
        }

        /// <summary>
        /// Движение курсора
        /// </summary>
        private void ArkanoidForm_MouseMove(object sender, MouseEventArgs e)
        {
            physics.MoveRacket(e.Location);
        }

        /// <summary>
        /// Нажатие по экрану
        /// </summary>
        private void ArkanoidForm_Click(object sender, EventArgs e)
        {
            if (isEndGame)
            {
                isEndGame = false;
                FillingBlocks();
            }
        }
    }
}
