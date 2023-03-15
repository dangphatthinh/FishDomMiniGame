using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// note - code reference https://www.redblobgames.com/grids/hexagons/implementation.html#offset

public struct HexOffsetCoord
{
    public HexOffsetCoord(int row, int col)
    {
        this.col = col;
        this.row = row;
        this.vectorInt = new Vector2Int(row, col);
    }

    public readonly int col;
    public readonly int row;
    public readonly Vector2Int vectorInt;

    static public Vector2[][] Rdirections = new Vector2[][]
    {
        new Vector2 [] {new Vector2(0,  +1), new Vector2(-1, 0), new Vector2(-1, -1), new Vector2(0 ,- 1), new Vector2 (+1, - 1), new Vector2(+1, 0)},
        new Vector2 [] {new Vector2(0, +1), new Vector2(-1, +1), new Vector2(-1, 0), new Vector2 (0, -1), new Vector2(+1, 0), new Vector2(+1, +1)}
    };

    static public Vector2[][] Rdirections2 = new Vector2[][]
    {
        new Vector2 [] {new Vector2(0,  2), new Vector2(0, -2), new Vector2(1, -2), new Vector2(1 , 1), new Vector2 (-1, -2), new Vector2(-1, 1), new Vector2 (2, -1), new Vector2(2, 0), new Vector2(2, 1), new Vector2 (-2, -1), new Vector2(-2, 0), new Vector2(-2, 1)},
        new Vector2 [] {new Vector2(0,  2), new Vector2(0, -2), new Vector2(1, -1), new Vector2(1 , 2), new Vector2 (-1, -1), new Vector2(-1, 2), new Vector2 (2, -1), new Vector2(2, 0), new Vector2(2, 1), new Vector2 (-2, -1), new Vector2(-2, 0), new Vector2(-2, 1)}
    };

    static public Vector2 ROffsetToPixel(float radius, HexOffsetCoord off)
    {
        int even = off.row % 2 == 0 ? 0 : 1;
        double x = radius * (off.col + .5f * even - 5) * 0.99f;// shift all even rows by .5
        double y = radius * off.row * 0.84f;
        return new Vector2((float)x, (float)y);
    }

    static public HexOffsetCoord ROffsetNeighbor(HexOffsetCoord offC, int direction)
    {
        var parity = offC.row & 1;
        var dir = Rdirections[parity][direction];
        return new HexOffsetCoord(offC.row + (int)dir.x, offC.col + (int)dir.y);
    }

    static public HexOffsetCoord ROffsetNeighbor2(HexOffsetCoord offC, int index)
    {
        var parity = offC.row & 1;
        var dir = Rdirections2[parity][index];
        return new HexOffsetCoord(offC.row + (int)dir.x, offC.col + (int)dir.y);
    }
}
