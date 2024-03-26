using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Classes
{
    internal class Racket
    {
        private Rectangle racket;
        private Rectangle display;

        public Racket(Rectangle display)
        {
            this.display = display;
            racket = new Rectangle(display.Width / 2 - 90 / 2, display.Height - 200, 90, 20);
        }
        public Rectangle getRacket()
        {
            return racket;
        }
        public Point getCenterRacket(Size SizeBall)
        {
            var point = new Point(racket.X + racket.Width / 2 - SizeBall.Width / 2, racket.Y - SizeBall.Height);
            return point;
        }
        public void Move(Point mouseLocation)
        {
            racket.X = mouseLocation.X;
            if (racket.X + racket.Width >= display.Width)
            {
                racket.X = display.Width - racket.Width;
            }
        }
    }
}
