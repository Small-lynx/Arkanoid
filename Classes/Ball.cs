using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid.Classes
{
    internal class Ball
    {
        private readonly Bitmap ball;
        private int speedX;
        private int speedY;
        private Point positionBall;
        private Rectangle display;
        private bool isEndGame;

        public Ball(Rectangle display)
        {
            this.display = display;
            ball = new Bitmap(Properties.Resources.sphere, 40, 40);
            speedX = 5;
            speedY = -12;
            isEndGame = false;
        }
        public Rectangle Move(List<Rectangle> rectangles, Rectangle racket)
        {
            positionBall.Y += speedY;
            positionBall.X += speedX;
            var temporaryBall = new Rectangle(positionBall, new Size(ball.Width, ball.Height));

            if (positionBall.X + ball.Width >= display.Width || positionBall.X <= 0)
            {
                speedX *= -1;
            }
            if (positionBall.Y + ball.Height >= display.Height || positionBall.Y <= 0)
            {
                speedY *= -1;
                if (positionBall.X + ball.Width <= display.Width)
                {
                    isEndGame = true;
                }
            }
            var destroydetRectangle = new Rectangle();
            foreach (var item in rectangles)
            {
                if (item.IntersectsWith(temporaryBall))
                {
                    speedY *= -1;
                    destroydetRectangle = item;
                }
            }

            if (racket.IntersectsWith(temporaryBall))
            {
                speedY *= -1;
            }
            return destroydetRectangle;
        }
        public Point getPositionBall()
        {
            return positionBall;
        }

        public Size getSizeBall()
        {
            return new Size(ball.Width, ball.Height);
        }

        public void setPosition(Point position)
        {
            positionBall = position;
        }

        public bool getGameMode()
        {
            return isEndGame;
        }
        public void setGemeMode()
        {
        }
    }
}
