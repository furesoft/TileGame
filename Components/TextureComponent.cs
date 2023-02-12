using Microsoft.Xna.Framework.Graphics;
using TileGame.Core;

namespace TileGame.Components;

public class TextureComponent : Component
{
    private readonly string _name;

    public TextureComponent(string name)
    {
        _name = name;
        Texture = Content.Load<Texture2D>(_name);
    }

    public override void Initialize()
    {
        Texture = Content.Load<Texture2D>(_name);
    }

    public Texture2D Texture { get; set; }
}