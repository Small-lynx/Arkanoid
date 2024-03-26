using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arkanoid
{
    internal class Physics
    {
        private readonly Classes.Ball ball;
        private readonly Classes.Racket racket;

        public Physics(Rectangle display)
        {
            ball = new Classes.Ball(display);
            racket = new Classes.Racket(display);
        }

        public Rectangle getRacket()
        {
            return racket.getRacket();
        }

        public Point getPositionBall()
        {
            return ball.getPositionBall();
        }

        public Point getCenterRacket()
        {
            Point position = racket.getCenterRacket(ball.getSizeBall());
            ball.setPosition(position);
            return position;
        }

        public List<Rectangle> MoveBall(List<Rectangle> rectangles)
        {
            rectangles.Remove(ball.Move(rectangles, racket.getRacket()));
            return rectangles;
        }

        public void MoveRacket(Point mouseLocation)
        {
            racket.Move(mouseLocation);
        }

        public bool IsEndGame()
        {
            return ball.getGameMode();
        }
    }
}
