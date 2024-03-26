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

        /// <summary>
        /// Возвращяет прямоугольник ракетки
        /// </summary>
        /// <returns></returns>
        public Rectangle getRacket()
        {
            return racket.getRacket();
        }

        /// <summary>
        /// Возвращяет координаты мяча
        /// </summary>
        /// <returns></returns>
        public Point getPositionBall()
        {
            return ball.getPositionBall();
        }

        /// <summary>
        /// Возвращяет координаты центра ракетки и задает новую позицию мяча
        /// </summary>
        /// <returns></returns>
        public Point getCenterRacket()
        {
            Point position = racket.getCenterRacket(ball.getSizeBall());
            ball.setPosition(position);
            return position;
        }

        /// <summary>
        /// Движение мяча
        /// </summary>
        /// <param name="rectangles">Список блоков</param>
        /// <returns></returns>
        public List<Rectangle> MoveBall(List<Rectangle> rectangles)
        {
            rectangles.Remove(ball.Move(rectangles, racket.getRacket()));
            return rectangles;
        }

        /// <summary>
        /// Движение ракетки
        /// </summary>
        /// <param name="mouseLocation">Координаты курсора</param>
        public void MoveRacket(Point mouseLocation)
        {
            racket.Move(mouseLocation);
        }

        /// <summary>
        /// Возвращяет значение окончания игры
        /// </summary>
        /// <returns></returns>
        public bool IsEndGame()
        {
            return ball.getEndGame();
        }
    }
}
