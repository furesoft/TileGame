using Microsoft.Xna.Framework.Graphics;
using TileGame.Core;

namespace TileGame.Components;

public class TextureComponent : IComponent
{
    public TextureComponent(string name)
    {
        Texture = IComponent.Content.Load<Texture2D>(name);
    }

    public Texture2D Texture { get; set; }
    public void Initialize()
    {
        
    }

    public void Start()
    {
        
    }
}