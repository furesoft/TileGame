using Furesoft.Core.Componenting;
using Microsoft.Xna.Framework;
using MonoGame.Extended;

namespace TileGame.Components;

public class PositionComponent : Component
{
    public Vector2 Position { get; set; }
    public Size Size { get; set; }

    public Rectangle Bounds => new((int) Position.X, (int) Position.Y, Size.Width, Size.Height);

    public PositionComponent(Vector2 position, Size size)
    {
        Position = position;
        Size = size;
    }
}