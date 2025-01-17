﻿namespace Tetris;

public class OBlock : Block
{
    public override int Id => 4;

    protected override Position[][] Tiles => new Position[][]
    {
        new Position[]{new(0,0),new(0,1),new(1,0),new(1,1)}
    };

    protected override Position StartOffSet => new Position(0, 4);
}
