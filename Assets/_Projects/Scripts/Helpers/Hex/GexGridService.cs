using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this service class is not generic, its tightly coupled with type of grid laout game is using at the moment
public static class GexGridService
{
    public enum HorizontalRowLayout
    {
        Odd,
        Even,
    }
    // converts hexcoordinates to pixel coordinates
    public static Vector2 HexOffsetCoordToPixel(HexOffsetCoord hexOffset)
    {
        float gridCellSize = 1f;

        Vector2 pixelPosition = HexOffsetCoord.ROffsetToPixel(gridCellSize, hexOffset);

        return pixelPosition;
    }

    // returns all neighbour based on the input coord
    public static HexOffsetCoord GetNeightbourCoord(HexOffsetCoord hexOffset, int direction)
    {
        HexOffsetCoord neighbour = HexOffsetCoord.ROffsetNeighbor(hexOffset, direction);
        return neighbour;
    }

    public static HexOffsetCoord GetNeightbourCoord2(HexOffsetCoord hexOffset, int index)
    {
        HexOffsetCoord neighbour = HexOffsetCoord.ROffsetNeighbor2(hexOffset, index);
        return neighbour;
    }

    public static List<HexOffsetCoord> GetAllCoordsInRow(int row)
    {
        List<HexOffsetCoord> neighbourCoords = new List<HexOffsetCoord>();
        for (int i = 0, c = row % 2 == 1 ? 10 : 11; i < c; i++) // 6 neighbour 
        {
            neighbourCoords.Add(new HexOffsetCoord(row, i));

        }
        return neighbourCoords;
    }

    // returns all 6 neighbour coordinates
    public static List<HexOffsetCoord> GetAllNeightbourCoords(HexOffsetCoord hexOffset)
    {
        List<HexOffsetCoord> neighbourCoords = new List<HexOffsetCoord>();
        for (int i = 0; i < 6; i++) // 6 neighbour 
        {
            neighbourCoords.Add(GetNeightbourCoord(hexOffset, i));

        }
        return neighbourCoords;
    }

    // returns all  neighbour coordinates
    public static List<HexOffsetCoord> GetAllNeightbourCoords(HexOffsetCoord hexOffset, int radius)
    {
        List<HexOffsetCoord> neighbourCoords = new List<HexOffsetCoord>();
        if (radius >= 1)
        {
            for (int i = 0; i < 6; i++) // 6 neighbour 
            {
                neighbourCoords.Add(GetNeightbourCoord(hexOffset, i));
            }
        }
        if (radius >= 2)
        {
            for (int jj = 0; jj < 12; jj++)
            {
                neighbourCoords.Add(GetNeightbourCoord2(hexOffset, jj));
            }
        }
        if (radius >= 3)
        {
            throw new System.InvalidOperationException();
        }

        //cleanup invaild hexcell

        return neighbourCoords;
    }
}
