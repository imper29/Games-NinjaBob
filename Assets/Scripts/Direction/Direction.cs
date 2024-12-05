[System.Flags]
public enum Direction : byte
{
    None = 0,

    Up = 1 << 0,
    Down = 1 << 1,
    Right = 1 << 2,
    Left = 1 << 3,

    Vertical = Up | Down,
    Horizontal = Left | Right
}
