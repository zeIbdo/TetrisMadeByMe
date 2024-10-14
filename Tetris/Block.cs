using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tetris
{
    public abstract class Block
    {
        protected abstract Position[][] Tiles { get; }
        protected abstract Position StartOffSet { get; }
        public abstract int Id { get; }
        private int rotationState;
        private Position offSet;

        protected Block()
        {
            offSet = new Position(StartOffSet.Row, StartOffSet.Column);
        }
        public IEnumerable<Position> TilePositions()
        {
            foreach (var p in Tiles[rotationState])
            {
                yield return new Position(p.Row + offSet.Row, p.Column + offSet.Column);
            }
        }
        public void RotateCw()
        {
            rotationState = (rotationState + 1) % Tiles.Length;
        }
        public void RotateCCw()
        {
            if (rotationState == 0) rotationState = Tiles.Length - 1;
            else rotationState--;
        }
        public void Move(int r, int c)
        {
            offSet.Row += r;
            offSet.Column += c;
        }
        public void Reset()
        {
            rotationState = 0;
            offSet.Row = StartOffSet.Row;
            offSet.Column = StartOffSet.Column;
        }
    }
}
