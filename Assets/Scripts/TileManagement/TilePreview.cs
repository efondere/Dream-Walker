using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class TilePreview : MonoBehaviour
{
    public enum Direction
    {
        Right = 0,
        Left  = 1, 
        Down  = 2, 
        Up    = 3,
    }

    public static Direction GetNextDirection(Direction currentDirection, bool clockwise)
    {
        if (clockwise)
        {
            if (currentDirection == Direction.Up)    return Direction.Right;
            if (currentDirection == Direction.Right) return Direction.Down;
            if (currentDirection == Direction.Down)  return Direction.Left;
            if (currentDirection == Direction.Left)  return Direction.Up;
        }
        else
        {
            if (currentDirection == Direction.Up)    return Direction.Left;
            if (currentDirection == Direction.Left)  return Direction.Down;
            if (currentDirection == Direction.Down)  return Direction.Right;
            if (currentDirection == Direction.Right) return Direction.Up;
        }

        return Direction.Up; // Shouldn't happen, but for some reason, C# says there is no return statement...
    }

    [SerializeField] public TileBase[] tiles;
    [SerializeField] public ResizableTilemap grid;

    [HideInInspector] public Direction direction = Direction.Up;

    public int GetTile(int x, int y)
    {
        if ((int)direction >= 2) // up or down
        {
            return grid.GetTile((direction == Direction.Up) ? x : -x, (direction == Direction.Up) ? y : -y);
        }
        else // left or right
        {
            return grid.GetTile((direction == Direction.Right) ? -y : y, (direction == Direction.Right) ? x : -x);
        }
    }

    // grid indices:
    // -1 : no tile (air)
    // <= -2: placeholder / clearance tile. We will support values other than just -2 for various types of placeholder tiles (ie. colors?)
    // >= 0: various tiles from the tiles array.
}
