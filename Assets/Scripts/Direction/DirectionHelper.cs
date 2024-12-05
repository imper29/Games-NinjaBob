using UnityEngine;

public static class DirectionHelper
{
    public static Direction GetDirection(Vector2 vector)
    {
        Direction d = Direction.None;
        if (vector.x > 0f)
            d |= Direction.Right;
        else if (vector.x < 0f)
            d |= Direction.Left;

        if (vector.y > 0f)
            d |= Direction.Up;
        else if (vector.y < 0f)
            d |= Direction.Down;

        return d;
    }
    public static Direction ReverseDirection(Direction dir)
    {
        Direction d = Direction.None;
        if ((dir & Direction.Right) != 0)
            d |= Direction.Left;
        else if ((dir & Direction.Left) != 0)
            d |= Direction.Right;

        if ((dir & Direction.Down) != 0)
            d |= Direction.Up;
        else if ((dir & Direction.Up) != 0)
            d |= Direction.Down;

        return d;
    }

    public static Vector2 GetVector(Direction direction)
    {
        Vector2 vec = Vector2.zero;
        if ((direction & Direction.Up) != 0)
            vec.y++;
        if ((direction & Direction.Down) != 0)
            vec.y--;
        if ((direction & Direction.Right) != 0)
            vec.x++;
        if ((direction & Direction.Left) != 0)
            vec.x--;

        return vec;
    }
}
